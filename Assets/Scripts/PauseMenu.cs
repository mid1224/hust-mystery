using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] AudioListener audioListener;
    public bool isPausing;

    public void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isPausing = !isPausing;

            if (isPausing == true)
            {
                Time.timeScale = 0;
                audioListener.enabled = false;
                container.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                audioListener.enabled = true;
                container.SetActive(false);
            }
        }
    }

    public void Continue()
    {
        isPausing = false;
        Time.timeScale = 1;
        audioListener.enabled = true;
        container.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
