using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purple_phealthtext : MonoBehaviour
{
    public Text healthTxt;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        healthTxt.text = Purple_Playermove.health.ToString();
    }
}
