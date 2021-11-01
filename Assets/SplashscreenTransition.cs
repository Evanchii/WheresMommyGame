using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashscreenTransition : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(splashscreen());
    }

    IEnumerator splashscreen()
    {
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("triggerA");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("triggerB");
    }

    public void accept()
    {
        StartCoroutine(transition());
    }

    IEnumerator transition()
    {
        animator.SetTrigger("triggerC");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
