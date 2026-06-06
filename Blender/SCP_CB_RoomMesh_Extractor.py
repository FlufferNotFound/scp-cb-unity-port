"""
DISCLAIMER
These extractor were made by generative AI, and are purpose built for extracting the models stored
in RoomMesh model files, and are not guaranteed to work with other games that use RoomMesh files to store their assets.

Also since these are AI generated, do what the hell you want with them.
"""

bl_info = {
    "name": "SCP: Containment Breach RoomMesh Importer (.rmesh)",
    "author": "AI",
    "version": (1, 5, 0),
    "blender": (3, 0, 0),
    "location": "File > Import > RoomMesh (.rmesh)",
    "description": "Imports single or batch SCP: CB rooms into dedicated file collections with dual UV node materials.",
    "category": "Import-Export",
}

import bpy
import struct
import os
from bpy_extras.io_utils import ImportHelper
from bpy.props import StringProperty, CollectionProperty
from bpy.types import Operator, OperatorFileListElement

def read_b3d_string(file):
    """Reads a Blitz3D length-prefixed binary string."""
    length_bytes = file.read(4)
    if not length_bytes or len(length_bytes) < 4:
        return ""
    length = struct.unpack("<I", length_bytes)[0]
    if length == 0:
        return ""
    try:
        return file.read(length).decode('utf-8', errors='ignore').strip()
    except:
        return ""

def create_rmesh_material(diffuse_file, lightmap_file, folder_path):
    """Creates a Node Material pairing Diffuse and Lightmap nodes to correct UV sets."""
    mat_name = os.path.splitext(diffuse_file)[0] if diffuse_file else "Material_RoomMesh"
    
    if mat_name in bpy.data.materials:
        return bpy.data.materials[mat_name]
        
    mat = bpy.data.materials.new(name=mat_name)
    mat.use_nodes = True
    nodes = mat.node_tree.nodes
    links = mat.node_tree.links
    
    bsdf = nodes.get("Principled BSDF")
    
    # Setup Diffuse Layer
    if diffuse_file:
        tex_node = nodes.new('ShaderNodeTexImage')
        tex_node.label = "Diffuse Map"
        tex_node.location = (-300, 300)
        
        img_path = os.path.join(folder_path, diffuse_file)
        if os.path.exists(img_path):
            try:
                tex_node.image = bpy.data.images.load(img_path)
            except Exception as e:
                print(f"Could not load diffuse texture {diffuse_file}: {e}")
                
        uv_node = nodes.new('ShaderNodeUVMap')
        uv_node.uv_map = "UVMap_Diffuse"
        uv_node.location = (-550, 300)
        
        links.new(uv_node.outputs['UV'], tex_node.inputs['Vector'])
        links.new(tex_node.outputs['Color'], bsdf.inputs['Base Color'])

    # Setup Lightmap Layer
    if lightmap_file:
        lm_node = nodes.new('ShaderNodeTexImage')
        lm_node.label = "Lightmap"
        lm_node.location = (-300, -50)
        
        img_path = os.path.join(folder_path, lightmap_file)
        if os.path.exists(img_path):
            try:
                lm_node.image = bpy.data.images.load(img_path)
            except Exception as e:
                print(f"Could not load lightmap texture {lightmap_file}: {e}")
                
        uv_lm_node = nodes.new('ShaderNodeUVMap')
        uv_lm_node.uv_map = "UVMap_Lightmap"
        uv_lm_node.location = (-550, -50)
        
        links.new(uv_lm_node.outputs['UV'], lm_node.inputs['Vector'])
        
    return mat

def load_rmesh(filepath):
    if not os.path.exists(filepath):
        return {'CANCELLED'}

    file_fullname = os.path.basename(filepath)
    room_name = os.path.splitext(file_fullname)[0]
    folder_path = os.path.dirname(filepath)
    
    # 1. Create a dedicated Blender Collection named after the model file
    room_collection = bpy.data.collections.new(name=file_fullname)
    bpy.context.scene.collection.children.link(room_collection)
    
    # 2. Create Root Empty to anchor sub-meshes
    root_obj = bpy.data.objects.new(room_name, None)
    room_collection.objects.link(root_obj)

    with open(filepath, "rb") as f:
        magic = read_b3d_string(f)
        if "RoomMesh" not in magic:
            print(f"Warning: Magic string mismatch ({magic}) in file {file_fullname}.")

        chunk_count = struct.unpack("<I", f.read(4))[0]

        for chunk_idx in range(chunk_count):
            diffuse_file = ""
            lightmap_file = ""
            
            for layer in range(2): 
                tex_flag = f.read(1)
                if tex_flag != b'\x00':
                    tex_path = read_b3d_string(f)
                    if tex_path:
                        if layer == 0:
                            diffuse_file = os.path.basename(tex_path)
                        elif layer == 1:
                            lightmap_file = os.path.basename(tex_path)

            v_count = struct.unpack("<I", f.read(4))[0]
            
            chunk_vertices = []
            chunk_uv_diffuse = []
            chunk_uv_lightmap = []
            chunk_faces = []

            vertex_format = "<fffffffBBB"
            stride_size = struct.calcsize(vertex_format)
            
            for _ in range(v_count):
                v_data = f.read(stride_size)
                vx, vy, vz, u1, v1, u2, v2, r, g, b = struct.unpack(vertex_format, v_data)
                
                chunk_vertices.append((vx, -vz, vy))
                chunk_uv_diffuse.append((u1, 1.0 - v1)) 
                chunk_uv_lightmap.append((u2, 1.0 - v2))

            f_count = struct.unpack("<I", f.read(4))[0]
            
            for _ in range(f_count):
                f_data = f.read(12)
                idx0, idx1, idx2 = struct.unpack("<III", f_data)
                chunk_faces.append((idx0, idx1, idx2))
                
            # Create sub-mesh object
            obj_name = f"{room_name}_{os.path.splitext(diffuse_file)[0] if diffuse_file else f'chunk_{chunk_idx}'}"
            mesh = bpy.data.meshes.new(name=obj_name)
            obj = bpy.data.objects.new(obj_name, mesh)
            
            room_collection.objects.link(obj)
            obj.parent = root_obj
            
            mesh.from_pydata(chunk_vertices, [], chunk_faces)
            mesh.update()

            # Assign UV Layers
            if chunk_vertices:
                uv_layer_diff = mesh.uv_layers.new(name="UVMap_Diffuse")
                uv_layer_lm = mesh.uv_layers.new(name="UVMap_Lightmap")
                
                for poly in mesh.polygons:
                    for loop_index in poly.loop_indices:
                        v_idx = mesh.loops[loop_index].vertex_index
                        if v_idx < len(chunk_uv_diffuse):
                            uv_layer_diff.data[loop_index].uv = chunk_uv_diffuse[v_idx]
                            uv_layer_lm.data[loop_index].uv = chunk_uv_lightmap[v_idx]

            # Generate material setup
            mat = create_rmesh_material(diffuse_file, lightmap_file, folder_path)
            obj.data.materials.append(mat)

    return {'FINISHED'}

class ImportRoomMesh(Operator, ImportHelper):
    bl_idname = "import_scene.rmesh"
    bl_label = "Import .rmesh"
    filename_ext = ".rmesh"
    filter_glob: StringProperty(default="*.rmesh", options={'HIDDEN'})

    # Properties required for batch/multi-file selection
    directory: StringProperty(subtype='DIR_PATH')
    files: CollectionProperty(type=OperatorFileListElement, options={'HIDDEN', 'SKIP_SAVE'})

    def execute(self, context):
        if self.files:
            # Process batch selection
            for file_elem in self.files:
                filepath = os.path.join(self.directory, file_elem.name)
                load_rmesh(filepath)
        else:
            # Fallback for single file path fallthrough
            load_rmesh(self.filepath)
            
        return {'FINISHED'}

def menu_func_import(self, context):
    self.layout.operator(ImportRoomMesh.bl_idname, text="SCP RoomMesh (.rmesh)")

def register():
    bpy.utils.register_class(ImportRoomMesh)
    bpy.types.TOPBAR_MT_file_import.append(menu_func_import)

def unregister():
    bpy.utils.unregister_class(ImportRoomMesh)
    bpy.types.TOPBAR_MT_file_import.remove(menu_func_import)

if __name__ == "__main__":
    register()