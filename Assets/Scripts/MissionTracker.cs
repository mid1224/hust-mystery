using TMPro;
using UnityEngine;

public class MissionTracker : MonoBehaviour
{
    public static MissionTracker Instance { get; private set; }

    [SerializeField] GameObject container;
    [SerializeField] TextMeshProUGUI missionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep across scenes
    }

    public void SetCurrentMission(string missionName)
    {
        container.SetActive(true);
        missionText.text = missionName;
    }

    public void DisableMissionPanel()
    {
        container.SetActive(false);
    }
}
