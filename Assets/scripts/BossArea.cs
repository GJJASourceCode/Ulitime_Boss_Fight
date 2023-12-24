
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    GameObject boss;
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = boss.transform.position;
    }
}
