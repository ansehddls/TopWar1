using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    
    public GameObject obstacleFactory;                                                                                                      //프리팹에 있는 장애물을 담을 변수
    float currentTime = 0;                                                                                                                       //현재 시간을 담을 변수
    int createNum;                                                                                                                                //장애물리 생성될 수
    void Start()
    {
        createNum = 25;
    }

    
    void Update()
    {   
        if (GameManager.GM.limitTime <= 0) return;                                                                                      //만약 제한시간이 끝난다면 update문을 종료한다
        CreateObstacle();                                                                                                                          //장애물 생성 메서드
    }

    void CreateObstacle()                                                                                                                         //장애물을 만들 메서드
    {
        currentTime += Time.deltaTime;                                                                                                       //현재 시간에 Time.deltTime을 더해준다
        if (currentTime >= 2)                                                                                                                     //만약 현재 시간이 4초보다 크다면
        {
            for (int i = 0; i < createNum; i++)                                                                                                 //장애물이 생성될 숫자
            {
                float obsPos_x = Random.Range(-24f, 24f);                                                                                  //장애물이 생성될 랜덤 x위치를 정한다
                float obsPos_z = Random.Range(-24f, 24f);                                                                                  //장애물이 생성될 랜덤 z위치를 정한다
                Vector3 obsCreatePos = new Vector3(obsPos_x, 8f, obsPos_z);                                                         //장애물이 생성될 위치를 정한다
                GameObject obstacle = Instantiate(obstacleFactory, obsCreatePos, Quaternion.identity);                        //장애물을 생성한다
                Collider[] obsCol = Physics.OverlapSphere(obsCreatePos, 0.6f);                                                         //장애물 생성 위치에 다른 장애물이 겹치는지 확인한다
                bool isObs = false;                                                                                                                 //주위에 장애물이 있는지 확인할 변수
                if (obsCol.Length >= 1)                                                                                                           //주위에 콜라이더가 있다면
                {
                    isObs = true;                                                                                                                    //장애물이 겹쳐져 있다
                }
                while(isObs)                                                                                                                          //장애물이 겹쳐져 있다면 반복한다
                {
                    obsPos_x = Random.Range(-24f, 24f);                                                                                    //장애물이 생성될 랜덤 x위치를 정한다
                    obsPos_z = Random.Range(-24f, 24f);                                                                                    //장애물이 생성될 랜덤 z위치를 정한다
                    obsCreatePos = new Vector3(obsPos_x, 8f, obsPos_z);                                                               //장애물이 생성될 위치를 정한다
                    obsCol = Physics.OverlapSphere(obsCreatePos, 0.6f);                                                                 //주위에 다른 장애물이 있는지 확인한다
                    if (obsCol.Length >= 1)                                                                                                      //주위에 장애물이 있다면
                    {
                        isObs = true;                                                                                                               //장애물이 겹쳐 있다
                    }
                    else                                                                                                                               //주위에 장애물이 없다면
                    {
                        isObs = false;                                                                                                              //장애물이 겹쳐져 있지 않다
                        obstacle.transform.position = obsCreatePos;                                                                       //장애물의 위치는겹쳐져 있지 않은 생성 위치로 한다
                    }
                }
            }
            currentTime = 0;                                                                                                                      //현재 시간 초기화
        }
    }
}
