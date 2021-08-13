using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NMHZombie : NMHUnit
{
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 변수

    GameObject objPlayer;                                           //쫒아갈 플레이어
        
    NavMeshAgent navZombie;                                         //컴포넌트

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 Unity 함수

    void Start ()
    {
        InitZombie();                                               //좀비 초기화
    }
	
	void Update ()
    {
        ChasePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "Player")
        {
            Attack();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            Attack();
        }
    }

    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////
    //아래는 private 함수

    void InitZombie()
    {
        base.InitUnit(-1, 200, 2.0f, UnitType.ZOMBIE);               //상위 클래스 Unit 초기화

        objPlayer = GameObject.Find("Player");                      //쫒아갈 플레이어 Find

        navZombie = GetComponent<NavMeshAgent>();                   //겟 컴포넌트
        navZombie.speed = fMoveSpeed;                               //오브젝트의 이동 속도 초기화
    }

    void ChasePlayer()
    {
        navZombie.SetDestination(objPlayer.transform.position);     //플레이어를 목적지로 삼고 따라감
    }

    void Attack()
    {
        if (objPlayer.transform.transform.localPosition.y >= -320.0f && objPlayer.GetComponent<NMHPlayerHP>().bIsArmor == false)
        {
            {
                objPlayer.GetComponent<PlayerMove>().PlayerHit(transform.rotation * Vector3.forward + new Vector3(-1f, 0.0005f, 0));
                objPlayer.GetComponent<NMHPlayerHP>().SetHP(objPlayer.GetComponent<NMHPlayerHP>().GetHP() - 20);
                StartCoroutine(objPlayer.GetComponent<NMHPlayerHP>().SetArmor());

                if(objPlayer.GetComponent<NMHPlayerHP>().GetHP() <= 0)
                {
                    NMHWaveMng.instance.IngameUICtrl.ShowGameOver();
                }
            }
        }
    }
}
