using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Area_Detect : MonoBehaviour
{
    public static int whichArea;
    public static bool inCenter;
    bool area1, area2, area3;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        whichArea = 0;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Ar1")
        {
            area1 = true;
        }
        if (col.gameObject.name == "Ar2")
        {
            area2 = true;
        }
        if (col.gameObject.name == "Ar3")
        {
            area3 = true;
        }
        if (col.gameObject.name=="CenterArea"){
            inCenter = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Ar1")
        {
            area1 = false;
        }
        if (col.gameObject.name == "Ar2")
        {
            area2 = false;
        }
        if (col.gameObject.name == "Ar3")
        {
            area3 = false;
        }
        if (col.gameObject.name=="CenterArea"){
            inCenter = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (area1)
        {
            whichArea = 1;
        }
        if (!area1 && area2)
        {
            whichArea = 2;
        }
        if (!area2 && area3)
        {
            whichArea = 3;
        }
        if (!area3)
        {
            whichArea = 4;
        }
        transform.position = boss.transform.position;
    }
}
