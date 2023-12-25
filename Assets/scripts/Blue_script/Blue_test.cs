using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_test : MonoBehaviour
{
    GameObject mouth;
    void Start()
    {
        mouth = GameObject.FindGameObjectWithTag("Tongue01");
        transform.position = mouth.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mouth.transform.position;
    }
}