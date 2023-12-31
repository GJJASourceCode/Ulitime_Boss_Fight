using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluepattern : MonoBehaviour
{
    public static bool isAttacking, isDeath;
    public static int monsterHealth;
    public static int state;
    public static bool readyfire;
    public GameObject player, victory;
    Animator anim;
    GameObject[] area;
    Rigidbody rigid;
    Vector3 currentVec;
    Vector3 backpos = new Vector3(0, 0.475f, 0);
    bool area1, area2, lookAtPlayer, run, getback;
    Quaternion rotGoal;

    void Awake()
    {
        monsterHealth = 500;
        area = new GameObject[5];
        area[0] = GameObject.Find("Chest_collider");
        area[1] = GameObject.Find("Head_collider");
        area[2] = GameObject.Find("Tongue01_collider");
        area[3] = GameObject.Find("Middle01_L_collider");
        area[4] = GameObject.Find("Middle01_R_collider");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        state = 0;
        area1 = false;
        area2 = false;
        lookAtPlayer = false;
        run = false;
        state = 0;
        choosePattern();
    }
    IEnumerator idle01()
    {
        float time;
        yield return new WaitForSeconds(0.2f);
        lookAtPlayer = true;
        time = Random.Range(0f, 0.8f);
        yield return new WaitForSeconds(time);
        Debug.Log("sd");
        if (area1)
        {
            if (area2)
            {
                state = Random.Range(1, 3);
            }
            else
            {
                state = 3;
            }
        }
        else
        {
            state = 4;
        }
        lookAtPlayer = false;
        choosePattern();
    }
    IEnumerator attack1()
    {
        anim.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.7f);
        //attack1Sound.Play();
        yield return new WaitForSeconds(0.1f);
        area[0].SetActive(true);
        area[1].SetActive(true);
        area[2].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
    IEnumerator attack2()
    {
        anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(0.7f);
        //attack1Sound.Play();
        yield return new WaitForSeconds(0.1f);
        area[0].SetActive(true);
        area[1].SetActive(true);
        area[2].SetActive(true);
        area[3].SetActive(true);
        area[4].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        area[3].SetActive(false);
        area[4].SetActive(false);
        yield return new WaitForSeconds(0.5f);
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
        Debug.Log("trace");
        anim.SetTrigger("scream");
        //howlingSound.Play();
        yield return new WaitForSeconds(3f);
        anim.SetInteger("run", 1);
        //dashSound.Play();
        //area[3].SetActive(true);
        isAttacking = true;
        run = true;
        lookAtPlayer = true;
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.5f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.5f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.5f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.5f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.75f);
        //area[3].SetActive(false);
        isAttacking = false;
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
                StartCoroutine("idle01"); //calm
                break;
            case 1:
                StartCoroutine("attack1"); // bite
                break;
            case 2:
                StartCoroutine("attack2"); // swoop
                break;
            case 3:
                StartCoroutine("movetime"); // walk
                anim.SetInteger("walk", 1);
                break;
            case 4:
                StartCoroutine("trace"); // rush
                break;
            default:
                break;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Area1")//�ٱ���
        {
            area1 = true;
            //Debug.Log("����1");
        }
        if (col.gameObject.name == "Area2")//���ʿ�
        {
            area2 = true;
            //Debug.Log("����2");
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
        /*if (monsterHealth <= 0 && !isDeath)
        {
            isDeath = true;
            state = 6;
            monsterHealth = 0;
            anim.SetTrigger("death");
            anim.SetInteger("dying", 1);
            StartCoroutine("Diiie");
        }
        if (isDeath)
        {
            monsterHealth = 0;
        }*/
        if (area2 && state == 4)
        {
            StopCoroutine("trace");
            lookAtPlayer = false;
            isAttacking = false;
            anim.SetInteger("run", 0);
            run = false;
            state = 0;
            choosePattern();
        }
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

        if (state == 3)
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
        if (state == 0 || state == 1 || state == 2)
        {
            rigid.velocity = zero * 4.0f;
        }
    }

}


