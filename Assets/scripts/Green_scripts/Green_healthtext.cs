using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Green_healthtext : MonoBehaviour
{
    public Text healthTxt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthTxt.text = GreenPattern.monsterHealth.ToString();
    }
}
