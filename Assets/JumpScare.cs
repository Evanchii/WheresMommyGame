using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{

    public AudioSource scareAudioSource;
    public AudioClip scareSound;

    private bool hasPlayedAudio;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& hasPlayedAudio == false){
            scareAudioSource.PlayOneShot(scareSound);
            hasPlayedAudio = true;
        }
    }
}
