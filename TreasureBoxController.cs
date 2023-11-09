using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxController : MonoBehaviour
{
    
    Animator trBoxAnim;                                                                                    //보물 상자의 애니메이션을 담을 변수
    void Start()
    {
        trBoxAnim = GetComponentInChildren<Animator>();                                                   //보물 상자의 애니메이션을 가져온다
    }

    
    void Update()
    {
        if (StageThreeManager.TBM.trBoxHp <= 0)                                                  //만약 보물상자의 체력이 0보다 작다면
        {
            StageThreeManager.TBM.isDie = true;                                                    //보물상자는 죽은 상태이다
            trBoxAnim.SetTrigger("Open");                                                               //보물상자는 애니메이션을 실행한다
            if (trBoxAnim.GetCurrentAnimatorStateInfo(0).IsName("Open") && trBoxAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                //만약 지금 플레이중인 애니메이션의 이름이open이고 오픈의 애니메이션이 다 진행됐다면 
            {
                Destroy(gameObject);                                                                      //게임오브젝트를 파괴한다
            }
        }
    }

}
