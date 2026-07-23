using UnityEngine;

/*
 * NOTE:
 * the original scp:cb options menu has some settings that may not be applicable in Unity.
 */

public class GameVariables
{
    //Graphics settings
    public bool enableBumpMapping = true;

    public bool enableVSync = true;

    //question now is what AA should be used, no idea what AA og uses
    public bool antiAliasing = true;

    public bool roomLighting = true;

    //The og game handles gamma between a 0-200% range, default is 100%
    //but Unity handles gamma between a 0-3 range, default is 2.2f
    public float screenGamma = 2.2f;

    public float particleAmount = 1.0f;

    //Og handles texture LOD bias in steps of 0.8, 0.4, 0, -0.8, -0.4 2.0.
    //but unity handles texture LOD bias differently, so find a way to convert the og values to unity values
    public float textureLODBias = 0.0f;

    //pretty sure this one won't be applicable in Unity, keep here just in case
    public bool saveTexturesOnVRAM = true;

    //-----------------------------------------------------------------

    //Audio settings
    [Range(0.0f, 1.0f)]
    public float musicVolume = 1.0f;

    [Range(0.0f, 1.0f)]
    public float soundVolume = 1.0f;

    //in the og, when a sound isn't used after 5 seconds, it's released from memory but unity handles 
    //this stuff automatically, so this setting just sits here chilling out, what a life champ
    public bool soundAutoRelease = true;

    public bool enableUserTracks = true;

    public bool userTrackMode = true;

    //-----------------------------------------------------------------

    //Control settings
    public float mouseSensitivity = 1.0f;

    public bool invertMouseYAxis = false;

    public float mouseSmoothing = 0.0f;

    //Keybinds go here, no idea how to change them at runtime, so this section is empty for now


    //-----------------------------------------------------------------

    //Advanced settings

    public bool showHud = true;
    
    public bool enableConsole = true;
    
    public bool openConsoleOnError = true;
    
    public bool achievementPopups = true;
    
    public bool showFPS = true;
    
    public bool frameLimit = true;
    
    public int fpsLimit = 60;
    
    public bool antiAliasedText = true;

}
