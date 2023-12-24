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
    bool area1, area2, lookAtPlayer, run, getback;
    Quaternion rotGoal;
    // set varieties

    void Awake()
    {

        monsterHealth = 500;
        area = new GameObject[2];
        area[0] = GameObject.Find("Claw_collider");
        area[1] = GameObject.Find("Head_collider");
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
                state = Random.Range(1, 4);
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
        anim.SetTrigger("basic attack");
        yield return new WaitForSeconds(0.7f);
        //attack1Sound.Play();
        yield return new WaitForSeconds(0.1f);
        area[0].SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        area[0].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
    IEnumerator claw_attack()
    {
        anim.SetTrigger("claw attack");
        yield return new WaitForSeconds(0.7f);
        //attack1Sound.Play();
        yield return new WaitForSeconds(0.1f);
        area[1].SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        area[1].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
    IEnumerator fly_flame_attack()
    {
        anim.SetTrigger("take off");
        yield return new WaitForSeconds(2.867f);
        //jumpAttackSound1.Play();
        //readyfire = true;
        getback = true;
        isAttacking = true;
        readyfire = true;
        yield return new WaitForSeconds(3.267f);
        //area[2].SetActive(true);
        //jumpAttackSound2.Play();
        readyfire = false;
        getback = false;
        isAttacking = false;
        yield return new WaitForSeconds(3.7f);
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
        Debug.Log("trace작동");
        anim.SetTrigger("scream");
        //howlingSound.Play();
        yield return new WaitForSeconds(1.4f);
        anim.SetInteger("run", 1);
        //dashSound.Play();
        //area[3].SetActive(true);
        run = true;
        lookAtPlayer = true;
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
        if (area2 && state == 5)
        {
            StopCoroutine("trace");
            anim.SetInteger("run", 0);
            state = 0;
            choosePattern();
        }
        //Debug.Log(run);
        Vector3 dir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        Vector3 zero = new Vector3(0f, 0f, 0f);
        if (run)
        {
            rigid.velocity = currentVec.normalized * 20.0f;
        }
        if (getback)
        {
            transform.position = Vector3.Lerp(transform.position, backpos, 0.002f);

        }
        if (transform.position.y > 0.5)
        {
            this.transform.position = new Vector3(0, 0.475f, 0);
        }
        if (transform.position.y < 0.41)
        {
            this.transform.position = new Vector3(0, 0.475f, 0);
        }
        if (lookAtPlayer)
        {
            anim.SetInteger("walk", 1);
            rotGoal = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.008f);
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
        if (state == 0 || state == 1 || state == 2 || state == 3)
        {
            rigid.velocity = zero * 4.0f;
        }
    }

}

