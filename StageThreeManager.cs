using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StageThreeManager : MonoBehaviour
{
    public static StageThreeManager TBM;                                                                              //싱글턴을 만들기 위한 변수
    private void Awake()
    {
        if (TBM == null)
        {
            TBM = this;
        }
        else
        {
            Destroy(TBM);
        }
    }                                                                                                      //싱글톤을 만듬
    public GameObject trBox;                                                                                                //프리팹에 있는 게임오브젝트를 담을 변수
    public GameObject coinPref;                                                                                            //프리팸에 있는 코인을 가져온다
    public bool isDie;                                                                                                           //보물상자가 죽었는지 확인할 변수
    public int trBoxHp = 120;                                                                                                //보물 상자의 체력을 담을 변수
    bool isCoin = false;                                                                                                        //코인 생성 제한 변수
    
    void Start()
    {
        Instantiate(trBox);                                                                                                          //trBox 프리팹에 가지고 있는 transfom컴포넌트 대로 생성된다
        isDie = false;                                                                                                                //보물상자는 죽지 않았다
        GameManager.GM.coinNum = 25;                                                                                    //코인이 생성될 숫자
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)                                                                                                                     //만약 보물상자가 죽었다면
        {
            if (!isCoin)                                                                                                              //코인생성이 시작되었다면
            {
                for (int i = 0; i < GameManager.GM.coinNum; i++)                                                      //코인넘버 만큼 반복
                {
                    float randCoinPosX = Random.Range(-24f, 24f);                                                       //코인을 생성할 랜덤 x값
                    float randCoinPosZ = Random.Range(-24f, 24f);                                                       //코인을 생성할 랜덤 z값
                    Vector3 coinPos = new Vector3(randCoinPosX, 6, randCoinPosZ);                                //코인을 생성할 랜덤위치
                    RaycastHit hitinfo;                                                                                             //레이를 쐈을 때 맞은 물체를 담을 변수
                    bool isFloor = false;                                                                                          //바닥에 맞지 않았다
                    if (Physics.SphereCast(coinPos, 0.6f, Vector3.down, out hitinfo))                                   //만약 레이를 랜덤위치에서 아래로 쏴서 맞았다면
                    {
                        if (hitinfo.transform.tag == "Floor")                                                                    //맞은 물체가 바닥인지 아닌지 확인
                        {
                            isFloor = true;                                                                                          //바닥에 닿았다
                        }
                        else
                        {
                            isFloor = false;
                        }
                    }
                    while (!isFloor)                                                                                                     //바닥에 맞지 않았다면 반복해라
                    {
                        randCoinPosX = Random.Range(-24f, 24f);                                                              //코인을 생성할 랜덤 x값
                        randCoinPosZ = Random.Range(-24f, 24f);                                                              //코인을 생성할 랜덤 z값
                        coinPos = new Vector3(randCoinPosX, 6, randCoinPosZ);                                           //코인을 생성할 랜덤위치
                        if (Physics.SphereCast(coinPos, 0.6f, Vector3.down, out hitinfo))                                   //만약 레이를 랜덤위치에서 아래로 쏴서 맞았다면
                        {
                            if (hitinfo.transform.tag == "Floor")                                                                   //맞은 물체가 바닥인지 아닌지 확인
                            {
                                isFloor = true;                                                                                          //바닥에 닿았다
                            }
                            else                                                                                                           //태그가 floor이 아니라면
                            {
                                isFloor = false;                                                                                         //바닥에 닿지 않았다
                            }
                        }
                    }
                    coinPos.y = 0;
                    Instantiate(coinPref, coinPos, Quaternion.identity);                                                        //코인을 생성해라
                }
                isCoin = true;                                                                                                     //코인 생성 제한
            }
        }
    }
}
