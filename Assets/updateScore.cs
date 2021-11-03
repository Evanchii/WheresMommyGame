using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class updateScore : MonoBehaviour
{

    public GameObject tNote;
    private TriggerEscape triggerNote;
    //public GameObject counter;
    TextMeshProUGUI count;

    // Start is called before the first frame update
    void Start()
    {
        tNote = GameObject.FindWithTag("trigger1");
        triggerNote = tNote.GetComponent<TriggerEscape>();
        count = FindObjectOfType<TextMeshProUGUI>();
        //counter = GameObject.FindWithTag("counter1");
        //tmp = counter.gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Count: " + triggerNote.count);
        //Debug.Log("Selected: " + counter.gameObject.tag);
        count.SetText("Found " + triggerNote.count + " out of 5");
    }
}
