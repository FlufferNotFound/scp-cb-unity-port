using System.Collections;
using UnityEngine;
using UnityEngine.Video;

/*
   (Read 6000.4's on Video file compatibility)

   The videos for the startup are stored as AVIs, problem is .avi is only supported in windows, and after reading
   the only video codec supported by Windows/MacOS/Linux is VP8. The problem is that when I bundled the video and audio
   into a VP8 encoded webm, even with the lowest -crf 4, the video looked like shit, the compression artifacts and
   general looks of it were bad.

   VP9 looks way better than VP8 but Unity doesn't support it, and though I found some plugins in the asset store that add
   support for them, I can't afford them, so this stays until I find a way to add support for VP9.
    
-FlufferNotFound, 2026-06-08
   */

[System.Serializable]
public class StartUpVideoSequence
{
    [SerializeField]
    [Tooltip("The video clip to play at startup")]
    public VideoClip videoClip = null;

    [SerializeField]
    [Tooltip("The audio clip to play at startup. (Optional)")]
    public AudioClip audioClip = null;
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(VideoPlayer))]
public class StartUpVideoPlayer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Videos to play at startup.")]
    private StartUpVideoSequence[] videoSequences;

    [HideInInspector]
    private SceneController sceneController;

    [HideInInspector]
    private VideoPlayer videoPlayer;

    [HideInInspector]
    private AudioSource audioSource;

    [HideInInspector]
    private GatherInput gatherInput;

    private void Awake()
    {
        //Check if scene controller exists
        if (GameObject.Find("SceneController") == null)
        {
            Debug.LogError("No SceneController found on this scene. Make sure to add it and name it 'SceneController'.");
        }
        else
        {
            sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            if (sceneController == null) Debug.LogError("Found SceneController but it has no SceneController component.");
        }

        //skip to menu if there's no videos
        if (videoSequences.Length <= 0)
        {
            Debug.LogWarning("No videos on sequence. Loading menu.");
            sceneController.LoadSceneByName("Menu");
        }

        gatherInput = GetComponent<GatherInput>();
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null || videoPlayer == null)
        {
            Debug.LogError("VideoPlayer and AudioSource components are missing, please add them.");
        }

        //Component configuration
        audioSource.mute = false;
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.isLooping = false;
        videoPlayer.skipOnDrop = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(PlayVideoSequence());
    }

    private IEnumerator PlayVideoSequence()
    {
        for (int i = 0; i < videoSequences.Length; i++)
        {
            //Skip if no video on index
            if (videoSequences[i].videoClip == null)
            {
                //Fancier than i++ i guess. Didn't know this one existed
                continue;
            }

            videoPlayer.clip = videoSequences[i].videoClip;
            audioSource.clip = videoSequences[i].audioClip;

            //Prepare the video, so it's on buffer and doesn't stutter or freeze while playing.
            videoPlayer.Prepare();

            while (!videoPlayer.isPrepared)
            {
                yield return null;
            }

            videoPlayer.Play();  
            
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }

            while (videoPlayer.isPlaying)
            {
                yield return null;

                if (gatherInput.AnyKey)
                {
                    gatherInput.AnyKey = false;

                    break;
                }
            }

            videoPlayer.Stop();
            audioSource.Pause();
        }

        sceneController.LoadSceneByName("Menu");
    }

    private void OnDestroy()
    {
        videoPlayer.clip = null;
        audioSource.clip = null;

        StopCoroutine(PlayVideoSequence());
    }
}
