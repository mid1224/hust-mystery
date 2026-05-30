using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractTrigger : MonoBehaviour
{
    private InteractSystem interactSystem;

    [Header("Event")]
    [SerializeField] UnityEvent targetEvent;
    [SerializeField] bool destroyAfterTrigger;

    [Header("Require Items")]
    [SerializeField] bool requireItem;
    [SerializeField] string requiredItemName;
    [SerializeField] string successMessage;
    [SerializeField] string failureMessage;

    [Header("UI")]
    [SerializeField] Canvas popUpCanvas;

    private void Awake()
    {
        interactSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InteractSystem>();
    }

    void Start()
    {
        if (popUpCanvas != null)
        {
            popUpCanvas.enabled = false;
        }
    }

    public void TriggerEvent()
    {
        if (requireItem == true)
        {
            if (Inventory.Instance.HasItem(requiredItemName) == false)
            {
                Dialogue.Instance.CreateDialogue(failureMessage, 3f);
                return;
            }
            else
            {
                Dialogue.Instance.CreateDialogue(successMessage, 2f);
            }
        }

        targetEvent.Invoke();

        if (destroyAfterTrigger == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactSystem.SetCurrentTrigger(this);

            if (popUpCanvas != null)
            {
                popUpCanvas.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactSystem.SetCurrentTrigger(null);

            if (popUpCanvas != null)
            {
                popUpCanvas.enabled = false;
            }
        }
    }
}
