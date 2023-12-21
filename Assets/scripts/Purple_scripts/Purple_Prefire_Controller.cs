using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Prefire_Controller : MonoBehaviour
{
    Rigidbody rigid;
    GameObject nemo;
    Vector3 dir;
    void Awake(){
        rigid = GetComponent<Rigidbody>();
        nemo = GameObject.Find("mouth");
        dir = nemo.transform.TransformDirection(Vector3.forward*-10);
        Destroy(gameObject,1.5f);
        
    }
    

    void Update(){
        rigid.velocity = dir;
        
        

    }
   
    
}
