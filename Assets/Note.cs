using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{

    public Image noteImage;
    public GameObject NoteHideBtn;

    public AudioClip pickupSound;
    public AudioClip putawaySound;
    // Start is called before the first frame update
    void Start()
    {
        noteImage.enabled=false;
        NoteHideBtn.SetActive(false);
    }

    // Update is called once per frame

public void ShowImage(){
    noteImage.enabled = true;
    GetComponent<AudioSource>().PlayOneShot(pickupSound);
    NoteHideBtn.SetActive(true);
        Time.timeScale = 0;
    }
public void HideImage(){
    noteImage.enabled=false;
    GetComponent<AudioSource>().PlayOneShot(putawaySound);
     NoteHideBtn.SetActive(false);
        Time.timeScale = 1;
}
}
