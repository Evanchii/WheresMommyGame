using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{

    public Image noteImage;

    public AudioClip pickupSound;
    public AudioClip putawaySound;
    // Start is called before the first frame update
    void Start()
    {
        noteImage.enabled=false;
    }

    // Update is called once per frame

public void ShowImage(){
    noteImage.enabled = true;
    GetComponent<AudioSource>().PlayOneShot(pickupSound);
}
public void HideImage(){
    noteImage.enabled=false;
    GetComponent<AudioSource>().PlayOneShot(putawaySound);
}
}
