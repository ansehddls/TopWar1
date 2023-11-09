using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControll : MonoBehaviour
{
    public GameObject rangeparticle;                                                                                      //운석이 떨어질때 범위를 표시할 파티클
    float obstacleMove;                                                                                                       //장애물의 움직이는 속도
    int obstaclePower;                                                                                                         //장애물의 공격력
    GameObject range;

    void Start()
    {
        obstacleMove = 3f;                                                                                                   //장애물의 움직이는 속도는 3이다
        obstaclePower = 50;                                                                                                  //장애물의 공격력은 50이다
        range = Instantiate(rangeparticle);                                                                                 //범위 파티클 생성
        range.transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);               //운석 위치의 높이 0값에 생성
    }

    
    void Update()
    {
        transform.position += Vector3.down * obstacleMove * Time.deltaTime;                                  //장애물은 아래 방향으로 초당 2씩 떨어진다
    }

    private void OnTriggerEnter(Collider other)                                                                         // 장애물과 부딪힌 콜라이더
    {
        if (other.gameObject.tag == "Floor")                                                                             //부딪힌 콜라이더의 태그가 바닥이라면
        {
            Destroy(gameObject);                                                                                            //장애물을 파괴한다
            Destroy(range);                                                                                                    //범위 파티클을 파괴한다
        }
        else if (other.gameObject.tag == "Player")                                                                      //만약 부딪힌 콜라이더의 태그가 플레이어 라면
        {
            GameManager.GM.PlayerDamaged(obstaclePower);                                                       //플레이어가 데미지를 받는 메서드
            Destroy(gameObject);                                                                                             //장애물을 파괴한다
            Destroy(range);                                                                                                     //범위 파티클을 파괴한다
        }
    }
}
