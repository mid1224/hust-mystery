using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Time.timeScale = 1f; // Ensure time scale is reset when loading a new scene

        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
