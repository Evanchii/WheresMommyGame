using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Animator animator;
    VideoPlayer vid;

    public void Start()
    {
        animator = GetComponent<Animator>();
        vid = GetComponentInChildren<VideoPlayer>();
        vid.gameObject.SetActive(false) ;
    }

    public void PlayGame()
    {
        StartCoroutine(transition());
    }

    IEnumerator transition()
    {
        GameObject fade = GameObject.FindWithTag("visibility4"),
            play = GameObject.FindWithTag("visibility1"),
            exit = GameObject.FindWithTag("visibility2"),
            creds = GameObject.FindWithTag("visibility5");
        fade.SetActive(false);
        play.SetActive(false);
        creds.SetActive(false);
        exit.SetActive(false);
        animator.SetTrigger("triggerA");
        yield return new WaitForSeconds(2f);
        vid.gameObject.SetActive(true);
        yield return new WaitForSeconds((float)vid.length + 1f);
        fade.SetActive(true);
        vid.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void creds()
    {
        StartCoroutine(transCred());
    }

    IEnumerator transCred()
    {
        animator.SetTrigger("triggerA");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
