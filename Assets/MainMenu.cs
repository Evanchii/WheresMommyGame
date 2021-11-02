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
        GameObject fade = GameObject.FindWithTag("Fade"),
            play = GameObject.FindWithTag("btnPlay"),
            exit = GameObject.FindWithTag("btnExit");
        fade.SetActive(false);
        play.SetActive(false);
        exit.SetActive(false);
        animator.SetTrigger("triggerA");
        yield return new WaitForSeconds(2f);
        vid.gameObject.SetActive(true);
        Debug.Log("Video is Active: " + vid.gameObject.activeInHierarchy);
        yield return new WaitForSeconds((float)vid.length + 1f);
        fade.SetActive(true);
        vid.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
