using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;

public class Stage3 : MonoBehaviour
{
    private PlayerController playerController;
    private RoomTransitionEffect roomTransitionEffect;

    [SerializeField] GameObject event1Container;
    [SerializeField] PlayableDirector openingCutsence;
    [SerializeField] float openingCutsenceLength;
    [SerializeField] Transform playerSpawnPos1;

    [SerializeField] Light2D globalLight;

    [SerializeField] GameObject flashlightTutorial;

    [SerializeField] AudioSource music;

    [SerializeField] GameObject finalBoss;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        roomTransitionEffect = FindFirstObjectByType<RoomTransitionEffect>();
    }

    void Start()
    {
        openingCutsence.gameObject.SetActive(false);

        event1Container.SetActive(true);

        OpeningCutsence();

        Inventory.Instance.AddItem("Flashlight");

        finalBoss.SetActive(false);
    }

    public void OpeningCutsence()
    {
        StartCoroutine(OpeningCutsenceCoroutine());
    }

    IEnumerator OpeningCutsenceCoroutine()
    {
        playerController.transform.position = playerSpawnPos1.position;

        playerController.gameObject.SetActive(false);

        openingCutsence.gameObject.SetActive(true);
        openingCutsence.Play();

        yield return new WaitForSeconds(openingCutsenceLength);

        openingCutsence.gameObject.SetActive(false);

        playerController.gameObject.SetActive(true);

        event1Container.SetActive(false);

        MissionTracker.Instance.SetCurrentMission("CHẠY!");

        music.Play();

        finalBoss.SetActive(true);
    }

    public void LightsOff()
    {
        globalLight.intensity = 0;

        ShowFlashlightTutorialTemp();
    }

    public void LightsOn()
    {
        globalLight.intensity = 0.75f;
    }

    public void ShowFlashlightTutorialTemp()
    {
        StartCoroutine(ShowFlashlightTutorialCoroutine());
    }
    IEnumerator ShowFlashlightTutorialCoroutine()
    {
        flashlightTutorial.SetActive(true);

        yield return new WaitForSeconds(2f);

        flashlightTutorial.SetActive(false);
    }
}
