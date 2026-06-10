using TMPro;
using UnityEngine;

public class Level_8_Password : MonoBehaviour
{
    [SerializeField] TMP_InputField textInput;
    [SerializeField] string correctPassword;

    [SerializeField] GameObject triggerToDisable;
    [SerializeField] GameObject triggerToEnable;

    [SerializeField] PlayerController playerController;

    public void CheckPassword()
    {
        if (textInput.text == correctPassword)
        {
            UnlockDoor();
        }
        else
        {
            Dialogue.Instance.CreateDialogue("Sai", 0.75f);
        }
    }

    private void UnlockDoor()
    {
        Dialogue.Instance.CreateDialogue("Mở được rồi!", 1f);

        triggerToDisable.SetActive(false);
        triggerToEnable.SetActive(true);

        gameObject.SetActive(false);
    }
}
