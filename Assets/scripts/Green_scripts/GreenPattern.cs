using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPattern : MonoBehaviour
{
    public AudioSource attack1Sound, attack2Sound, jumpAttackSound1, jumpAttackSound2, howlingSound, dashSound;
    public static bool isAttacking, isDeath;
    public static int monsterHealth;
    public static int state;
    Animator anim;
    GameObject[] area;
    public GameObject player, victory, BackJumpPos, FrontJumpPos, DustParticle;
    Rigidbody rigid;
    Vector3 currentVec, BackJumpVec, FrontJumpVec;
    bool lookAtPlayer, run, isBackJump, isMove, isFrontJump;
    Quaternion rotGoal;

    int isRight;


    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        isAttacking = false;
        isDeath = false;
        isBackJump = false;
        isFrontJump = false;
        monsterHealth = 1000;
        BackJumpPos = GameObject.Find("BackJumpPosition");
        DustParticle = GameObject.Find("DustParticle");
        FrontJumpPos = GameObject.Find("FrontJumpPosition");
        area = new GameObject[5];
        area[0] = GameObject.Find("Hand_collider");//손 공격범위
        area[1] = GameObject.Find("Head_collider");//머리 공격범위
        area[2] = GameObject.Find("downAttackRange");
        area[3] = GameObject.Find("hornRange");//돌진 공격범위
        area[4] = GameObject.Find("chargeRange");
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        area[3].SetActive(false);
        area[4].SetActive(false);
        DustParticle.SetActive(false);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        state = 0;
        lookAtPlayer = false;
        run = false;
        choosePattern();
    }
    IEnumerator state_0()
    {
        yield return new WaitForSeconds(0.1f);
        if (Green_Area_Detect.whichArea == 1)
        {
            state = 1;
        }
        else if (Green_Area_Detect.whichArea == 2)
        {
            state = Random.Range(2, 6);
        }
        else if (Green_Area_Detect.whichArea == 3)
        {
            state = Random.Range(6, 8);
        }
        else if (Green_Area_Detect.whichArea == 4)
        {
            state = 8;
        }
        choosePattern();
        yield return 0;
    }
    IEnumerator BackJump()
    {
        anim.SetTrigger("BackJump");
        BackJumpVec = BackJumpPos.transform.position;
        yield return new WaitForSeconds(0.6f);
        isBackJump = true;
        yield return new WaitForSeconds(0.4f);
        isBackJump = false;
        state = 0;
        choosePattern();
    }
    IEnumerator FrontJump()
    {
        anim.SetTrigger("BackJump");
        FrontJumpVec = FrontJumpPos.transform.position;
        yield return new WaitForSeconds(0.6f);
        isFrontJump = true;
        yield return new WaitForSeconds(0.4f);
        isFrontJump = false;
        state = 0;
        choosePattern();
    }
    IEnumerator move()
    {
        yield return new WaitForSeconds(2f);
        anim.SetInteger("walk", 0);
        anim.SetInteger("isRight", 0);
        state = 0;
        choosePattern();
    }
    IEnumerator charge()
    {
        anim.SetTrigger("scream");
        //howlingSound.Play();
        yield return new WaitForSeconds(1.2f);
        anim.SetInteger("run", 1);
        //dashSound.Play();
        area[4].SetActive(true);
        isAttacking = true;
        run = true;
        lookAtPlayer = true;
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.1f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        lookAtPlayer = false;
        yield return new WaitForSeconds(1f);
        run = false;
        area[4].SetActive(false);
        isAttacking = false;
        anim.SetInteger("run", 0);
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }

    IEnumerator bite()
    {
        lookAtPlayer = false;
        anim.SetTrigger("bite");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        area[1].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        area[1].SetActive(false);
        yield return new WaitForSeconds(0.8f);
        lookAtPlayer = true;
        isAttacking = false;
        state = 0;
        choosePattern();
    }
    IEnumerator claw()
    {
        anim.SetTrigger("claw");
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.2f);
        area[0].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
        area[0].SetActive(false);
        yield return new WaitForSeconds(1f);
        lookAtPlayer = true;
        state = 0;
        choosePattern();
    }
    IEnumerator horn()
    {
        lookAtPlayer = false;
        anim.SetTrigger("horn");
        isAttacking = true;
        yield return new WaitForSeconds(0.7f);
        Debug.Log("지금");
        area[3].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
        area[3].SetActive(false);
        yield return new WaitForSeconds(0.8f);
        lookAtPlayer = true;
        state = 0;
        choosePattern();
    }
    IEnumerator downAttack()
    {
        anim.SetTrigger("downAttack");
        isAttacking = true;
        yield return new WaitForSeconds(1.05f);
        DustParticle.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.1f);
        //DustParticle.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        area[2].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        area[2].SetActive(false);
        yield return new WaitForSeconds(0.25f);
        DustParticle.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
        lookAtPlayer = true;
        state = 0;
        choosePattern();
    }
    void choosePattern()
    {
        if (!isDeath)
        {
            switch (state)
            {
                case 0:
                    StartCoroutine("state_0");//일시정지
                    break;
                case 1:
                    if(Green_Area_Detect.inCenter){
                        StartCoroutine("BackJump");
                    }
                    else{
                        StartCoroutine("FrontJump");
                    }
                    break;
                case 2:
                    //attack2 물기
                    StartCoroutine("bite");
                    break;
                case 3:
                    //박치기
                    StartCoroutine("horn");
                    break;
                case 4:
                    //한번 패기
                    StartCoroutine("claw");
                    break;
                case 5:
                    StartCoroutine("downAttack");
                    break;
                case 6:
                    StartCoroutine("move");
                    break;
                case 7:
                    isRight = Random.Range(0, 2);
                    StartCoroutine("move");
                    //옆으로
                    break;
                case 8:
                    StartCoroutine("charge");
                    break;
                //돌진
                //비고
                default:
                    break;
            }
        }
    }

    IEnumerator Diiie()
    {
        yield return new WaitForSeconds(3f);
        victory.SetActive(true);

    }
    void Update()
    {
        if (isBackJump)
        {
            transform.position = Vector3.Lerp(transform.position, BackJumpVec, 0.02f);
        }
        if (isFrontJump)
        {
            transform.position = Vector3.Lerp(transform.position, FrontJumpVec, 0.02f);
        }
        if (monsterHealth <= 0 && !isDeath)
        {
            isDeath = true;
            lookAtPlayer = false;
            state = 9;
            monsterHealth = 0;
            anim.SetTrigger("death");
            anim.SetInteger("dying", 1);
            StartCoroutine("Diiie");
        }
        if (isDeath)
        {
            monsterHealth = 0;
            lookAtPlayer = false;
        }
        Vector3 dir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        Vector3 zero = new Vector3(0f, 0f, 0f);
        if (run)
        {
            rigid.velocity = dir.normalized * 20.0f;
        }
        if (state == 6)
        {
            lookAtPlayer = true;
            anim.SetInteger("walk", 1);
            if (Green_Area_Detect.whichArea != 3)
            {
                StopCoroutine("move");
                anim.SetInteger("walk", 0);
                state = 0;
                choosePattern();
            }
            rigid.velocity = dir.normalized * 4.0f;
        }

        if (state == 7)
        {
            anim.SetInteger("walk", 1);
            lookAtPlayer = true;
            if (Green_Area_Detect.whichArea != 3)
            {
                StopCoroutine("move");
                anim.SetInteger("walk", 0);
                anim.SetInteger("isRight", 0);
                state = 0;
                choosePattern();
            }
            if (isRight == 1)
            {
                //오른쪽걷기모션 설정
                anim.SetInteger("isRight", 1);
                rigid.velocity = transform.right * 4.0f;
            }
            else if (isRight == 0)
            {
                //왼쪽쪽걷기모션 설정
                anim.SetInteger("isRight", 2);
                rigid.velocity = transform.right * -4.0f;
            }
        }

        if (lookAtPlayer)
        {
            rotGoal = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.02f);
        }
        else
        {
            anim.SetInteger("walk", 0);
        }

        if (state == 0 || state == 1 || state == 2 || state == 3)
        {
            rigid.velocity = zero * 4.0f;
        }
    }
}
