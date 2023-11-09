using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    public GameObject createParticle;                                                                                              //코인 생성시 발생할 파티클
    
    void Start()
    {
        Instantiate(createParticle, transform.position, Quaternion.identity);
    }


    void Update()
    {
        transform.Rotate(Vector3.up * 180f * Time.deltaTime);                                                                //코인의 위치가 180도 돌아간다 초당
    }

    private void OnTriggerEnter(Collider other)                                                                                 //콜라이더가 부딪힌다면 발생하는 메서드
    {
        if (other.transform.name == "Player")                                                                                     //만약 부딪힌 콜라이더의 이름이 플레이어라면
        {
            GameManager.GM.coinCount++;                                                                                      //게임메니저의 코인 카운트 +1
            Destroy(gameObject);                                                                                                     //오브젝트는 파괴한다
        }
    }
}
