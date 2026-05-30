using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] bool key_stairToSeventhFloor;
    [SerializeField] bool flashlight;

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
            key_stairToSeventhFloor = true;
        }
        if (itemName == "Flashlight")
        {
            flashlight = true;
        }
        // 
    }

    public void RemoveItem(string itemName)
    {
        if (itemName == "Key_StairToSeventhFloor")
        {
            key_stairToSeventhFloor = false;
        }
        if (itemName == "Flashlight")
        {
            flashlight = false;
        }
    }

    public bool HasItem(string itemName)
    {
        if (itemName == "Key_StairToSeventhFloor")
        {
            return key_stairToSeventhFloor;
        }
        if (itemName == "Flashlight")
        {
            return flashlight;
        }

        return false;
    }
}
