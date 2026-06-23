using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial1;
    public float tutorial1Duration;

    public GameObject tutorial2;
    public float tutorial2Duration;

    public GameObject tutorial3;
    public float tutorial3Duration;

    private void Start()
    {
        if (tutorial1 != null)
            tutorial1.SetActive(false);

        if (tutorial2 != null)
            tutorial2.SetActive(false);

        if (tutorial3 != null)
            tutorial3.SetActive(false);
    }

    public void ShowTutorial1()
    {
        StartCoroutine(ShowTutorial1Coroutine());
    }
    IEnumerator ShowTutorial1Coroutine()
    {
        tutorial1.SetActive(true);

        yield return new WaitForSeconds(tutorial1Duration);

        tutorial1.SetActive(false);
    }

    public void ShowTutorial2()
    {
        StartCoroutine(ShowTutorial2Coroutine());
    }
    IEnumerator ShowTutorial2Coroutine()
    {
        tutorial2.SetActive(true);

        yield return new WaitForSeconds(tutorial2Duration);

        tutorial2.SetActive(false);
    }

    public void ShowTutorial3()
    {
        StartCoroutine(ShowTutorial3Coroutine());
    }
    IEnumerator ShowTutorial3Coroutine()
    {
        tutorial3.SetActive(true);

        yield return new WaitForSeconds(tutorial3Duration);

        tutorial3.SetActive(false);
    }
}
