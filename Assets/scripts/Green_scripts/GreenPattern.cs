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
    public GameObject player, victory, BackJumpPos;
    Rigidbody rigid;
    Vector3 currentVec, BackJumpVec;
    bool lookAtPlayer, run, isBackJump;
    Quaternion rotGoal;

    int isRight;

    
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        isAttacking = false;
        isDeath = false;
        isBackJump = false;
        monsterHealth = 1000;
        BackJumpPos = GameObject.Find("BackJumpPosition");
        area = new GameObject[4];
        area[0] = GameObject.Find("Hand_collider");//손 공격범위
        area[1] = GameObject.Find("Head_collider");//머리 공격범위
        area[3] = GameObject.Find("chargeRange");//돌진 공격범위
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[3].SetActive(false);
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
        if(Green_Area_Detect.whichArea==1){
            state = 1;
        }
        else if(Green_Area_Detect.whichArea==2){
            state = Random.Range(2, 6);
        }
        else if(Green_Area_Detect.whichArea==3){
            state = Random.Range(6,8);
        }
        else if(Green_Area_Detect.whichArea==4){
            state = 8;
        }
        choosePattern();
        yield return 0;
    }
    IEnumerator BackJump(){
        anim.SetTrigger("BackJump");
        BackJumpVec = BackJumpPos.transform.position;
        yield return new WaitForSeconds(0.6f);
        isBackJump = true;
        yield return new WaitForSeconds(0.4f);
        isBackJump = false;
    }
    /*IEnumerator move(){
        yield return new WaitForSeconds(2f);
        state = 0;
        choosePattern();
    }*/

    /*IEnumerator attack2()
    {
        anim.SetTrigger("Attack2");
        attack2Sound.Play();
        yield return new WaitForSeconds(0.8f);
        area[1].SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);
        area[1].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(1.4f);
        state = 0;
        choosePattern();
    }*/
    IEnumerator charge()
    {
        anim.SetTrigger("scream");
        howlingSound.Play();
        yield return new WaitForSeconds(1.4f);
        anim.SetInteger("run", 1);
        dashSound.Play();
        area[3].SetActive(true);
        isAttacking = true;
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
        area[3].SetActive(false);
        isAttacking = false;
        anim.SetInteger("run", 0);
        run = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
    /*IEnumerator jumpAttack()
    {

        anim.SetTrigger("jumpAttack");
        yield return new WaitForSeconds(0.4f);
        jumpAttackSound1.Play();
        yield return new WaitForSeconds(1.2f);
        isAttacking = true;
        area[2].SetActive(true);
        jumpAttackSound2.Play();
        yield return new WaitForSeconds(0.1f);
        area[2].SetActive(false);
        isAttacking = false;
        state = 0;
        choosePattern();
    }
    IEnumerator movetime()
    {
        yield return new WaitForSeconds(2f);
        state = 0;
        choosePattern();
    }*/
    void choosePattern()
    {
        if(!isDeath){
            switch (state)
        {
            case 0:
                StartCoroutine("state_0");//일시정지
                break;
            case 1:
                //attack1 뒤로 회피

                break;
            case 2:
                //attack2 물기
                break;
            case 3:
                //박치기
                break;
            case 4:
                //한번 패기   
                break;
            case 5:
                //두번 패기
                break;
            case 6:
                StartCoroutine("move");
                break;
            case 7:
                isRight = Random.Range(0,2);
                StartCoroutine("move");
                //옆으로
                break;
            case 8:
                //비고
            default:
                break;
        }
        }
    }

    IEnumerator Diiie(){
        yield return new WaitForSeconds(3f);
        victory.SetActive(true);

    }
    void Update()
    {
        Debug.Log(Green_Area_Detect.whichArea);
        if(isBackJump){
            transform.position = Vector3.Lerp(transform.position, BackJumpVec, 0.02f);
        }
        if(monsterHealth<=0&&!isDeath){
            isDeath = true;
            state = 8;
            monsterHealth = 0;
            anim.SetTrigger("death");
            anim.SetInteger("dying",1);
            StartCoroutine("Diiie");
        }
        if(isDeath){
            monsterHealth = 0;
        }
        Vector3 dir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        Vector3 zero = new Vector3(0f, 0f, 0f);
        if (state==6)
        {
            if(Green_Area_Detect.whichArea!=3){
                StopCoroutine("move");
                state = 0;
                choosePattern();
            }
            lookAtPlayer = true;
            //걷기모션 설정
            rigid.velocity = dir.normalized * 4.0f;
        }

        if (state==7)
        {
            if(Green_Area_Detect.whichArea!=3){
                StopCoroutine("move");
                //걷기모션 죽이기
                state = 0;
                choosePattern();
            }
            if(isRight==1){
                //오른쪽걷기모션 설정
                rigid.velocity = transform.right * 4.0f;
            }
            else if(isRight==0){
                //왼쪽쪽걷기모션 설정
                rigid.velocity = transform.right * -4.0f;
            }
            lookAtPlayer = true;
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
