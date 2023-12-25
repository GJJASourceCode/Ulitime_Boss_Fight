using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePattern : MonoBehaviour
{
    public static bool isAttacking, isDeath;
    public static int monsterHealth;
    public static int state;
    public static bool readyfire;
    Animator anim;
    GameObject[] area;
    GameObject player;
    Rigidbody rigid;
    Vector3 currentVec;
    Vector3 backpos = new Vector3(0, 0.475f, 0);
    bool area1, area2, lookAtPlayer, run, getback,zeropos, isBeforeFly;
    Quaternion rotGoal;
    // set varieties

    void Awake()
    {
        isBeforeFly = false;
        monsterHealth = 500;
        area = new GameObject[3];
        area[0] = GameObject.Find("Head_collider");
        area[1] = GameObject.Find("L_Claw_collider");
        area[2] = GameObject.Find("R_Claw_collider");
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        state = 0;
        area1 = false;
        area2 = false;
        lookAtPlayer = false;
        run = false;
        choosePattern();
    }
    IEnumerator idle()
    {
        float time;
        yield return new WaitForSeconds(0.2f);
        lookAtPlayer = true;
        time = Random.Range(0f, 0.8f);
        yield return new WaitForSeconds(time);
        if (area1)
        {
            if (area2)
            {
                if(isBeforeFly){
                    state = Random.Range(1, 3);
                    isBeforeFly = false;
                }
                else{
                    state = Random.Range(1, 4);
                }
            }
            else
            {
                state = 4;
            }
        }
        else
        {
            state = 5;
        }
        lookAtPlayer = false;
        choosePattern();
    }
    IEnumerator basic_attack()
    {
        isAttacking = true;
        lookAtPlayer = true;
        area[0].SetActive(true);
        anim.SetTrigger("basic attack");
        yield return new WaitForSeconds(0.1f);
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.6f);
        //attack1Sound.Play();
        yield return new WaitForSeconds(0.1f);
        
        yield return new WaitForSeconds(0.2f);
        area[0].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
    IEnumerator claw_attack()
    {
        isAttacking = true;
        lookAtPlayer = true;
        area[1].SetActive(true);
        area[2].SetActive(true);
        anim.SetTrigger("claw attack");
        yield return new WaitForSeconds(0.1f);
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.6f);
        //attack1Sound.Play();
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.2f);
        area[1].SetActive(false);
        area[2].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
    IEnumerator fly_flame_attack()
    {
        isBeforeFly = true;
        anim.SetTrigger("take off");
        yield return new WaitForSeconds(2.867f);
        //jumpAttackSound1.Play();
        //readyfire = true;
        getback = true;
        isAttacking = true;
        readyfire = true;
        yield return new WaitForSeconds(3.267f);
        readyfire = false;
        readyfire = true;
        yield return new WaitForSeconds(3.267f);
        readyfire = false;        
        zeropos = true;
        //area[2].SetActive(true);
        //jumpAttackSound2.Play();
        readyfire = false;
        getback = false;
        isAttacking = false;
        yield return new WaitForSeconds(3.7f);
        zeropos = false;
        
        //area[2].SetActive(false);

        state = 0;
        choosePattern();
    }
    IEnumerator movetime()
    {
        yield return new WaitForSeconds(2f);
        state = 0;
        choosePattern();
    }
    IEnumerator trace()
    {
        //Debug.Log("trace작동");
        anim.SetTrigger("scream");
        //howlingSound.Play();
        //Debug.Log(run);
        run = false;
        yield return new WaitForSeconds(1.4f);
        anim.SetInteger("run", 1);
        //dashSound.Play();
        //area[3].SetActive(true);
        lookAtPlayer = true;
        run = true;
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.75f);
        //area[3].SetActive(false);
        anim.SetInteger("run", 0);
        run = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }

    void choosePattern()
    {
        if(!isDeath){
           switch (state)
        {
            case 0:
                StartCoroutine("idle"); //calm state
                break;
            case 1:
                StartCoroutine("basic_attack"); // headbutt 
                break;
            case 2:
                StartCoroutine("claw_attack"); // hot breath 
                break;
            case 3:
                StartCoroutine("fly_flame_attack"); // welcome to hell
                break;
            case 4:
                StartCoroutine("movetime"); // walk
                anim.SetInteger("walk", 1);
                break;
            case 5:
                StartCoroutine("trace"); // shorten distance
                break;
            default:
                break;
        } 
        }
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Area1")//바깥원
        {
            area1 = true;
            //Debug.Log("접근1");
        }
        if (col.gameObject.name == "Area2")//안쪽원
        {
            area2 = true;
            //Debug.Log("접근2");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Area1")
        {
            area1 = false;
        }
        if (col.gameObject.name == "Area2")
        {
            area2 = false;
        }
    }
    void Update()
    {
        Vector3 zero = new Vector3(0f, 0f, 0f);
        Debug.Log(isDeath);
        if(monsterHealth <= 0 && !isDeath)
        {
            state = 6;
            anim.SetTrigger("Mdeath");
            anim.SetInteger("Mdying",1);
            StopCoroutine("fly_flame_attack");
            getback = false;
            isAttacking = false;
            readyfire = false;
            zeropos = false;
        }
        if (area2 && state == 5)
        {
            StopCoroutine("trace");
            anim.SetInteger("run", 0);
            state = 0;
            choosePattern();
        }
        //Debug.Log(run);
        Vector3 dir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        if (run)
        {
            rigid.velocity = currentVec.normalized * 20.0f;
        }
        if (getback)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward * 20, 0.005f);
            

        }
        if(zeropos)
        {
            transform.position = Vector3.Lerp(transform.position, backpos, 0.004f); 
            //transform.position = backpos;
        }
        if (transform.position.y > 0.5)
        {
            this.transform.position = new Vector3(transform.position.x, 0.475f, transform.position.z);
        }
        if (transform.position.y < 0.41)
        {
            this.transform.position = new Vector3(transform.position.x, 0.475f, transform.position.z);
        }
        if (lookAtPlayer)
        {
            anim.SetInteger("walk", 1);
            rotGoal = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.07f);
        }
        else
        {
            anim.SetInteger("walk", 0);
        }

        if (state == 4)
        {
            lookAtPlayer = true;
            if (area2 == true)
            {
                StopCoroutine("movetime");
                lookAtPlayer = false;
                state = 0;
                choosePattern();
            }
            rigid.velocity = dir.normalized * 4.0f;

        }
        if (state == 0 || state == 1 || state == 2 || state == 3 || state == 6)
        {
            rigid.velocity = zero * 4.0f;
        }
        //Debug.Log(zeropos);
        if (monsterHealth <= 0 && !isDeath)
        {
            
            isDeath = true;
            monsterHealth = 0;
            
            
        }
    }

}

