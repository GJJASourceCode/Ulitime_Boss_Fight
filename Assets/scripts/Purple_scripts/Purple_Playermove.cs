using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple_Playermove : MonoBehaviour
{
    float bbCurrentTime, bbTime;
    public AudioSource slash1, slash2, bossBattleBGM, defeatBGM, victoryBGM;
    GameObject body;
    public GameObject deathtext;
    public static int slashNum, health;
    Animator anim;
    float xInput, zInput, pSpeed, slashTime, slashCurrentTime, rollTime, rollCurrentTime;
    Vector3 moveVec, point;
    Vector2 turn;
    Rigidbody pRigid;
    bool canRoll, rollEnd;
    public static bool isSlashing, isRoll, isHited, isDeath, isEnd;
    void Awake()
    {
        isEnd = false;
        bossBattleBGM.Play();
        bbTime = 130f;
        body = GameObject.Find("Armature");
        pRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pSpeed = 4.0f;
        slashCurrentTime = 0f;
        rollCurrentTime = 0f;
        slashTime = 0f;
        rollTime = 0.9f;
        isRoll = false;
        isSlashing = false;
        isHited = false;
        isDeath = false;
        canRoll = true;
        slashNum = 0;
        health = 200;
    }
    void Update()
    {
        
        bbCurrentTime += Time.deltaTime;
        if(bbCurrentTime>bbTime){
            bbCurrentTime =0f;
            bossBattleBGM.Play();
        }
        if(isDeath&&!isEnd){
            isEnd = true;
            bossBattleBGM.Stop();
            defeatBGM.Play();
        }
        if(PurplePattern.isDeath&&!isEnd){
            isEnd = true;
            bossBattleBGM.Stop();
            Debug.Log("승리");
            victoryBGM.Play();
        }
        if(health<=0&&!isDeath){
            anim.SetTrigger("death");
            isDeath = true;
            health=0;
            anim.SetInteger("dying", 1);
            deathtext.SetActive(true);
        }
        turn.x += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(xInput, 0f, zInput);
        slashCurrentTime += Time.deltaTime;
        rollCurrentTime += Time.deltaTime;
        if (slashCurrentTime > slashTime)
        {
            isSlashing = false;
        }
        if (rollCurrentTime > 0.3f)
        {
            pSpeed = 4.0f;
            isRoll = false;
        }
        if (rollCurrentTime > 0.7f)
        {
            rollEnd = true;
        }
        if (rollCurrentTime > rollTime)
        {
            canRoll = true;
        }
        if(!isDeath){
            if (Input.GetKey("w"))
        {
            pRigid.velocity = transform.forward * pSpeed;
        }
        if (Input.GetKey("s"))
        {
            pRigid.velocity = -transform.forward * pSpeed;
        }
        if (Input.GetKey("d"))
        {
            pRigid.velocity = transform.right * pSpeed;
        }
        if (Input.GetKey("a"))
        {
            pRigid.velocity = -transform.right * pSpeed;
        }
        }
        if (Input.GetMouseButton(0) && !isSlashing && rollEnd && !isDeath)
        {
            slashCurrentTime = 0;
            isSlashing = true;
            slashNum = Random.Range(1, 3);
            if (slashNum == 1)
            {
                slashTime = 1.2f / 1.5f;
                anim.SetTrigger("slashing1");
                slash1.Play();
            }
            else if (slashNum == 2)
            {
                slashTime = 1.8f / 1.5f;
                anim.SetTrigger("slashing2");
                slash2.Play();
            }
        }
        else if (Input.GetKeyDown("left shift") && !isSlashing && canRoll && !isDeath)
        {
            rollCurrentTime = 0;
            isRoll = true;
            rollEnd = false;
            canRoll = false;
            pSpeed = 10f;
            anim.SetTrigger("roll");
        }
        if (Mathf.Abs(xInput) > Mathf.Epsilon || Mathf.Abs(zInput) > Mathf.Epsilon)
        {
            if(!isDeath){
                anim.SetInteger("animState", 1);
            }
        }
        else
        {
            if(!isDeath){
                anim.SetInteger("animState", 0);
            }
        }
        Debug.Log(isDeath);
    }
}
