using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControll : MonoBehaviour
{
    float arrowMoveSpeed = 5f;                                                                                                                      //화살의 이동 속도
    float arrowTime;                                                                                                                                     //화살이 없어질 변수
    void Update()
    {
        ArrowMove();                                                                                                                                        //화살이 이동할 메서드
    }

    void ArrowMove()                                                                                                                                      //화살이 이동할 메서드
    {
        arrowTime += Time.deltaTime;                                                                                                                  //시간을 더해준다
        transform.localPosition += transform.forward.normalized *  arrowMoveSpeed * Time.deltaTime;                              //화살의 이동
        if (arrowTime >= 4f)                                                                                                                               //에로우 타임이 4초보다 커진다면
        {
            Destroy(gameObject);                                                                                                                          //화살 파괴
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")                                                                                                      //부딪힌 게임오브젝트의 이름이 플레이어라면
        {
            GameManager.GM.PlayerDamaged(EnemyManager.EM.enemyAttackPower);                                                    //플레이어가 데미지를 받는 메서드
            Destroy(gameObject);                                                                                                                         //화살을 파괴한다
        }
    }
}
