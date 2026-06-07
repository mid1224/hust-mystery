using TMPro;
using UnityEngine;

public class MissionTracker : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] TextMeshProUGUI missionText;

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
