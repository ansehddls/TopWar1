using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController playerCc;                                                                                                     //플레이어의 캐릭터 컨트롤러 컴포넌트를 넣어놓을 변수
    float moveSpeed = 5f;                                                                                                              //플레이어가 움직일 이동 속도는 5이다
    
    Animator playerAnim;                                                                                                              //플레이어의 하위객체에 있는 에니매이션 컨트롤러를 담을 변수
    float attackTime;                                                                                                                     //공격 시간
    float h = 0;                                                                                                                            //좌우 키값을 담을 변수
    float v = 0;                                                                                                                            //앞뒤 키값을 담을 변수
    bool isAttack = false;                                                                                                               //공격 상태가 아니다

    enum PlayerFSM                                                                                                                     //플레이어의 상태를 나타내기 위한 열거형 변수
    {
        Idle,                                                                                                                                 //대기
        Run,                                                                                                                                //달리기
        Attack,                                                                                                                             //공격
        Die                                                                                                                                 //죽음
    }

    PlayerFSM playerFSM;                                                                                                              //플레이어의 강태를 조정할 변수

    void Start()
    {  
        playerCc = GetComponent<CharacterController>();                                                                     //플레이어의 캐릭터 컨트롤러 컴포넌트를 가져온다
        playerAnim = GetComponentInChildren<Animator>();                                                                 //플레이어의 자식에게 있는 에니메이터 컴포넌트를 가져온다
        playerFSM = PlayerFSM.Idle;                                                                                                  //플레이어는 시작하자 마자 대기 상태이다
        playerAnim.Play("PlayerIdle");                                                                                                 //플레이어는 대기 에니메이션을 실행한다
    }


    void Update()
    {
        h = Input.GetAxis("Horizontal");                                                                                              //좌우 키값 을 받아온다
        v = Input.GetAxis("Vertical");                                                                                                  //앞뒤 키 값을 받아온다
        if (Input.GetButtonDown("Fire1"))                                                                                            //마우스 좌클릭을 누르면
        {
            isAttack = true;                                                                                                                //공격 상태이다
            playerFSM = PlayerFSM.Attack;                                                                                            //공격 상태로 전환한다
        }
        PlayerStateControll();
        PlayerDie();
    }

    void PlayerMove()                                                                                                                    //플레이어가 움직이는 메서드
    {
        
        float gravity = 2f;                                                                                                                   //중력
        
        Vector3 moveDir = new Vector3(h, 0, v);                                                                                     //캐릭터를 움직일 방향
        moveDir = moveDir.normalized;                                                                                                //대각선 이동 방향도 같게 하기 위해 노말라이즈 해준다
        if (h != 0 || v != 0)                                                                                                                  //h와 v값이 0일시 캐릭터가 보는 방향에 문제가 생김
        {
            transform.forward = moveDir;                                                                                              //캐릭터의 정면은 움직이는 방향으로 한다
            playerAnim.SetBool("IsRun", true);                                                                                         //플레이어는 달리기 에니메이션을 실행한다
        }
        else                                                                                                                                    //캐릭터가 움직이지 않을 시         
        {
            playerFSM = PlayerFSM.Idle;                                                                                                //플레이어는 대기 상태이다
            playerAnim.SetBool("IsRun", false);                                                                                        //플레이어의 대기 애니메이션을 실행한다
        }
        moveDir = moveDir * moveSpeed;                                                                                            //캐릭터의 이동속도를 넣는다
        moveDir.y = -gravity;
        playerCc.Move(moveDir * Time.deltaTime);                                                                                  //캐릭터는 초당 5M이동한다
    }

    void PlayerStateControll()
    {
        switch (playerFSM)
        {
            case PlayerFSM.Idle:                                                                                                           //플레이어는 대기상태이다
                if (h != 0 || v != 0)                                                                                                         //h와 v값이 0이 아니라면
                {
                    playerFSM = PlayerFSM.Run;                                                                                        //플레이어는 달리기 상태이다
                }
                break;
            case PlayerFSM.Run:                                                                                                           //플레이어는 이동하는 상태이다
                PlayerMove();                                                                                                                //플레이어의 이동을 담당하는 메서드
                break;
            case PlayerFSM.Attack:                                                                                                        //플레이어는 공격상태이다
                attackTime += Time.deltaTime;                                                                                         //공격 시간이 증가한다
                playerAnim.SetBool("IsRun", false);                                                                                     //플레이어는 공격 에니메이션을 실행한다
                playerAnim.SetBool("IsAttack", true);                                                                                  //플레이어는 공격 에니메이션을 실행한다
                
                if (attackTime >= 1.8f)                                                                                                    //공격 에니메이션 발동 시간
                {
                    isAttack = false;                                                                                                        //공격중이 아니다
                    attackTime = 0;                                                                                                        //공격시간 초기화
                    if (!isAttack)                                                                                                             //공격중이 아니다
                    {
                        if (h != 0 || v != 0)
                        {
                            playerAnim.SetBool("IsAttack", false);                                                                     //플레이어는 달리기 에니메이션을 실행한다
                            playerFSM = PlayerFSM.Run;                                                                               //플레이어는 달리기 상태이다
                        }
                        else
                        {
                            playerAnim.SetBool("IsAttack", false);                                                                     //플레이어는 달리기 에니메이션을 실행한다
                            playerFSM = PlayerFSM.Idle;                                                                               //플레이어는 대기상태이다
                        }
                    }
                    else                                                                                                                      //마우스 좌클릭을 연타해서 타이밍에 맞게 공격이 된다면
                    {
                        playerAnim.SetBool("IsAttack", false);                                                                        //공격 초기화
                        playerFSM = PlayerFSM.Attack;                                                                               //공격 재실행
                    }
                }
                break;
            case PlayerFSM.Die:                                                                                                          //플레이어는 죽은상태이다 
                playerAnim.Play("PlayerDead");                                                                                        //죽음 애니메이션 실행
                playerCc.enabled = false;                                                                                               //플레이어의 캐릭터 컨트롤러를 비활성화한다
                break;
        }
    }

    void PlayerDie()
    {
        if (GameManager.GM.playerHp <= 0)                                                                                       //플레이어의 피가 0보다 작으면
        {
            GameManager.GM.isEnd = true;                                                                                          //플레이어는 죽은 상태이다
            playerFSM = PlayerFSM.Die;                                                                                               //플레이어는 죽은 상태이다
        }
        if (GameManager.GM.isEnd)                                                                                                   //게임 메니저에서 죽었다면
        {
            playerFSM = PlayerFSM.Die;                                                                                              //플레이어는 사망한다
        }
    }
}
