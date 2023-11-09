using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)                                                                                                 //콜라이더가 부딪혔는지 확인하는 메서드
    {
        if (other.gameObject.name == "Player")                                                                                                  //만약 부딪힌 콜라이더가 플레이어라면
        {
            GameManager.GM.PlayerDamaged(EnemyManager.EM.enemyAttackPower);                                               //플레이어가 공격을 받는 메서드
        }
    }

}
