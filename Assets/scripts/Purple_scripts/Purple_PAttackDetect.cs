using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_PAttackDetect : MonoBehaviour
{
    void Start()
    {
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "AttackRange")
        {
            if (PurplePattern.isAttacking && !Playermove.isRoll && !Playermove.isDeath)
            {
                switch (PurplePattern.state)
                {
                    case 1:
                        Playermove.health -= 17;
                        break;
                    case 2:
                        Playermove.health -= 17;
                        break;
                    case 3:
                        Playermove.health -= 5;
                        break;
                    default:
                        break;
                }
            }

        }
    }
    void Update()
    {
        Debug.Log(Playermove.health);
    }
    
   
}
