using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_FrontJump : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject boss;
    void Awake()
    {
        boss = GameObject.Find("Green");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(boss.transform.position.x/2, 0.475f, boss.transform.position.z/2);
    }
}
