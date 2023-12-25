using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Breath_Controller : MonoBehaviour
{
    public GameObject prefab;  
    
    void Update()
    {
        if(PurplePattern.readyfire)
        {
            
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(prefab,30f);

        }
    }

  
}
