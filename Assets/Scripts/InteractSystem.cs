using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private InputSystem_Actions input;

    [SerializeField] InteractTrigger currentTrigger;
    [SerializeField] float interactCooldown = 0.5f;
    private float lastInteractTime = 0f;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        if (currentTrigger != null)
        {
            if (input.Player.Interact.WasPressedThisFrame() && CanInteract())
            {
                Interact();
            }
        }
    }

    public void SetCurrentTrigger(InteractTrigger trigger)
    {
        currentTrigger = trigger;
    }

    private void Interact()
    {
        currentTrigger.TriggerEvent();
        lastInteractTime = Time.time;
    }

    private bool CanInteract()
    {
        return Time.time >= lastInteractTime + interactCooldown;
    }
}
