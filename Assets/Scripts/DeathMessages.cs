using UnityEngine;

/*
 * Please note that these are the death messages I found in the game's Wiki.
 * More will be added as I parse through the spaghetti nightmare that is the og's code.
 * Also, some of these messages have typos in them, but won't be corrected to stay as 1:1 as possible with the original game.
 * 
 * As per death messages caused by SCP-294, those are defined in drinks.json.
 */

public class DeathMessages
{

    public static readonly string[] deathMessages = {
        // 0
        "Subject D-9341 found ingesting Dr. [REDACTED] at Sector [REDACTED]. Subject immediately terminated by Nine-Tailed Fox and sent for autopsy. SCP-008 infection was confirmed, after which the body was incinerated.",
        // 1
        "The whereabouts of SCP-1499 are still unknown, but a recon team has been dispatched to investigate reports of a violent attack to a church in the Russian town of [REDACTED].",
        // 2
        "Subject D-9341 found wandering around Gate A. Subject was immediately terminated by Nine-Tailed Fox and sent for autopsy. SCP-008 infection was confirmed, after which the body was incinerated.",
        // 3
        "Subject D-9341 found wandering around Gate B. Subject was immediately terminated by Nine-Tailed Fox and sent for autopsy. SCP-008 infection was confirmed, after which the body was incinerated.",
        // 4
        "Subject D-9341. Cause of death: multiple lacerations and severe blunt force trauma caused by [DATA EXPUNGED], who was infected with SCP-008. Said subject was located by Nine-Tailed Fox and terminated.",
        // 5
        "Subject D-9341 found in a pool of blood next to SCP-012. Subject seems to have ripped open his wrists and written three extra lines to the composition before dying of blood loss.",
        // 6
        "We will need more than the regular cleaning team to take care of this. Two large and highly active tentacle-like appendages seem to have formed inside the chamber. Their level of aggression is unlike anything we've seen before - it looks like they have beaten some unfortunate Class D to death at some point during the breach.",
        // 7
        "One large and highly active tentacle-like appendage seems to have grown outside the dead body of a scientist within office area [REDACTED]. Its level of aggression is unlike anything we've seen before - it looks like it has beaten some unfortunate Class D to death at some point during the breach.",
        // 8
        "Class-D Subject D-9341 found dead inside SCP-035's containment chamber. The subject exhibits heavy hemorrhaging of blood vessels around the eyes and inside the mouth and nose. Sent for autopsy.",
        // 9
        "Subject D-9341. Cause of death: multiple lacerations and severe blunt force trauma caused by an instance of SCP-049-2.",
        // 10
        "Three (3) active instances of SCP-049-2 discovered in the tunnel outside SCP-049's containment chamber. Terminated by Nine-Tailed Fox.",
        // 11
        "An active instance of SCP-049-2 was discovered in [REDACTED]. Terminated by Nine-Tailed Fox.",
        // 12
        "A large amount of blood found in [DATA REDACTED]. DNA identified as Subject D-9341. Most likely [DATA REDACTED] by SCP-096.",
        // 13
        "Subject D-9341. Cause of Death: Fatal cervical fracture. The surveillance tapes confirm that the subject was killed by SCP-173.",
        // 14
        "If I'm not mistaken, one of the main purposes of these rooms was to stop SCP-173 from moving further in the event of a containment breach. So, whose brilliant idea was it to put A GODDAMN MAN-SIZED VENTILATION DUCT in there?!",
        // 15
        "Subject D-9341: Fatal cervical fracture. Assumed to be attacked by SCP-173.",
        // 16
        "Subject D-9341. Cause of death: Fatal cervical fracture. According to Security Chief Franklin who was present at SCP-173's containment chamber during the breach, the subject was killed by SCP-173 as soon as the disruptions in the electrical network started.",
        // 17
        "The SCP-205 cycle seems to have resumed its normal course after the anomalies observed during [REDACTED]. The body of a Class D subject D-9341 was discovered inside the chamber. The subject exhibits signs of blunt force trauma typical for personnel who have entered the chamber when the lights are off.",
        // 18
        "Requesting support from MTF Nu-7. We need more firepower to take this thing down.\r\n",
        // 19
        "What we know is that he died of cardiac arrest. My guess is that it was caused by SCP-895, although it has never been observed affecting video equipment from this far before. Further testing is needed to determine whether SCP-895's \"Red Zone\" is increasing.",
        // 20
        "Class D viewed SCP-895 through a pair of digital night vision goggles, killing him.",
        // 21
        "Class D viewed SCP-895 through a pair of digital night vision goggles, presumably enhanced by SCP-914. It might be possible that the subject was able to resist the memetic effects partially through these goggles. The goggles have been stored for further study.",
        // 22
        "A heavily mutilated corpse found inside the output booth of SCP-914. DNA testing identified the corpse as Class D Subject D-9341. The subject had obviously been \"refined\" by SCP-914 on the \"Rough\" setting, but we are still confused as to how he ended up inside the intake booth and who or what wound the key.",
        // 23
        "A Class D jumpsuit found in [DATA REDACTED]. Upon further examination, the jumpsuit was found to be filled with 12.5 kilograms of blue ash-like substance. Chemical analysis of the substance remains non-conclusive. Most likely related to SCP-914.",
        // 24
        "Subject D-9341 found dead inside SCP-914's output booth next to what appears to be an ordinary 9V battery. The subject is covered in severe electrical burns, and assumed to be killed via an electrical shock caused by the battery. The battery has been stored for further study.",
        // 25
        "Subject D-9341 found in a comatose state in [DATA REDACTED]. The subject was holding what appears to be a cigarette and smiling widely. Chemical analysis of the cigarette has been inconclusive, although it seems to contain a high concentration of an unidentified chemical whose molecular structure is remarkably similar to that of tetrahydrocannabinol.",
        // 26
        "All four escaped SCP-939 (4) specimens have been captured and recontained successfully. Three (3) of them made quite a mess at Storage Area 6. A cleaning team has been dispatched.",
        // 27
        "He died of a cardiac arrest after reading SCP-1025, that's for sure. Is there such a thing as psychosomatic cardiac arrest, or does SCP-1025 have some anomalous properties we are not yet aware of?",
        // 28
        "Subject D-9341 was shot dead after attempting to attack a member of Nine-Tailed Fox. Surveillance tapes show that the subject had been wandering around the site approximately 9 minutes prior, shouting the phrase \"get rid of the four pests\" in chinese. SCP-1123 was found in [REDACTED] nearby, suggesting the subject had come into physical contact with it. How exactly SCP-1123 was removed from its containment chamber is still unknown.",
        // 29
        "A dead Class D subject was discovered within the containment chamber of SCP-1162. An autopsy revealed that his right lung was missing, which suggests interaction with SCP-1162.",
        // 30
        "All personnel situated within Evacuation Shelter LC-2 during the breach have been administered Class-B amnestics due to Incident 1499-E. The Class D subject involved in the event died shortly after being shot by Agent [REDACTED].",
        // 31
        "An unidentified male and a deceased Class D subject were discovered in [REDACTED] by the Nine-Tailed Fox. The man was described as highly agitated and seemed to only speak Russian. He's been taken into a temporary holding area at [REDACTED] while waiting for a translator to arrive.",
        // 32
        "Subject D-9341. Body partially decomposed by what is assumed to be SCP-106's \"corrosion\" effect. Body disposed of via incineration.",
        // 33
        "In addition to the decomposed appearance typical of the victims of SCP-106, the body exhibits injuries that have not been observed before: massive skull fracture, three broken ribs, fractured shoulder and heavy lacerations.",
        // 34
        "In addition to the decomposed appearance typical of the victims of SCP-106, the subject seems to have suffered multiple heavy fractures to both of his legs.",
        // 35
        "Subject D-9341. Terminated by Nine-Tailed Fox.",
        // 36
        "Subject D-9341. Died of blood loss after being shot by Nine-Tailed Fox.",
        // 37
        "Subject D-9341. Cause of death: Gunshot wound to the head. The surveillance tapes confirm that the subject was terminated by Agent Ulgrin shortly after the site lockdown was initiated.",
        // 38
        "CH-2 to control. Shot down a runaway Class D at Gate B.",
        // 39
        "CH-2 to control. Shot down a runaway Class D at Gate A.",
        // 40
        "Agent G. to control. Eliminated a Class D escapee in Gate B's courtyard.",
        // 41
        "Subject D-9341 found dead in [DATA REDACTED]. Cause of death: suffocation due to decontamination gas.",
        // 42
        "Subject D-9341 killed by the Tesla Gate at [REDACTED]"
    };

    //Eat some Dr, get shot by MTF
    public const int prion_zombie_dr = 0;

    //Get 008, put on 1499, die.
    public const int prion_zombie_gp5 = 1;

    //Be at Gate A, get killed by 008 and then MTFs
    public const int prion_zombie_gate_a = 2;

    //Be at Gate B, get killed by 008 and then MTFs
    public const int prion_zombie_gate_b = 3;

    //Killed by 008 zombie 
    public const int prion_zombie_beaten = 4;

    //Attempt to finish On Mount Golgotha. Spoiler alert, you can't
    public const int composition_score = 5;

    //Killed by the tentacles in 035's chamber
    public const int tentacles_chamber = 6;

    //Beaten my 035's tentacle in the office
    public const int tentacles_office = 7;

    //Stay in 035s chamber for far too long.
    public const int comedy_mask_chamber = 8;

    //049-2 zombie beat you up
    public const int plague_zombie_beaten = 9;

    //Become 049-2 in 049's chamber, get shot by MTF
    public const int plague_zombie_chamber = 10;

    //Become 049-2 anywhere else, get shot by MTF
    public const int plague_zombie_anywhere = 11;

    //Removed from life by the shy guy
    public const int shy_guy = 12;

    //Killed by 173 in lockroom, storeroom or 895's chamber
    public const int statue_rooms = 13;

    //Killed by 173 in the T-shape lockrooms
    public const int statue_tlock = 14;

    //Killed by 173 in other rooms
    public const int statue_any_rooms = 15;

    //Killed by peanut at the start of the game
    public const int statue_start = 16;

    //Watching 205 do its thing
    public const int lamps_show = 17;

    //Killed by 427
    public const int collar_427 = 18;

    //Killed by 895 by watching the monitor
    public const int screen = 19;

    //Killed by 895 with regular NVGs
    public const int regular_nvgs = 20;

    //Killed by 895 with Super NVGs
    public const int super_nvgs = 21;

    //Rough refining
    public const int rough_death = 22;

    //Very fine refining
    public const int very_fine = 23;

    //Strange battery
    public const int strange_battery = 24;

    //Smoking joint or smelly joint
    public const int joint = 25;

    //Attacked by 939s
    public const int red_lizard = 26;

    //Reading encyclopedia of diseases
    public const int encyclopedia = 27;

    //Touch 1123 outside its chamber
    public const int skull_outside = 28;

    //Death by 1162
    public const int wall_hole = 29;

    //Death by 1499-1s inside its dimension
    public const int gp5_inside = 30;

    //Death by 1499-1s outside its dimension
    public const int gp5_outside = 31;

    //Killed by larry
    public const int larry_death = 32;

    //Slammed by the stones
    public const int pocket_stones = 33;

    //Fell to the Abyss
    public const int pocket_abyss_fall = 34;

    //Fastest fingers in the west MTF 
    public const int mtf_gsw_instant = 35;

    //Shot and bloodloss
    public const int mtf_gsw_bloodloss = 36;

    //Agent Ulgrin
    public const int ulgrin_death = 37;

    //Gate B helo
    public const int apache_gate_b = 38;

    //Gate A helo
    public const int apache_gate_a = 39;

    //Gate B guard.
    public const int agent_gate_b = 40;

    //Decontamination gas
    public const int decon_gas = 41;
    
    //Tesla gate
    public const int tesla_gate = 42;
}
