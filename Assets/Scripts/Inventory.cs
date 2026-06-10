using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] bool key_UtilityRoom;
    [SerializeField] bool keycard;
    [SerializeField] bool flashlight;
    [SerializeField] bool key_Stair;

    // One bool per item. False = not in inventory, True = in inventory.

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep inventory across scenes
    }

    public void AddItem(string itemName)
    {
        if (itemName == "Key_StairToSeventhFloor")
        {
            key_Stair = true;
        }
        if (itemName == "Flashlight")
        {
            flashlight = true;
        }
        if (itemName == "Key_UtilityRoom")
        {
            key_UtilityRoom = true;
        }
        if (itemName == "Keycard")
        {
            keycard = true;
        }

    }

    public void RemoveItem(string itemName)
    {
        if (itemName == "Key_Stair")
        {
            key_Stair = false;
        }
        if (itemName == "Flashlight")
        {
            flashlight = false;
        }
        if (itemName == "Keycard")
        {
            keycard = false;
        }
        if (itemName == "Key_UtilityRoom")
        {
            key_UtilityRoom = false;
        }
    }

    public bool HasItem(string itemName)
    {
        if (itemName == "Key_Stair")
        {
            return key_Stair;
        }
        if (itemName == "Flashlight")
        {
            return flashlight;
        }
        if (itemName == "Keycard")
        {
            return keycard;
        }
        if (itemName == "Key_UtilityRoom")
        {
            return key_UtilityRoom;
        }

        return false;
    }
}
