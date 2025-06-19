using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionController : MonoBehaviour
{
    public Material transitionMaterial;
    public string nextScene = "Test3";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(DoTransition());
    }

    IEnumerator DoTransition()
    {
        float t = -1f;
        while (t < 1f)
        {
            t += Time.deltaTime; 
            transitionMaterial.SetFloat("_TransitionPara", t);
            yield return null;
        }
        SceneManager.LoadScene(nextScene);
    }
}
