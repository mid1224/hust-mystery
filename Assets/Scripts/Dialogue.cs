using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance { get; private set; }

    [SerializeField] GameObject dialogueContainer;
    [SerializeField] TextMeshProUGUI dialogueText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        dialogueContainer.SetActive(false);
    }

    public void CreateDialogue(string text, float duration)
    {
        if (text == null || text.Trim() == "")
        {
            Debug.LogWarning("Dialogue text is empty.");
            return;
        }

        if (duration <= 0f)
        {
            Debug.LogWarning("Dialogue duration must >0.");
            return;
        }

        StartCoroutine(DisplayDialogue(text, duration));
    }

    public void CreateShortDialogue(string text)
    {
        StartCoroutine(DisplayDialogue(text, 2f));
    }

    public void CreateLongDialogue(string text)
    {
        StartCoroutine(DisplayDialogue(text, 4f));
    }

    IEnumerator DisplayDialogue(string text, float duration)
    {
        dialogueContainer.SetActive(true);

        dialogueText.text = text;

        yield return new WaitForSeconds(duration);

        dialogueContainer.SetActive(false);
    }
}