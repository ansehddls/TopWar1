using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColControll : MonoBehaviour
{
    public Collider playerAttackCol;                                                            //플레이어의 공격 콜라이더를 담을 변수
    
    void Start()
    {
        playerAttackCol.enabled = false;                                                       //플레이어의 공격 콜라이더를 끈다
    }

    void StartCol()
    {
        playerAttackCol.enabled = true;                                                       //플레이어의 공격 콜라이더를 켠다
    }

    void EndCol()
    {
        playerAttackCol.enabled = false;                                                       //플레이어의 공격 콜라이더를 끈다
    }

    void Update()
    {
        
    }
}
