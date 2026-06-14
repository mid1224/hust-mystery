using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] bool key_UtilityRoom;
    [SerializeField] bool keycard;
    [SerializeField] bool flashlight;
    [SerializeField] bool key_Stair;
    [SerializeField] bool fuse1;
    [SerializeField] bool fuse2;
    [SerializeField] bool fuse3;

    // One bool per item. False = not in inventory, True = in inventory.

    public Flashlight flashlightObj;

    [Header("UI")]
    [SerializeField] GameObject key_UtilityRoom_Icon;
    [SerializeField] GameObject keycard_Icon;
    [SerializeField] GameObject flashlight_Icon;
    [SerializeField] GameObject key_Stair_Icon;
    [SerializeField] GameObject fuse1_Icon;
    [SerializeField] GameObject fuse2_Icon;
    [SerializeField] GameObject fuse3_Icon;

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

    void Start()
    {
        UpdateInventoryUI();

        flashlightObj.batterySlider.gameObject.SetActive(false);
    }

    public void AddItem(string itemName)
    {
        if (itemName == "Key_Stair")
        {
            key_Stair = true;
            key_Stair_Icon.SetActive(true);
        }
        if (itemName == "Flashlight")
        {
            flashlight = true;
            flashlight_Icon.SetActive(true);
            flashlightObj.batterySlider.gameObject.SetActive(true);
        }
        if (itemName == "Key_UtilityRoom")
        {
            key_UtilityRoom = true;
            key_UtilityRoom_Icon.SetActive(true);
        }
        if (itemName == "Keycard")
        {
            keycard = true;
            keycard_Icon.SetActive(true);
        }
        if (itemName == "Fuse1")
        {
            fuse1 = true;
            fuse1_Icon.SetActive(true);
        }
        if (itemName == "Fuse2")
        {
            fuse2 = true;
            fuse2_Icon.SetActive(true);
        }
        if (itemName == "Fuse3")
        {
            fuse3 = true;
            fuse3_Icon.SetActive(true);
        }

        UpdateInventoryUI();
    }

    public void RemoveItem(string itemName)
    {
        if (itemName == "Key_Stair")
        {
            key_Stair = false;
            key_Stair_Icon.SetActive(false);
        }
        if (itemName == "Flashlight")
        {
            flashlight = false;
            flashlight_Icon.SetActive(false);
            flashlightObj.batterySlider.gameObject.SetActive(false);
        }
        if (itemName == "Keycard")
        {
            keycard = false;
            keycard_Icon.SetActive(false);
        }
        if (itemName == "Key_UtilityRoom")
        {
            key_UtilityRoom = false;
            key_UtilityRoom_Icon.SetActive(false);
        }
        if (itemName == "Fuse1")
        {
            fuse1 = false;
            fuse1_Icon.SetActive(false);
        }
        if (itemName == "Fuse2")
        {
            fuse2 = false;
            fuse2_Icon.SetActive(false);
        }
        if (itemName == "Fuse3")
        {
            fuse3 = false;
            fuse3_Icon.SetActive(false);
        }

        UpdateInventoryUI();
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
        if (itemName == "Fuse1")
        {
            return fuse1;
        }
        if (itemName == "Fuse2")
        {
            return fuse2;
        }
        if (itemName == "Fuse3")
        {
            return fuse3;
        }

        return false;
    }

    private void UpdateInventoryUI()
    {
        key_Stair_Icon.SetActive(key_Stair);
        flashlight_Icon.SetActive(flashlight);
        keycard_Icon.SetActive(keycard);
        key_UtilityRoom_Icon.SetActive(key_UtilityRoom);
        fuse1_Icon.SetActive(fuse1);
        fuse2_Icon.SetActive(fuse2);
        fuse3_Icon.SetActive(fuse3);
    }
}
