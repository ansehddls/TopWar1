using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public enum EnemyType                                                                                                                                           //열거형 변수로 에너미 종류를 만듬
    {
        EnemyA,
        EnemyB,
        EnemyC,
        EnemyD
    }
    [SerializeField] public EnemyType enemyType;                                                                                                              //에너미 타입을 사용할 변수

    enum EnemyFSM                                                                                                                                                  //에너미의 동적상태를 나타낼 변수
    {
        Idle,                                                                                                                                                                //대기
        Run,                                                                                                                                                                //달리기
        Attack,                                                                                                                                                             //공격
        Damaged,                                                                                                                                                        //피격
        Die                                                                                                                                                                 //죽음
    }
    EnemyFSM enemyFsm;                                                                                                                                            //동적 상태를 사용하기 위한 변수
    CapsuleCollider enemyCol;                                                                                                                                       //에너미의 캡슐 콜라이더를 가져올 변수
    Animator enemyAnim;                                                                                                                                            //에너미 자식에게 있는 에니메이터를 담아놓을 변수
    Transform enemyTr;                                                                                                                                               //에너미의 위치 값을 넣을 변수
    float currentTime;                                                                                                                                                  //현재 시간을 넣을 변수
    bool isDamaged;                                                                                                                                                   //맞고 있는중인지 아닌지 확인할 변수
    bool isDie;                                                                                                                                                           //죽어있는지 확인할 변수
    public int enemyAHp;                                                                                                                                             //에너미의 체력을 담을 변수
    public int enemyBHp;                                                                                                                                             //에너미의 체력을 담을 변수
    public int enemyCHp;                                                                                                                                             //에너미의 체력을 담을 변수
    public int enemyDHp;                                                                                                                                                     //에너미의 체력을 담을 변수

    void Start()
    {
        enemyTr = GetComponent<Transform>();                                                                                                              //에너미의 위치 컴포넌트를 가져오기 위한 변수
        enemyAnim = GetComponentInChildren<Animator>();                                                                                              //에너미의 자식에게 있는 에니메이터를 가져온다
        enemyCol = GetComponent<CapsuleCollider>();                                                                                                     //에너미의 캡슐 콜라이더를 가져온다
        EnemyTypeStatus();                                                                                                                                           //에너미의 종류의 따라 정적상태를 나타내는 메서드
        enemyFsm = EnemyFSM.Idle;                                                                                                                              //처음 적이 생성되었을 때 대기 상태이다
        isDamaged = false;                                                                                                                                           //맞고 있는중이 아니다
        isDie = false;
    }


    void Update()
    {
        print(enemyFsm);
        if (GameManager.GM.isEnd)                                                                                                                                //만약 게임이 끝났다면
        {
            if (enemyType == EnemyType.EnemyD)
            {
                Destroy(gameObject, 3f);
            }
            if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRun"))
            {
                enemyAnim.SetBool("IsRun", false);
            }
            return;

        }
        else
        {
            if (enemyType == EnemyType.EnemyA)
            {
                EnemyDynamicState();
                if (enemyAHp <= 0)
                {
                    enemyFsm = EnemyFSM.Die;
                }
            }
            else if (enemyType == EnemyType.EnemyB)
            {
                EnemyDynamicState();
                if (enemyBHp <= 0)
                {
                    enemyFsm = EnemyFSM.Die;
                }
            }
            else if (enemyType == EnemyType.EnemyC)
            {
                EnemyDynamicState();
                if (enemyCHp <= 0)
                {
                    enemyFsm = EnemyFSM.Die;
                }
            }
            else if (enemyType == EnemyType.EnemyD)
            {
                
                if (enemyDHp <= 0)
                {
                    enemyFsm = EnemyFSM.Die;
                }
                EnemyDDynamicState();
            }
        }                                                                                                      //에너미의 종류에 따라 움직이는 메서드
    }

    void EnemyTypeStatus()                                                                                                                                        //에너미의 종류의 따라 상태를 나타내는 메서드
    {
        switch (enemyType)                                                                                                                                         //에너미의 종류에 따라 다르게 실행한다
        {
            case EnemyType.EnemyA:                                                                                                                              //에너미 A라면(근거리)
                EnemyManager.EM.enemyMoveSpeed = 2f;                                                                                                   //에너미의 이동속도는 3이다
                enemyAHp = 100;                                                                                                                                   //에너미 A의 체력은 100이다
                EnemyManager.EM.enemyAttackPower = 5;                                                                                                   //에너미 A의 공격력은 5이다
                break;
            case EnemyType.EnemyB:                                                                                                                              //에너미 B라면(원거리)
                EnemyManager.EM.enemyMoveSpeed = 1f;                                                                                                   //에너미의 이동속도는 3이다
                enemyBHp = 50;                                                                                                                                     //에너미 B의 체력은 50이다
                EnemyManager.EM.enemyAttackPower = 3;                                                                                                   //에너미 A의 공격력은 3이다
                break;
            case EnemyType.EnemyC:                                                                                                                              //에너미 C라면(중간보스)
                EnemyManager.EM.enemyMoveSpeed = 1f;                                                                                                   //에너미의 이동속도는 3이다
                enemyCHp = 150;                                                                                                                                    //에너미 C의 체력은 150이다
                EnemyManager.EM.enemyAttackPower = 10;                                                                                                 //에너미 C의 공격력은 10이다
                break;
            case EnemyType.EnemyD:                                                                                                                              //에너미 D라면(최종보스)
                EnemyManager.EM.enemyAttackPower = 30;
                enemyDHp = 1000;
                break;
        }
    }

    void EnemyDynamicState()                                                                                                                                    //에너미의 동적 상태 메서드
    {
       
        switch (enemyFsm)                                                                                                                                          //에너미의 동적 상태에 따라 바뀌는 모션들
        {
            case EnemyFSM.Idle:                                                                                                                                    //에너미가 대기상태라면
                enemyAnim.Play("EnemyIdle");                                                                                                                    //1초동안 아이들 애니메이션 실행
                currentTime += Time.deltaTime;                                                                                                                 //현재 시간에 Time.deltaTime을 곱해준다
                if (currentTime >= 1f)                                                                                                                              //생성되고 1초 후에
                {
                    enemyFsm = EnemyFSM.Run;                                                                                                                //바로 Run상태로 이동
                    currentTime = 0f;                                                                                                                                //현재 시간 초기화
                }
                break;
            case EnemyFSM.Run:                                                                                                                                   //에너미가 추적중이라면
                if (GameManager.GM.isEnd) return;                                                                                                            //플레이어가 죽었다면 이동 금지
                StopCoroutine(EnemyAttack());
                EnemyManager.EM.EnemyMove(enemyTr);                                                                                                   //에너미가 플레이어를 추적하기 위해서 사용할 메서드
                enemyAnim.SetBool("IsRun", true);                                                                                                              //Run애니메이션 실행
                EnemyManager.EM.Distance(enemyTr.position);                                                                                             //에너미와 플레이어 사이의 거리값을 반환한다
                if (enemyType == EnemyType.EnemyA || enemyType == EnemyType.EnemyC)
                {

                    if (EnemyManager.EM.distance < 1.5f)                                                                                                     //에너미와 플레이어 사이의 거리가 1.5보다 작다면
                    {
                        enemyFsm = EnemyFSM.Attack;                                                                                                        //에너미의 상태는 공격상태가 된다
                        enemyAnim.SetBool("IsRun", false);                                                                                                     //Run애니메이션 중지
                    }
                }
                else if (enemyType == EnemyType.EnemyB)                                                                                                 //만약 에너미 B라면
                {

                    if (EnemyManager.EM.distance < 5f)                                                                                                       //에너미와 플레이어 사이의 거리가 1.5보다 작다면
                    {
                        enemyFsm = EnemyFSM.Attack;                                                                                                         //에너미의 상태는 공격상태가 된다
                        enemyAnim.SetBool("IsRun", false);                                                                                                     //Run애니메이션 중지
                    }
                }
                break;
            case EnemyFSM.Attack:                                                                                                                                //에너미가 공격상태라면
                StopCoroutine(EnemyAttack());
                StartCoroutine(EnemyAttack());                                                                                                                   //에너미의 공격 코루틴 실행
                break;
            case EnemyFSM.Damaged:                                                                                                                            //에너미가 피격상태라면
                if (isDamaged)                                                                                                                                        //만약 적이 맞는 상태라면
                {
                    StopCoroutine(EnemyAttack());                                                                                                               //실행중인 코루틴을 정지한다
                    enemyAnim.SetBool("IsDamaged", true);                                                                                                   //에너미는 데미지 받는 에니메이션을 실행한다
                    if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("EnemyDamaged") && 
                        enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                    {
                        isDamaged = false;                                                                                                                         //맞는중이 아니다
                    }
                }
                else
                {
                    EnemyManager.EM.Distance(enemyTr.position);                                                                                         //에너미와 플레이어 사이의 거리값을 반환한다
                    if (enemyType == EnemyType.EnemyA || enemyType == EnemyType.EnemyC)                                                 //에너미의 종류가 에너미 A라면
                    {
                        if (EnemyManager.EM.distance < 1.5f)                                                                                                 //에너미와 플레이어의 거리가 1.5m보다 작다면
                        {
                            enemyAnim.SetBool("IsDamaged", false);                                                                                         //에너미는 데미지 받는 에니메이션을 실행한다
                            enemyFsm = EnemyFSM.Attack;                                                                                                    //에너미는 공격 상태가 된다

                        }
                        else
                        {
                            enemyAnim.SetBool("IsDamaged", false);                                                                                          //에너미는 데미지 받는 에니메이션을 실행한다
                            enemyFsm = EnemyFSM.Run;                                                                                                        //에너미는 달리기 상태가 된다
                        }
                        
                        }
                    else if (enemyType == EnemyType.EnemyB)                                                                                             //에너미의 종류가 에너미 A라면
                    {
                        if (EnemyManager.EM.distance < 5f)                                                                                                   //에너미와 플레이어의 거리가 1.5m보다 작다면
                        {
                            enemyAnim.SetBool("IsDamaged", false);                                                                                         //에너미는 데미지 받는 에니메이션을 실행한다
                            enemyFsm = EnemyFSM.Attack;                                                                                                    //에너미는 공격 상태가 된다

                        }
                        else
                        {
                            enemyAnim.SetBool("IsDamaged", false);                                                                                           //에너미는 데미지 받는 에니메이션을 실행한다
                            enemyFsm = EnemyFSM.Run;                                                                                                         //에너미는 달리기 상태가 된다
                        }

                    }
                }
                break;
            case EnemyFSM.Die:                                                                                                                                     //에너미가 죽음상태라면
                if (!isDie)
                { 
                    StopCoroutine(EnemyAttack());                                                                                                                //실행중인 코루틴을 정지한다
                    enemyCol.enabled = false;                                                                                                                     //에너미의 콜라이더를 비활성화 시킨다
                    enemyAnim.SetTrigger("Die");                                                                                                                 //죽는 애니메이션 실행
                    isDie = true;                                                                                                                                       //죽는 애니메이션 실행
                    GameManager.GM.killCount++;                                                                                                              //플레이어의 킬 카운트 증가
                }
                if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("EnemyDead") 
                    && enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    Destroy(gameObject);                                                                                                                          //죽는 애니메이션이 끝나면 게임오브젝트 파괴
                }
                break;
        }
    }
    IEnumerator EnemyAttack()
    {
        if (enemyType == EnemyType.EnemyB)
        {
            EnemyManager.EM.AttackRotation(enemyTr);                                                                                                  //공격하기전에 적을 향해서 회전한다                                                                                                       
        }
        enemyAnim.SetBool("IsAttack", true);                                                                                                                 //공격 애니메이션 실행
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack") 
            && enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            EnemyManager.EM.Distance(transform.position);                                                                                              //에너미와 플레이어 사이의 거리값을 반환한다
            if (enemyType == EnemyType.EnemyA || enemyType == EnemyType.EnemyC)                                                       //에너미 A나 에너미 C라면
            {
                if (EnemyManager.EM.distance < 1.5f)                                                                                                        //에너미와 플레이어 사이의 거리가 1.5보다 작다면
                {
                    yield return null;                                                                                                                               //한 프레임을 넘긴다
                    enemyAnim.SetTrigger("Idle");                                                                                                              //대기 상테의 에니메이션을 실행한다

                }
                else                                                                                                                                                    //에너미와 플레이어 사이의 거리가 1.5보다 크다면
                {
                    yield return null;
                    enemyAnim.SetBool("IsAttack", false);                                                                                                     //공격에서 달리기 상태가 된다
                    enemyFsm = EnemyFSM.Run;                                                                                                               //에너미의 상태는 달리기 상태이다
                }
            }
        }
        if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("EnemyB Attack") 
            && enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            EnemyManager.EM.Distance(transform.position);                                                                                               //에너미와 플레이어 사이의 거리값을 반환한다
            if (enemyType == EnemyType.EnemyB)                                                                                                          //만약 에너미C라면
            {

                if (EnemyManager.EM.distance < 5f)                                                                                                          //에너미와 플레이어 사이의 거리가 5보다 작다면
                {
                    yield return null;                                                                                                                               //한 프레임을 넘긴다
                    enemyAnim.SetTrigger("Idle");                                                                                                              //대기 상테의 에니메이션을 실행한다

                }
                else                                                                                                                                                   //에너미와 플레이어 사이의 거리가 5보다 작다면
                {
                    yield return null;
                    enemyAnim.SetBool("IsAttack", false);                                                                                                    //공격에서 달리기 상태가 된다
                    enemyFsm = EnemyFSM.Run;                                                                                                              //에너미의 상태는 달리기 상태이다
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon")                                                                                                    //부딪힌 콜라이더의 이름이 playerAttackCol일때
        {
            if (enemyType == EnemyType.EnemyD)
            {
                isDamaged = false;
            }
            else
            {
                isDamaged = true;                                                                                                                                   //맞은 상태이다
                enemyFsm = EnemyFSM.Damaged;                                                                                                             //에너미는 데미지 받는 상태이다
            }
        }
    }

    void EnemyDDynamicState()
    {
        switch (enemyFsm)
        {
            case EnemyFSM.Idle:                                                                                                                                     //대기상태
                enemyAnim.Play("Idle");                                                                                                                             //대기 애니메이션을 실행한다
                if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle")
                    && enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)                                                        //만약 에너미 아이들 애니메이션이 30퍼센트 이상 진행된다면
                {
                    enemyFsm = EnemyFSM.Attack;                                                                                                              //에너미는 공격상태가 된다
                }
                break;
            case EnemyFSM.Attack:                                                                                                                                 //공격상태
                if (EnemyManager.EM.Distance(enemyTr.position) <= 5f)                                                                                  //플레이어와의 거리값이 2.5보다 작다면
                {
                    int randAttack = Random.Range(0, 2);                                                                                                      //랜덤 공격 방법
                    if (randAttack == 0)
                    {
                        EnemyManager.EM.AttackRotation(enemyTr);                                                                                          //공격하기전 플레이어를 향해 회전한다
                        enemyAnim.SetBool("IsAttack01", true);                                                                                                  //적이 근거리 공격을 시작한다
                        enemyAnim.SetBool("IsAttack02", false);
                        enemyAnim.SetBool("IsAttack03", false);
                    }
                    else
                    {
                        enemyAnim.SetBool("IsAttack01", false);
                        enemyAnim.SetBool("IsAttack02", true);                                                                                                 //적이 원거리 공격1을 시작한다
                        enemyAnim.SetBool("IsAttack03", false);
                    }
                                                                                                           
                }
                else                                                                                                                                                      //플레이어와의 거리값이 2.5보다 크다면 원거리 공격
                {
                    int randomAttack = Random.Range(0, 2);                                                                                                  //랜덤 공격하기 위한 변수
                    if (randomAttack == 0)                                                                                                                         //랜덤 공격변수가 0이라면(50퍼센트의 확률)
                    {
                        enemyAnim.SetBool("IsAttack01", false);                                                                                                    
                        enemyAnim.SetBool("IsAttack02", true);                                                                                                 //적이 원거리 공격1을 시작한다
                        enemyAnim.SetBool("IsAttack03", false);                                                                                                     
                    }
                    else                                                                                                                                                  //랜덤 공격변수가 0이라면(50퍼센트의 확률)
                    {
                        enemyAnim.SetBool("IsAttack01", false);                                                                                               
                        enemyAnim.SetBool("IsAttack02", false);                                                                                                    
                        enemyAnim.SetBool("IsAttack03", true);                                                                                                     //적이 원거리 공격2을 시작한다
                    }
                }
                break;
            case EnemyFSM.Die:                                                                                                                                          //죽음상태
                if (!isDie)
                {
                    enemyAnim.SetTrigger("Die");
                    isDie = true;
                }
                else
                {
                    if (enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Die")
                        && enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                    {
                        gameObject.SetActive(false);
                        GameManager.GM.BossDead();
                    }
                }
                break;
        }
    }
}
