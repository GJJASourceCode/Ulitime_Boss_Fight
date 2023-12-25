using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_AttackDetect : MonoBehaviour
{
    public static bool isHited;

    void OnTriggerEnter(Collider col)
    {
        if (Purple_Playermove.isSlashing && !isHited)
        {
            if (col.gameObject.tag == "DamageBox")
            {
                isHited = true;
                if (Purple_Playermove.slashNum == 1)
                {
                    PurplePattern.monsterHealth -= 16;
                }
                else if (Purple_Playermove.slashNum == 2)
                {
                    PurplePattern.monsterHealth -= 30;
                }
            }
            else if (col.gameObject.tag == "YackDamageBox")
            {
                isHited = true;
                if (Purple_Playermove.slashNum == 1)
                {
                    PurplePattern.monsterHealth -= 40;
                }
                else if (Purple_Playermove.slashNum == 2)
                {
                    PurplePattern.monsterHealth -= 50;
                }
            }

        }
    }
    void Start()
    {

    }

    // Update is called once per frame
}
