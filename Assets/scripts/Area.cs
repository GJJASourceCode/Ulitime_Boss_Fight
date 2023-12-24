
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
