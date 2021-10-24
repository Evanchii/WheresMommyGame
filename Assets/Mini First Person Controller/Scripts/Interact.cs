using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        if(interactIcon != null){
            interactIcon.enabled=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,interactDistance, interactLayer)){
            
            if(isInteracting==false){

                if(interactIcon != null){
                interactIcon.enabled=true;
                }
                if(TCKInput.GetAction("interact",EActionEvent.Down)){
                    Debug.Log("interact");
                    if(hit.collider.CompareTag("Door")){
                        Debug.Log("Door");
                        hit.collider.GetComponent<Door>().ChangerDoorState();
                    }
                }
            }

        }
    }
}
