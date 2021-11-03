using System.Collections;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{

    public string interactButton;

    public float interactDistance = 3f;

    public LayerMask interactLayer;

    public Image interactIcon;

    public bool isInteracting;

    public GameObject tNote;
    private TriggerEscape triggerNote;

    // Start is called before the first frame update
    void Start()
    {
        if(interactIcon != null){
            interactIcon.enabled=false;
        }
        tNote = GameObject.FindWithTag("trigger1");
        triggerNote = tNote.GetComponent<TriggerEscape>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,interactDistance, interactLayer)){
            //Debug.Log("see");
            if(isInteracting==false){

                if(interactIcon != null){
                interactIcon.enabled=true;
                }

                if(TCKInput.GetAction("interact",EActionEvent.Down)){
                    //Debug.Log("interact");
                    if(hit.collider.CompareTag("Door")){
                        //Debug.Log("Note");
                        hit.collider.GetComponent<Door>().ChangerDoorState();
                    }
                    // else
                     if(hit.collider.CompareTag("Note")){
                        //Debug.Log("Note");
                        if (hit.collider.gameObject.name == "Note1")
                        {
                            if (!triggerNote.note1)
                                triggerNote.count += 1;
                            triggerNote.note1 = true;
                        }
                        if (hit.collider.gameObject.name == "Note2")
                        {
                            if (!triggerNote.note2)
                                triggerNote.count += 1;
                            triggerNote.note2 = true;
                        }
                        if (hit.collider.gameObject.name == "Note3")
                        {
                            if (!triggerNote.note3)
                                triggerNote.count += 1;
                            triggerNote.note3 = true;
                        }
                        if (hit.collider.gameObject.name == "Note4")
                        {
                            if (!triggerNote.note4)
                                triggerNote.count += 1;
                            triggerNote.note4 = true;
                        }
                        if (hit.collider.gameObject.name == "Note5")
                        {
                            if (!triggerNote.note5)
                                triggerNote.count += 1;
                            triggerNote.note5 = true;
                        }

                        hit.collider.GetComponent<Note>().ShowImage();
                    }
                }
            }

        }
    }
}
