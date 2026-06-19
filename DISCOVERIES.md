# This is where discoveries made throughout development go.

Follow this format when putting stuff here.

---

#### YYYY-MM-DD

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
