"""
DISCLAIMER
These extractor were made by generative AI, and are purpose built for extracting the models stored
in DirectX model files, and are not guaranteed to work with other games that use DirectX files to store their assets.

Also since these are AI generated, do what the hell you want with them.
"""

bl_info = {
    "name": "DirectX Importer (Batch, Binary & ASCII) (.x)",
    "author": "AI",
    "version": (1, 4),
    "blender": (4, 0, 0),
    "location": "File > Import",
    "description": "Batch imports geometry/UVs from multiple text or binary (.x) files into dedicated collections.",
    "category": "Import-Export",
}

import bpy
import struct
import zlib
import re
import os
from bpy_extras.io_utils import ImportHelper
from bpy.props import StringProperty, CollectionProperty
from bpy.types import Operator, OperatorFileListElement

class DirectXParser:
    def __init__(self, filepath, target_collection):
        self.filepath = filepath
        self.target_collection = target_collection
        self.decompressed_data = bytearray()
        self.float_size = 32
        
    def parse_file(self):
        with open(self.filepath, "rb") as f:
            header = f.read(16)
            if len(header) < 16 or header[0:4] != b'xof ':
                raise ValueError("Not a valid DirectX file.")
                
            format_type = header[8:12]
            float_size_str = header[12:16]
            
            self.float_size = 32 if float_size_str == b'0032' else 64
            
            if format_type == b'bzip':
                self.decompress_mszip(f.read())
                self.process_binary_tokens()
            elif format_type == b'bin ':
                self.decompressed_data = bytearray(f.read())
                self.process_binary_tokens()
            elif format_type == b'txt ':
                text_content = f.read().decode('ascii', errors='ignore')
                tokens = self.tokenize_ascii(text_content)
                self.parse_ascii_structures(tokens)

    # ==========================================
    # BINARY / COMPRESSED PARSING PATH
    # ==========================================
    def decompress_mszip(self, compressed_payload):
        idx = 0
        length = len(compressed_payload)
        while idx < length:
            if idx + 2 > length:
                break
            if compressed_payload[idx:idx+2] == b'CK':
                idx += 2
                try:
                    decompressor = zlib.decompressobj(-15)
                    chunk = decompressor.decompress(compressed_payload[idx:])
                    self.decompressed_data.extend(chunk)
                    consumed = len(compressed_payload[idx:]) - len(decompressor.unused_data)
                    idx += consumed
                except Exception as e:
                    print(f"MSZIP block decompression failed: {e}")
                    break
            else:
                idx += 1

    def process_binary_tokens(self):
        view = memoryview(self.decompressed_data)
        idx = 0
        stream_len = len(view)
        tokens = []
        
        while idx < stream_len:
            if idx + 2 > stream_len:
                break
            token_id = struct.unpack_from("<H", view, idx)[0]
            idx += 2
            
            if token_id == 1:    # TOKEN_NAME
                count = struct.unpack_from("<I", view, idx)[0]
                idx += 4
                name = view[idx:idx+count].tobytes().decode('ascii', errors='ignore')
                idx += count
                tokens.append(('NAME', name))
            elif token_id == 2:  # TOKEN_STRING
                count = struct.unpack_from("<I", view, idx)[0]
                idx += 4
                string_val = view[idx:idx+count].tobytes().decode('ascii', errors='ignore')
                idx += count + 4 
                tokens.append(('STRING', string_val))
            elif token_id == 3:  # TOKEN_INTEGER
                val = struct.unpack_from("<i", view, idx)[0]
                idx += 4
                tokens.append(('INT', val))
            elif token_id == 5:  # TOKEN_GUID
                idx += 16
                tokens.append(('GUID', ''))
            elif token_id == 6:  # TOKEN_INTEGER_LIST
                count = struct.unpack_from("<I", view, idx)[0]
                idx += 4
                vals = list(struct.unpack_from(f"<{count}i", view, idx))
                idx += count * 4
                tokens.append(('INT_LIST', vals))
            elif token_id == 7:  # TOKEN_FLOAT_LIST
                count = struct.unpack_from("<I", view, idx)[0]
                idx += 4
                if self.float_size == 32:
                    vals = list(struct.unpack_from(f"<{count}f", view, idx))
                    idx += count * 4
                else:
                    vals = list(struct.unpack_from(f"<{count}d", view, idx))
                    idx += count * 8
                tokens.append(('FLOAT_LIST', vals))
            elif token_id == 10: # TOKEN_OBRACE
                tokens.append(('OBRACE', '{'))
            elif token_id == 11: # TOKEN_CBRACE
                tokens.append(('CBRACE', '}'))
            elif token_id in (12, 13, 14, 15):
                tokens.append(('SYMBOL', token_id))
            else:
                pass

        self.parse_binary_mesh_structures(tokens)

    def parse_binary_mesh_structures(self, tokens):
        i = 0
        num_tokens = len(tokens)
        while i < num_tokens:
            if tokens[i][0] == 'NAME' and tokens[i][1] == 'Mesh':
                mesh_name = "DirectX_Mesh"
                i += 1
                if i < num_tokens and tokens[i][0] == 'NAME':
                    mesh_name = tokens[i][1]
                    i += 1
                
                if i < num_tokens and tokens[i][0] == 'OBRACE':
                    i += 1
                    brace_depth = 1
                    mesh_tokens = []
                    
                    while i < num_tokens and brace_depth > 0:
                        t_type, _ = tokens[i]
                        if t_type == 'OBRACE':
                            brace_depth += 1
                        elif t_type == 'CBRACE':
                            brace_depth -= 1
                            if brace_depth == 0:
                                i += 1
                                break
                        mesh_tokens.append(tokens[i])
                        i += 1
                        
                    self.extract_single_binary_mesh(mesh_name, mesh_tokens)
            else:
                i += 1

    def extract_single_binary_mesh(self, name, tokens):
        vertices = []
        faces = []
        uvs = []
        mt_idx = 0
        num_mt = len(tokens)
        
        while mt_idx < num_mt:
            if tokens[mt_idx][0] == 'FLOAT_LIST':
                f_list = tokens[mt_idx][1]
                for j in range(0, len(f_list), 3):
                    if j + 2 < len(f_list):
                        vertices.append((f_list[j], f_list[j+1], f_list[j+2]))
                mt_idx += 1
                break
            mt_idx += 1
            
        face_int_stream = []
        uv_tokens = []
        
        while mt_idx < num_mt:
            t_type, t_val = tokens[mt_idx]
            if t_type == 'NAME' and t_val == 'MeshTextureCoords':
                uv_depth = 0
                while mt_idx < num_mt:
                    if tokens[mt_idx][0] == 'OBRACE':
                        uv_depth += 1
                    elif tokens[mt_idx][0] == 'CBRACE':
                        uv_depth -= 1
                        if uv_depth == 0:
                            break
                    uv_tokens.append(tokens[mt_idx])
                    mt_idx += 1
            elif t_type == 'INT':
                face_int_stream.append(t_val)
            elif t_type == 'INT_LIST':
                face_int_stream.extend(t_val)
            mt_idx += 1
            
        if face_int_stream:
            f_count = face_int_stream[0]
            f_stream_idx = 1
            for _ in range(f_count):
                if f_stream_idx >= len(face_int_stream):
                    break
                num_indices = face_int_stream[f_stream_idx]
                f_stream_idx += 1
                if f_stream_idx + num_indices <= len(face_int_stream):
                    face_indices = face_int_stream[f_stream_idx : f_stream_idx + num_indices]
                    faces.append(tuple(face_indices))
                    f_stream_idx += num_indices

        if uv_tokens:
            uv_floats = []
            for ut in uv_tokens:
                if ut[0] == 'FLOAT_LIST':
                    uv_floats.extend(ut[1])
            for k in range(0, len(uv_floats), 2):
                if k + 1 < len(uv_floats):
                    uvs.append((uv_floats[k], uv_floats[k+1]))
                    
        if vertices and faces:
            self.build_mesh(name, vertices, faces, uvs)

    # ==========================================
    # ASCII / TEXT PARSING PATH
    # ==========================================
    def tokenize_ascii(self, text):
        text = re.sub(r'//.*', '', text) 
        token_pattern = re.compile(r'([a-zA-Z_][a-zA-Z0-9_]*)|([-+]?(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][-+]?\d+)?)|(\{)|(\})|("([^"]*)")')
        
        tokens = []
        for match in token_pattern.finditer(text):
            name, num, obrace, cbrace, string_outer, string_inner = match.groups()
            if name:
                tokens.append(('NAME', name))
            elif num:
                tokens.append(('NUMBER', float(num) if ('.' in num or 'e' in num or 'E' in num) else int(num)))
            elif obrace:
                tokens.append(('OBRACE', '{'))
            elif cbrace:
                tokens.append(('CBRACE', '}'))
            elif string_outer:
                tokens.append(('STRING', string_inner))
        return tokens

    def parse_ascii_structures(self, tokens):
        i = 0
        num_tokens = len(tokens)
        while i < num_tokens:
            if tokens[i][0] == 'NAME' and tokens[i][1] == 'Mesh':
                mesh_name = "DirectX_Mesh"
                i += 1
                if i < num_tokens and tokens[i][0] == 'NAME':
                    mesh_name = tokens[i][1]
                    i += 1
                
                if i < num_tokens and tokens[i][0] == 'OBRACE':
                    i += 1
                    vertices = []
                    faces = []
                    uvs = []
                    
                    if i < num_tokens and tokens[i][0] == 'NUMBER':
                        v_count = int(tokens[i][1])
                        i += 1
                        for _ in range(v_count):
                            if i + 2 < num_tokens and tokens[i][0] == 'NUMBER' and tokens[i+1][0] == 'NUMBER' and tokens[i+2][0] == 'NUMBER':
                                vertices.append((float(tokens[i][1]), float(tokens[i+1][1]), float(tokens[i+2][1])))
                                i += 3
                    
                    if i < num_tokens and tokens[i][0] == 'NUMBER':
                        f_count = int(tokens[i][1])
                        i += 1
                        for _ in range(f_count):
                            if i < num_tokens and tokens[i][0] == 'NUMBER':
                                idx_count = int(tokens[i][1])
                                i += 1
                                face_indices = []
                                for _ in range(idx_count):
                                    if i < num_tokens and tokens[i][0] == 'NUMBER':
                                        face_indices.append(int(tokens[i][1]))
                                        i += 1
                                faces.append(tuple(face_indices))
                    
                    brace_depth = 1
                    while i < num_tokens and brace_depth > 0:
                        t_type, t_val = tokens[i]
                        if t_type == 'OBRACE':
                            brace_depth += 1
                            i += 1
                        elif t_type == 'CBRACE':
                            brace_depth -= 1
                            i += 1
                        elif t_type == 'NAME' and t_val == 'MeshTextureCoords':
                            i += 1
                            while i < num_tokens and tokens[i][0] != 'OBRACE':
                                i += 1
                            if i < num_tokens and tokens[i][0] == 'OBRACE':
                                brace_depth += 1
                                i += 1
                                if i < num_tokens and tokens[i][0] == 'NUMBER':
                                    uv_count = int(tokens[i][1])
                                    i += 1
                                    for _ in range(uv_count):
                                        if i + 1 < num_tokens and tokens[i][0] == 'NUMBER' and tokens[i+1][0] == 'NUMBER':
                                            uvs.append((float(tokens[i][1]), float(tokens[i+1][1])))
                                            i += 2
                        else:
                            i += 1
                            
                    if vertices and faces:
                        self.build_mesh(mesh_name, vertices, faces, uvs)
            else:
                i += 1

    # ==========================================
    # BLENDER ENGINE GENERATOR
    # ==========================================
    def build_mesh(self, name, vertices, faces, uvs=None):
        mesh = bpy.data.meshes.new(name=name)
        obj = bpy.data.objects.new(mesh.name, mesh)
        
        # Link directly into the file's specified isolation Collection
        self.target_collection.objects.link(obj)
        
        mesh.from_pydata(vertices, [], faces)
        mesh.update()
        
        if uvs and len(uvs) >= len(vertices):
            uv_layer = mesh.uv_layers.new(name="UVMap")
            for loop in mesh.loops:
                v_idx = loop.vertex_index
                if v_idx < len(uvs):
                    uv_layer.data[loop.index].uv = (uvs[v_idx][0], 1.0 - uvs[v_idx][1])


class IMPORT_OT_directx_universal(Operator, ImportHelper):
    bl_idname = "import_scene.directx_universal"
    bl_label = "Import DirectX (.x)"
    bl_options = {'REGISTER', 'UNDO'}
    
    filename_ext = ".x"
    filter_glob: StringProperty(default="*.x", options={'HIDDEN'})
    
    # Enable multiple file processing
    directory: StringProperty(subtype="DIR_PATH")
    files: CollectionProperty(type=OperatorFileListElement, options={'HIDDEN', 'SKIP_SAVE'})

    def execute(self, context):
        if not self.files:
            self.report({'WARNING'}, "No files selected.")
            return {'CANCELLED'}
            
        success_count = 0
        
        for file_elem in self.files:
            filepath = os.path.join(self.directory, file_elem.name)
            folder_name = file_elem.name # e.g., "monitor.x"
            
            # 1. Create unique collection for this file
            file_collection = bpy.data.collections.get(folder_name)
            if not file_collection:
                file_collection = bpy.data.collections.new(folder_name)
                context.scene.collection.children.link(file_collection)
            
            # 2. Parse and send meshes to collection
            parser = DirectXParser(filepath, file_collection)
            try:
                parser.parse_file()
                success_count += 1
            except Exception as e:
                self.report({'ERROR'}, f"Failed to extract {file_elem.name}: {str(e)}")
                
        self.report({'INFO'}, f"Successfully batch imported {success_count} DirectX file(s).")
        return {'FINISHED'}


def menu_func_import(self, context):
    self.layout.operator(IMPORT_OT_directx_universal.bl_idname, text="DirectX Model (.x)")

def register():
    bpy.utils.register_class(IMPORT_OT_directx_universal)
    bpy.types.TOPBAR_MT_file_import.append(menu_func_import)

def unregister():
    bpy.types.TOPBAR_MT_file_import.remove(menu_func_import)
    bpy.utils.unregister_class(IMPORT_OT_directx_universal)

if __name__ == "__main__":
    register()