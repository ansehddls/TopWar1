using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackColControll : MonoBehaviour
{

    public Collider enemyAttackCol;                                                                                             //에너미의 공격 콜라이더를 가져온다
    // Start is called before the first frame update
    void Start()
    {
        enemyAttackCol.enabled = false;                                                                                        //에너미의 공격 콜라이더를 끈다                              
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartCol()                                                                                                                    //에너미가 플레이어를 공격할 때 킬 메서드
    {
        enemyAttackCol.enabled = true;                                                                                         //에너미의 공격 콜라이더를 킨다
    }

    void EndCol()                                                                                                                    //에너미가 플레이어를 공격할 때 끌 메서드
    {
        enemyAttackCol.enabled = false;                                                                                         //에너미의 공격 콜라이더를 끈다
    }
}
