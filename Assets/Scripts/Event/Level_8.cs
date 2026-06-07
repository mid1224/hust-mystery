using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Level_8 : MonoBehaviour
{
    private PlayerController playerController;
    private RoomTransitionEffect roomTransitionEffect;
    private Dialogue dialogue;

    [Header("Event 1")]
    [SerializeField] PlayableDirector openingCutsence;
    [SerializeField] float openingCutsenceLength;

    [Header("Event 2")]
    [SerializeField] GameObject[] triggersToDisable;
    [SerializeField] PlayableDirector interactWithDoorCutsence;
    [SerializeField] float interactWithDoorCutsenceDuration;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        roomTransitionEffect = FindFirstObjectByType<RoomTransitionEffect>();
    }

    void Start()
    {
        StartEvent1();
    }

    private void StartEvent1()
    {
        StartCoroutine(Event1Coroutine());
    }

    IEnumerator Event1Coroutine()
    {
        playerController.gameObject.SetActive(false);

        openingCutsence.Play();

        yield return new WaitForSeconds(openingCutsenceLength);

        openingCutsence.gameObject.SetActive(false);

        playerController.gameObject.SetActive(true);

        roomTransitionEffect.PlayRoomTransitionEffect();

        StartEvent2();
    }

    private void StartEvent2()
    {
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
        playerController.gameObject.SetActive(false);

        interactWithDoorCutsence.Play();

        yield return new WaitForSeconds(interactWithDoorCutsenceDuration);

        foreach (GameObject trigger in triggersToDisable)
        {
            trigger.SetActive(true);
        }

        playerController.gameObject.SetActive(true);

        roomTransitionEffect.PlayRoomTransitionEffect();
    }
}
