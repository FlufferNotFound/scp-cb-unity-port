# This is where discoveries made throughout development go.

Follow this format when putting stuff here.

---

#### YYYY-MM-DD :

Put whatever you discover here.\

**And put your name here.**

---

#### 2026-06-07

The original game uses these fonts

- Courier New
- Courier New Bold
- DS-Digital
- Journal

So what's the problem?

Fonts have licenses such as OFL, the issue is that some have licenses that may or may not allow commercial/non-commercial, be free or have a fee, etc...

The terms differ but tl;dr I searched the licensing terms for these fonts but couldn't find conclusive results, and though this project is non-commercial, since I'm not gonna get into a legal dispute or whatever, I won't include them and instead replace them with free ones under OFL.

**FlufferNotFound**

---

#### 2026-06-16

Pretty much all model files have been extracted, tho only two models remain and those are the .smf, or "4×4 Evolution Model File".

I found some tools that could open or extract smf files, but none worked so I had to use AI to reverse engineer and extract.

Johnny Shumway, a.k.a "JShum00", https://github.com/JShum00/SMFViewer

I'm sorry for using AI but I literally don't have the time or experience to sit down and reverse engineer it.

---

While working on SCP-035's model and armature so they're easiert to work with latter, I discovered that the models are HUGE (maybe the size got messed up during import).

Not only that, there were plenty of small bones with names like _no_name_ that from toying around with the armature, appeared to do nothing.

In addition, some of the weights were weird, like when moving Bip01_RUpArmTwist, part of the forearm would move bot not its surrounding area.
There were also redundant bones which I had to remove.

Upon closer inspection, I appears this is the case for all character armatures.

**FlufferNotFound**

---

#### 2026-06-19

035's mesh is, messy...
I was expecting a single solid mesh, but it's actually made of four different meshes, the body, shoes and right arm.

This caused any issues, but since I edited the armature, fixed self intersecting verts, etc...
when weight painting, the weights aren't correctly mirrored, ~~and while checking the UVs over the pants, even on the unaltered model there's some weird mangling issues.~~

These "mangled" uvs are actually not mangled, there's texture work under them.
I don't want to know why they were placed like this.

This all means I have to modify the model EVEN more which is something I REALLY don't want to do, but will have to.

**FlufferNotFound**

---

#### 2026-06-20

035's model is now finished and imported to Unity

Upon closer inspection 106's model and armature aren't as messy as 035's.

Ofc there's self intersecting verts but that's all for the mesh.

The armature has a lot of duplicate bones in the arm and root, third spine, not sure why.

---

106's model was the easiest character to prepare, he's now in Unity.

**FlufferNotFound**

---

#### 2026-06-22

~~While working on the Class-D's model I noticed it uses the same mesh as 035, and so does more characters.~~
~~This means I can just re use the same mesh and call it a day.~~

Nvm, the model may "be" the same but the UVs are different.

Also, while working on the zombie surgeon, all models are _no_name_ bones and a lot of tiny bones that don't serve a use.

**FlufferNotFound**

---

#### 2026-06-27

The MTF's model has the P90 built in, as in it's part of the same mesh.
Also, not to discredit the original modeller of this model, the MTF's topology
is a bit messy in certain areas.

And just like other characters, there's tons of bones that serve no purpose.

---

The topology in Clerk's model is the messiest I've seen so far... hopefully it's just this one.

**FlufferNotFound**

---

#### 2026-06-28

SCP-079's model has plenty of modelled details, like individual keys on the keyboard, batteries, etc..

**FlufferNotFound**

---

#### 2026-06-29

Origami nothing special.

Severed hand has an armature bone that serves no purpose, removed.

SCP-500 was the eassiest model to port, just had to edit the material, remove intersecting verts and voila!

**FlufferNotFound**

---

#### 2026-07-01

All armatures have bones named no_name that servere no purpose.
I won't ask, why they were added, but most likely to make the model difficult to work with, idk.

Nothing interesting in the Hazmat suit.

**FlufferNotFound**

---

#### 2026-07-01

Nothing much noting on the NVGs, just the topology is a bit messy.

The most armatures have a bone named 'FIRESPOT'. I have no clue what this bones does, and there's no reference to it in the og game's src.

---

There's two 1048's models, scp-1048.b3d and scp-1048pp.b3d. Only difference is that pp has the drawing paper, but other one doesn't.

1048's model is split into multiple parts for its body.

**FlufferNotFound**

---

#### 2026-07-03

SCP-1048A has two materials, one for the body and another one for the bowtie.
The bowtie material uses 1048's texture.

**FlufferNotFound**

---

#### 2026-07-04

SCP-096 armature instead of bones, his armature was imported as a ton of empty objects, dunno why.

The empty axis objects are parented to his mesh, but EVERY empty axis is parented in a weird way I call the tower of Scene_Root hell

096's mesh isn't perefectly symmetric, but given that it's very complex, i'm not gonna symmetrize it. Just add some bones and call it a day.

**FlufferNotFound**

---

#### 2026-07-05

For some reason, a lot of the Blitz3D have armatures built in with no bones or puporse.
The freaking wallet has an armature, for what?

**FlufferNotFound**

---

#### 2026-07-06

Nothing interesting on SCP-1123 but 427 is something else...
the topology on the decour is high quality topology gore content...

---

scp-066.b3d contains more than just eric's toy, it also has 049's model, FOR SOME REASON WTF.

066's model has two materials, Material_001\_\_new066-2_1\_\_jpg and Material_001\_\_066_baketest2, both are the same.

066 has an armature, keeping it.

**FlufferNotFound**

---

#### 2026-07-06

scp-066.b3d contains more than just eric's toy, it also has 049's model, FOR SOME REASON WTF.

066's model has two materials, Material_001\_\_new066-2_1\_\_jpg and Material_001\_\_066_baketest2, both are the same.

066 has an armature, keeping it.

**FlufferNotFound**

---

#### 2026-07-07

SCP-049's armature has the same issues as the other characters, intersecting bones that have no purpose.

**FlufferNotFound**

---

#### 2026-07-11

Unity throws an error when importing fcveny.ogg.
At first I didn't know why and after a lil investigation with ffprobe, turns out the audio is corrupted or something.

[ogg @ 0000029b4b6af7c0] Format ogg detected only with low score of 1, misdetection possible!

[ogg @ 0000029b4b6af7c0] CRC mismatch!

    Last message repeated 1 times

[ogg @ 0000029b4b6af7c0] Header processing failed: Invalid data found when processing input

.\fcveny.ogg: Invalid data found when processing input

I tried importing the file into audacity but it didn't work on the first try, so I imported it as raw data.
Then I thought, if the audio is corrupted, then exporting it again should fix the issue, and it did.

---

Files that have any of these extensions: .pt, .pd, .s, .sc, .it are just jpgs under disguise.

---

All audio and texture files have been imported to Unity. Just need to work on the 3D models.

**FlufferNotFound**

---

#### 2026-07-13

The guards armature is weird.

The rocks have armature bones. May I ask why?

Even the trees have bones, wtf.

**FlufferNotFound**

---

#### 2026-07-13

I tried looking for a replacement for DS-Digital but couldn't find any, thankfully the font is under a Shareware license.

**FlufferNotFound**

---

#### 2026-07-18

Textures 294.png and 294test.png are the same same texture, there's no difference between the two.

**FlufferNotFound**

---

#### 2026-07-20

SCP-294's model has a lot of modelled details, like a keyboard, drink dispencer, etc...

---

The keycard model was made in a editor that uses brushes, like hammer editor, so to speak...

---

These materials in SCP-939, Material_004\_\_939_extremities and scp-939_licker_extremities2 have share the same texture.

**FlufferNotFound**

---

#### 2026-07-22

SCP-966 texture has D-9341's reflection in the pupils.

Like some other models, the armature is not made of bones, rather empty objects.

---

The models for SCP-205 have some interesting names, the og's devs new people would be poking around the game files.

205_demon1.b3d - "look behind you"
205_demon2.b3d - "these arent the decompile your looking for"
205_demon3.b3d - "If your reading this your already dead"
205_woman.b3d - "you could of saved her"

---

SCP-860's monster has this name

forestmonster.b3d - "regalis'\_mom_lol"

it also has a bone named "kenneth"

**FlufferNotFound**
