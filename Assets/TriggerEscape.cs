using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEscape : MonoBehaviour
{
    GameObject vid;
    public bool note1 = false, note2 = false, note3 = false, note4 = false, note5 = false;

    // Start is called before the first frame update
    void Start()
    {
        vid = GameObject.FindWithTag("visibility6");
        vid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (note1 && note2 && note3 && note4 && note5/*note*/)
            {
                GameObject Touchpad = GameObject.FindWithTag("visibility1"),
                    Joystick = GameObject.FindWithTag("visibility2"),
                    firebtn = GameObject.FindWithTag("visibility3"),
                    interactbtn = GameObject.FindWithTag("visibility4");

                Touchpad.SetActive(false);
                Joystick.SetActive(false);
                firebtn.SetActive(false);
                interactbtn.SetActive(false);

                //GameObject enemy = GameObject.FindWithTag("enemy");
                //enemy.SetActive(false);
                vid.SetActive(true);
            }
        }
    }
}
