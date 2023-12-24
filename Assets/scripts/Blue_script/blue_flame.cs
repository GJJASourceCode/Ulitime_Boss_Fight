using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_flame : MonoBehaviour
{
    Rigidbody rigid;
    GameObject nemo;
    Vector3 dir;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nemo = GameObject.Find("Tongue01");
        dir = nemo.transform.TransformDirection(Vector3.forward * -10);
        Destroy(gameObject, 2.5f);

    }


    void Update()
    {
        rigid.velocity = dir;



    }


}
