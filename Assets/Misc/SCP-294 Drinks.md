# How to create your own drinks for SCP-294.

All the drink definitions for SCP-294 are stored in scp-294.json.

To add, edit or remove drink, there's two ways to do it.
Manually editing the JSON file, or using the in-game editor.

When editing the JSON, copy the following template:

    {
        "DrinkName": [
            "Example drink"
        ],
        "RGBA": [
            255,
            255,
            255,
            255
        ],
        "Emissive": true,
        "DrinkSound": "",
        "DispenseSound": "",
        "IsDeadly": true,
        "DeathMessage": "Subject D-9341 found dead in [REDACTED]. Cause of death unknown.",
        "BlurTime": 5,
        "DrinkMessage": "Quite tasteful.",
        "DamageAmount": -1.5,
        "Bloodloss": -4,
        "Stomachache": true,
        "StaminaEffectT": 6,
        "StaminaEffectTimer": 125,
        "BlinkEffect": 6,
        "BlinkEffectTimer": 350,
        "DeathTimer": 5,
        "Explosion": true
    }

## Properties

**DrinkName** contains the various names a drink can do by. Example:
"DrinkName": [
"Water",
"H2O",
"Hydrohomie",
]

**RGBA** contains the Red, Green, Blue, Alpha values for the liquid in the cup. Example:
"RGBA": [
255, //Red
255, //Green
255, //Blue
255 //Alpha or transparency
],

**Emissive** is a true/false value that makes the liquid in the cup glow. Example:
"Emissive" : true
"Emissive" : false

**DrinkSound** is the sound to play when drinking. Example:
"DrinkSound": "",

**DispenseSound** is the sound to play when 294 dispenses the drink.
"DispenseSound": "",

**IsDeadly** kills the player upon consuming the drink. Example
"IsDeadly": true,

**DeathMessage** is the message to show in the game over screen if the player dies when drinking the liquid. Example:
"DeathMessage": "Subject D-9341 found dead in [REDACTED]. Cause of death unknown.",

**BlurTime** how many seconds to blur the screen after drinking the liquid. Example:
"BlurTime": 5,

**DrinkMessage** text to show after drinking the liquid. Example:
"DrinkMessage": "Quite tasteful.",

**DamageAmount** increases the injuries-value. Example:
"DamageAmount": -1.5,

**Bloodloss** increases the blood loss value. Example
"Bloodloss": -4

**Stomachache** has the same effect as appendicitis caused by SCP-1025 (decreased stamina and messages about stomach ache). Example
"Stomachache": true,

**StaminaEffect** controls how fast stamina decreases. 1.0 = normal speed 2.0 = decreases twice as fast, 0.0 = doesn't decrease at all. Example:
"StaminaEffectT": 6,

"StaminaEffectTimer": 125,

**BlinkEffect** controls how long blinks lasts. Example:
"BlinkEffect": 6,

**BlinkEffectTimer** controls how long the blink effect lasts. Example:
"BlinkEffectTimer": 350,

**DeathTimer** time in seconds to kill the player after after drinking. Example:
"DeathTimer": 5,

**Explosion** causes an explosion after the drink is dispenssed. Example:
"Explosion": true
