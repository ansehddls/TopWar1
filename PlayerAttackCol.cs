using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class PlayerAttackCol : MonoBehaviour
{
    int attackPower;                                                                                                                      //플레이어의 공격력을 담을 변수
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyA")                                                                                 //만약 플레이어의 태그가 에너미 A라면
        {
            
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);                                            //이펙트 생성
            attackPower = Random.Range(30, 90);                                                                               //플레이어의 공격력은 30~90 랜덤이다
            other.gameObject.GetComponent<EnemyController>().enemyAHp -= attackPower;                      //에너미 B의 체력에서 플레이어의 공격력만큼 감소시킨다
        }
        else if (other.gameObject.tag == "EnemyB")                                                                            //만약 플레이어의 태그가 에너미 B라면
        {
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);                                           //이펙트 생성
            attackPower = Random.Range(30, 90);                                                                               //플레이어의 공격력은 30~90 랜덤이다
            other.gameObject.GetComponent<EnemyController>().enemyBHp -= attackPower;                      //에너미 B의 체력에서 플레이어의 공격력만큼 감소시킨다
        }
        else if (other.gameObject.tag == "EnemyC")                                                                            //만약 플레이어의 태그가 에너미C 라면
        {
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);                                           //이펙트 생성
            attackPower = Random.Range(30, 90);                                                                               //플레이어의 공격력은 30~90 랜덤이다
            other.gameObject.GetComponent<EnemyController>().enemyCHp -= attackPower;                      //에너미 B의 체력에서 플레이어의 공격력만큼 감소시킨다
        }
        else if (other.gameObject.tag == "EnemyD")                                                                            //만약 플레이어의 태그가 에너미D 라면
        {
            GameObject hitEff = Instantiate(hitEffect, other.transform.position, Quaternion.identity);                //이펙트 생성
            hitEff.transform.localScale = Vector3.one * 4;
            attackPower = Random.Range(30, 90);                                                                               //플레이어의 공격력은 30~90 랜덤이다
            other.gameObject.GetComponent<EnemyController>().enemyDHp -= attackPower;                      //에너미 B의 체력에서 플레이어의 공격력만큼 감소시킨다
        }
        else if (other.gameObject.tag == "TreasureBox")
        {
            GameObject hitEff = Instantiate(hitEffect, other.transform.position, Quaternion.identity);                //이펙트 생성
            hitEff.transform.localScale = Vector3.one*4;
            attackPower = Random.Range(30, 90);                                                                               //플레이어의 공격력은 30~90 랜덤이다
            StageThreeManager.TBM.trBoxHp -= attackPower;                                                               //보물상자의 체력에서 플레이어의 공격력만큼 감소시킨다
            print(StageThreeManager.TBM.trBoxHp);
        }
    }
}
