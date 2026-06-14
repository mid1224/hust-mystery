using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class Stage2 : MonoBehaviour
{
    private PlayerController playerController;
    private RoomTransitionEffect roomTransitionEffect;

    [Header("Event1")]
    [SerializeField] GameObject event1Container;
    [SerializeField] PlayableDirector openingCutsence;
    [SerializeField] float openingCutsenceLength;
    [SerializeField] Transform playerSpawnPos1;
    [SerializeField] GameObject[] objectsToDisableEvent1;

    [Header("Event2")]
    [SerializeField] GameObject interactElectricalBoxTrigger;
    [SerializeField] int fuseLeft = 3;
    [SerializeField] GhostSpawner ghostSpawner;

    [Header("Event3")]
    [SerializeField] GameObject event3Container;
    [SerializeField] PlayableDirector lightOnCutscene;
    [SerializeField] PlayableDirector insideElevatorCutscene;
    [SerializeField] GameObject tryElevatorTrigger;
    [SerializeField] GameObject enterElevatorTrigger;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        roomTransitionEffect = FindFirstObjectByType<RoomTransitionEffect>();
    }

    void Start()
    {
        openingCutsence.gameObject.SetActive(false);
        //lightOnCutscene.gameObject.SetActive(false);
        //insideElevatorCutscene.gameObject.SetActive(false);

        event1Container.SetActive(true);
        event3Container.SetActive(false);

        fuseLeft = 3;
        ghostSpawner.isSpawning = false;

        StartEvent1();
    }

    private void StartEvent1()
    {
        StartCoroutine(Event1Coroutine());
    }

    IEnumerator Event1Coroutine()
    {
        foreach (GameObject obj in objectsToDisableEvent1)
        {
            obj.SetActive(false);
        }

        playerController.transform.position = playerSpawnPos1.position;

        playerController.gameObject.SetActive(false);

        openingCutsence.gameObject.SetActive(true);
        openingCutsence.Play();

        yield return new WaitForSeconds(openingCutsenceLength);

        openingCutsence.gameObject.SetActive(false);

        playerController.gameObject.SetActive(true);

        roomTransitionEffect.PlayRoomTransitionEffect();

        event1Container.SetActive(false);

        MissionTracker.Instance.SetCurrentMission("Tìm cách khôi phục nguồn điện.");

        foreach (GameObject obj in objectsToDisableEvent1)
        {
            obj.SetActive(true);
        }

        StartEvent2();
    }

    private void StartEvent2()
    {
        ghostSpawner.isSpawning = true;
    }

    public void InteractWithElectricalBox()
    {
        if (Inventory.Instance.HasItem("Fuse1"))
        {
            fuseLeft--;
            Inventory.Instance.RemoveItem("Fuse1");

            if (fuseLeft > 0)
            {
                Dialogue.Instance.CreateShortDialogue($"Đã gắn cầu chì. Còn cần {fuseLeft} chiếc.");
            }
            else
            {
                StartEvent3();
            }

            return;
        }

        if (Inventory.Instance.HasItem("Fuse2"))
        {
            fuseLeft--;
            Inventory.Instance.RemoveItem("Fuse2");

            if (fuseLeft > 0)
            {
                Dialogue.Instance.CreateShortDialogue($"Đã gắn cầu chì. Còn cần {fuseLeft} chiếc.");
            }
            else
            {
                StartEvent3();
            }

            return;
        }

        if (Inventory.Instance.HasItem("Fuse3"))
        {
            fuseLeft--;
            Inventory.Instance.RemoveItem("Fuse3");

            if (fuseLeft > 0)
            {
                Dialogue.Instance.CreateShortDialogue($"Đã gắn cầu chì. Còn cần {fuseLeft} chiếc.");
            }
            else
            {
                StartEvent3();
            }

            return;
        }

        Dialogue.Instance.CreateShortDialogue($"Mình cần {fuseLeft} chiếc cầu chì để gắn vào đây.");
    }

    private void StartEvent3()
    {
        interactElectricalBoxTrigger.SetActive(false);

        tryElevatorTrigger.SetActive(false);
        enterElevatorTrigger.SetActive(true);
    }
}
