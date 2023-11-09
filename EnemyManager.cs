using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager EM;                                                                                           //에너미 메니저를 싱글톤

    void Awake()
    {
        if (EM == null)
        {
            EM = this;
        }
        else
        {
            Destroy(EM);
        }
        CreateEnemyNum();                                                                                                       //에너미가 생성될 숫자 메서드
    }                                                                                                                   //에너미 매니저를 싱클톤 시킨다


    public Transform playerTr;                                                                                                    //플레이어의 위치값을 받아올 변수
    public float enemyMoveSpeed;                                                                                             //에너미 무브 스피드
    public float distance;                                                                                                           //적과 플레이어 사이의 길이값
    public int enemyAttackPower;                                                                                               //에너미의 공격력을 담을 변수
    public GameObject[] enemyPrefab;                                                                                         //에너미 프리팹
    bool isFloor;                                                                                                                      //바닥에 맞았는지 확인할 변수
    public int createNum;                                                                                                           //스테이지 1에서 에너미가 생성될 숫자
    public GameObject createParticle;                                                                                          //에너미가 생성될 때 사용할 파티클
    public GameObject boss;
    

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Stage05")
        {
            EnemyCreate();                                                                                                          //에너미 생성 메서드
        }
        else
        {
            boss = Instantiate(enemyPrefab[3]);                                                                                          //에너미 D를 소환한다
           GameObject bossParticle = Instantiate(createParticle, Vector3.forward * 6f, Quaternion.identity);
            bossParticle.transform.localScale = Vector3.one * 7f;
        }
       
    }

    void Update()
    {

    }
    public void EnemyMove(Transform enemyPos)                                                                           //에너미의 이동을 시키는 메서드
    {
        Vector3 enemyDir = (playerTr.position - enemyPos.position).normalized;                                      //에너미와 플레이어와의 방향을 계산한다
        enemyPos.forward = enemyDir;                                                                                          //에너미는 플레이어를 바라본다
        enemyPos.position += enemyDir * enemyMoveSpeed * Time.deltaTime;                                      //에너미는 초당 무브스피드만큼 움직인다
    }

    public float Distance(Vector3 Pos)
    {
        Vector3 dist = playerTr.position - Pos;                                                                                 //플레이어와 적 사이의 벡터값을 구한다
        distance = dist.magnitude;                                                                                                //플레이어와 적 사이의 길이값을 구한다
        return distance;                                                                                                              //플레이어와 적 사이의 길이값을 반환한다
    }                                                                                         //플레이어와의 거리값을 나타내는 메서드

    public void AttackRotation(Transform enemyPos)                                                                       //공격시 회전하는 메서드
    {
        Vector3 attackDir = (playerTr.position - enemyPos.position).normalized;                                      //플레이어와의 방향값 계산
        enemyPos.forward = attackDir;                                                                                          //공격시 플레이어를 바라본다
    }

    void EnemyCreate()
    {
        for (int i = 0; i < createNum; i++)                                                                                        // 25번 반복
        {
            float randCreateX = Random.Range(-24f, 24f);                                                                    //에너미가 생성될 랜덤 x값
            float randCreateZ = Random.Range(-24f, 24f);                                                                    //에너미가 생성될 랜덤 z값
            Vector3 createPos = new Vector3(randCreateX, 6, randCreateZ);                                             //랜덤 값을 위치를 생성
            RaycastHit Hitinfo;                                                                                                        //레이의 맞은 물체의 정보를 담을 변수
            isFloor = false;                                                                                                            //바닥에 맞지 않았다
            if (Physics.SphereCast(createPos, 1f, Vector3.down, out Hitinfo, 7f))                                         //만약 레이를 쏴서 맞았다면
            {
                if (Hitinfo.collider.gameObject.name == "Floor")                                                             //맞은 콜라이더의 게임 오브젝트 이름이 플로어라면
                {
                    isFloor = true;                                                                                                     //바닥에 맞았다
                }
                else                                                                                                                      //레이에 맞은게 플로어가 아니라면
                {
                    while (!isFloor)                                                                                                    //바닥에 맞을 때까지 반복
                    {
                        randCreateX = Random.Range(-24f, 24f);                                                               //에너미가 생성될 랜덤 x값
                        randCreateZ = Random.Range(-24f, 24f);                                                               //에너미가 생성될 랜덤 z값
                        createPos = new Vector3(randCreateX, 6, randCreateZ);                                            //랜덤 값을 위치를 생성
                        if (Physics.SphereCast(createPos, 1f, Vector3.down, out Hitinfo, 7f))                             //만약 레이를 쏴서 맞았다면
                        {
                            if (Hitinfo.collider.gameObject.name == "Floor")                                                 //맞은 콜라이더의 게임 오브젝트 이름이 플로어라면
                            {
                                isFloor = true;                                                                                          //바닥에 맞았다
                            }
                            else                                                                                                          //맞은 콜라이더의 이름이 플로어가 아니라면
                            {
                                isFloor = false;                                                                                        //바닥에 맞지 않았다
                            }
                        }
                    }
                }
                if (isFloor)                                                                                                              ///바닥에 닿았다면
                {
                    int randType = Random.Range(0, 10);                                                                        //확률에 따라 에너미를 생성
                    createPos.y = 1f;                                                                                                  //위치의 Y값을 1으로 바꾼다
                    if (randType <= 4)                                                                                                // 50퍼센트의 확률
                    {
                        Instantiate(enemyPrefab[0], createPos, Quaternion.identity);                                       //에너미 A를 생성한다
                        Instantiate(createParticle, new Vector3(createPos.x, 0, createPos.z), Quaternion.identity);   //에너미가 생성될때 파티클을 실행한다
                    }
                    else if (randType == 9)                                                                                          //10퍼센트의 확률로 
                    {
                        Instantiate(enemyPrefab[2], createPos, Quaternion.identity);                                        //에너미 C를 생성한다
                        Instantiate(createParticle, new Vector3(createPos.x, 0, createPos.z), Quaternion.identity);   //에너미가 생성될때 파티클을 실행한다
                    }
                    else                                                                                                                   //40퍼센트의 확률로 
                    {
                        Instantiate(enemyPrefab[1], createPos, Quaternion.identity);                                        //에너미 B를 생성한다
                        Instantiate(createParticle, new Vector3(createPos.x, 0, createPos.z), Quaternion.identity);   //에너미가 생성될때 파티클을 실행한다
                    }
                }

            }
        }
    }                                                                                                           //에너미를 생성하는 메서드

    void CreateEnemyNum()
    {
        if (SceneManager.GetActiveScene().name == "Stage01")
        {
            createNum = 25;                                                                                                         //스테이지 1에서 에너미가 생성될 숫자
        }
        else if (SceneManager.GetActiveScene().name == "Stage04")                                                    //스테이지 4에서 에네미가 생성될 숫자
        {
            createNum = 50;
        }
    }                                                                                                     //스테이지마다 에너미의 숫자를 조정할 메서드

}
