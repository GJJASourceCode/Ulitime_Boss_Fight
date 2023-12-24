using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_breath : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if (Bluepattern.readyfire)
        {
            Debug.Log("breath");
            //Instantiate(prefab, transform.position, Quaternion.identity);
            //Destroy(prefab, 4f);

        }
    }


}

