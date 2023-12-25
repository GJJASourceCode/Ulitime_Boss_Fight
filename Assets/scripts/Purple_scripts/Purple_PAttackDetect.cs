using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_PAttackDetect : MonoBehaviour
{
    void Start() { }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "AttackRange")
        {
            if (
                PurplePattern.isAttacking && !Purple_Playermove.isRoll && !Purple_Playermove.isDeath
            )
            {
                switch (PurplePattern.state)
                {
                    case 1:
                        Purple_Playermove.health -= 18;
                        break;
                    case 2:
                        Purple_Playermove.health -= 10;
                        break;
                    case 3:
                        Purple_Playermove.health -= 5;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void Update() { }
}
