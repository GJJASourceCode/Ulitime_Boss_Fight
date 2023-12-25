using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purple_phealthbar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Purple_Playermove.health / 206.1f, 0.83f, 1);
    }
}
