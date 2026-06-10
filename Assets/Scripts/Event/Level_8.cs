using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Level_8 : MonoBehaviour
{
    private PlayerController playerController;
    private RoomTransitionEffect roomTransitionEffect;

    [Header("Event 1")]
    [SerializeField] GameObject event1Container;
    [SerializeField] PlayableDirector openingCutsence;
    [SerializeField] float openingCutsenceLength;
    [SerializeField] Transform playerSpawnPos1;

    [Header("Event 2")]
    [SerializeField] GameObject event2Container;
    [SerializeField] GameObject[] triggersToDisable;
    [SerializeField] PlayableDirector interactWithDoorCutsence;
    [SerializeField] float interactWithDoorCutsenceDuration;
    [SerializeField] Transform playerSpawnPos2;

    [Header("Event 3")]
    [SerializeField] GameObject event3Container;
    [SerializeField] GameObject[] objectsToDisableEvent3;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        roomTransitionEffect = FindFirstObjectByType<RoomTransitionEffect>();
    }

    void Start()
    {
        openingCutsence.gameObject.SetActive(false);
        interactWithDoorCutsence.gameObject.SetActive(false);

        event1Container.SetActive(true);
        event2Container.SetActive(false);
        event3Container.SetActive(false);

        StartEvent1();
    }

    private void StartEvent1()
    {
        StartCoroutine(Event1Coroutine());
    }

    IEnumerator Event1Coroutine()
    {
        playerController.transform.position = playerSpawnPos1.position;

        playerController.gameObject.SetActive(false);

        openingCutsence.gameObject.SetActive(true);
        openingCutsence.Play();

        yield return new WaitForSeconds(openingCutsenceLength);

        openingCutsence.gameObject.SetActive(false);

        playerController.gameObject.SetActive(true);

        roomTransitionEffect.PlayRoomTransitionEffect();

        event1Container.SetActive(false);

        StartEvent2();
    }

    private void StartEvent2()
    {
        event2Container.SetActive(true);

        foreach (GameObject trigger in triggersToDisable)
        {
            trigger.SetActive(false);
        }
    }

    public void Event2_InteractWithDoor()
    {
        StartCoroutine(Event2_InteractWithDoorCoroutine());
    }

    IEnumerator Event2_InteractWithDoorCoroutine()
    {
        playerController.transform.position = playerSpawnPos2.position;

        playerController.gameObject.SetActive(false);

        interactWithDoorCutsence.gameObject.SetActive(true);
        interactWithDoorCutsence.Play();

        yield return new WaitForSeconds(interactWithDoorCutsenceDuration);

        foreach (GameObject trigger in triggersToDisable)
        {
            trigger.SetActive(true);
        }

        interactWithDoorCutsence.gameObject.SetActive(false);

        playerController.gameObject.SetActive(true);

        roomTransitionEffect.PlayRoomTransitionEffect();

        event2Container.SetActive(false);

        StartEvent3();
    }

    private void StartEvent3()
    {
        event3Container.SetActive(true);

        foreach (GameObject obj in objectsToDisableEvent3)
        {
            obj.SetActive(false);
        }
    }
}
