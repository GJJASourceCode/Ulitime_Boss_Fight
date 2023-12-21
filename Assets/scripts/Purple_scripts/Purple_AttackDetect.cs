using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_AttackDetect : MonoBehaviour
{
    bool isHited;
    public AudioSource hit;
    void OnTriggerStay(Collider col)
    {
        if (Playermove.isSlashing && !isHited)
        {
            if (col.gameObject.tag == "DamageBox")
            {
                isHited = true;
                if (Playermove.slashNum == 1)
                {
                    PurplePattern.monsterHealth -= 32;
                    hit.Play();
                }
                else if (Playermove.slashNum == 2)
                {
                    PurplePattern.monsterHealth -= 42;
                    hit.Play();
                }
            }
            else if (col.gameObject.tag == "YackDamageBox")
            {
                isHited = true;
                if (Playermove.slashNum == 1)
                {
                    PurplePattern.monsterHealth -= 48;
                    hit.Play();
                }
                else if (Playermove.slashNum == 2)
                {
                    PurplePattern.monsterHealth -= 63;
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
        if (Playermove.isSlashing == false)
        {
            isHited = false;
        }
    }
}
