using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_AttackDetect : MonoBehaviour
{
    bool isHited;
    public AudioSource hit;

    void OnTriggerStay(Collider col)
    {
        if (Purple_Playermove.isSlashing && !isHited)
        {
            if (col.gameObject.tag == "DamageBox")
            {
                isHited = true;
                if (Purple_Playermove.slashNum == 1)
                {
                    PurplePattern.monsterHealth -= 16;
                    hit.Play();
                }
                else if (Purple_Playermove.slashNum == 2)
                {
                    PurplePattern.monsterHealth -= 30;
                    hit.Play();
                }
            }
            else if (col.gameObject.tag == "YackDamageBox")
            {
                isHited = true;
                if (Purple_Playermove.slashNum == 1)
                {
                    PurplePattern.monsterHealth -= 40;
                    hit.Play();
                }
                else if (Purple_Playermove.slashNum == 2)
                {
                    PurplePattern.monsterHealth -= 50;
                    hit.Play();
                }
            }

        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Purple_Playermove.isSlashing == false)
        {
            isHited = false;
        }
        
    }
}
