using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] UnityEvent targetEvent;
    [SerializeField] bool destroyAfterTrigger;

    [Header("Require Items")]
    [SerializeField] bool requireItem;
    [SerializeField] string requiredItemName;

    [Header("UI")]
    [SerializeField] Canvas popUpCanvas;

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
                return;
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
            if (popUpCanvas != null)
            {
                popUpCanvas.enabled = true;
            }

            targetEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (popUpCanvas != null)
            {
                popUpCanvas.enabled = false;
            }

            targetEvent.Invoke();
        }
    }
}
