using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private InputSystem_Actions input;

    [SerializeField] InteractTrigger currentTrigger;

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
            if (input.Player.Interact.WasPressedThisFrame())
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
    }
}
