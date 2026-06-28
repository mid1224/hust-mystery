using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class VideoPlayerWebGLFix : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public string videoFileName = "smile-glitch.mp4"; // Include extension

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Dynamically get the path pointing to the StreamingAssets folder
        string videoPath = Path.Combine(Application.streamingAssetsPath, videoFileName);

        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = videoPath;

        // Optional: Pre-prepare the video
        videoPlayer.Prepare();
    }
}
