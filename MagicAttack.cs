using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{

    float magicAttackSpeed = 5f;                                                                                                                                //투사체의 날아가는 속도는 3이다
    int magicDamage = 40;                                                                                                                                       //마법 공격 데미지
    float attackTime = 0f;                                                                                                                                          // 공격 유지시간

    void Start()
    {
        
    }

    
    void Update()
    {
        if (GameManager.GM.isEnd)
        {
            Destroy(gameObject);
        }
        //정면이 플레이어를 향해서 날라간다
        attackTime += Time.deltaTime;                                                                                                                            //시간을 더해준다
        if (attackTime >= 6f)                                                                                                                                         //6초가 지나면
        {
            Destroy(gameObject);                                                                                                                                    //게임 오브젝트를 파괴한다
        }
        Vector3 targ = (EnemyManager.EM.playerTr.position - transform.position).normalized;                                                     //플레이어를 향해 방향을 구한다                                                                                                                                                   //마법 공격의 위치 y값은 1이다
        transform.position += targ * magicAttackSpeed * Time.deltaTime;                                                                              //플레이어에게 1초마다 3M이동한다

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")                                                                                                                    //만약 부딪힌 콜라이더의 태그가 플레이어라면
        {
            Destroy(gameObject);                                                                                                                                    //이 게임 오브젝트를 파괴한다
            GameManager.GM.PlayerDamaged(magicDamage);                                                                                              //플레이어가 데미지를 입는다
        }
        else if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall")                                                                 //벽이나 바닥에 닿는다면
        {
            Destroy(gameObject);                                                                                                                                    //이 게임 오브젝트를 파괴한다
        }
    }
}
