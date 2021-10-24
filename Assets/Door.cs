using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    public bool open =false;
    void Start()
    {
        _animator=GetComponent<Animator>();
    }

public void ChangerDoorState(){
    Debug.Log("Open/Close");
    open =! open;
}
// void OnTriggerEnter(Collider other)
// {
    
//         _animator.SetBool("open",true);
   
    
// }
// void OnTriggerExit(Collider other)
// {
//     _animator.SetBool("open",false);
// }
    // Update is called once per frame
    void Update()
    {
        if(open){
             _animator.SetBool("open",true);
        }else{
            _animator.SetBool("open",false);
        }
        
    }
}
