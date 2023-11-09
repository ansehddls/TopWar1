using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;                                                                                                //타겟의 위치를 가져온다
    Vector3 offSet;                                                                                                            //간격을 담을 Vector3변수
    void Start()
    {
        offSet = transform.position - target.position;                                                                 //간격은 카메라의 위치에서 플레이어의 위치를 뺀 값
    }

    void Update()
    {
        transform.position = target.position + offSet;                                                                //카메라의 위치는 타겟의 위치에서 간격을 더한다
    }

    void LateUpdate()                                                                                                       //카메라가 벽을 뚫고 가는것을 방지하기 위해 사용한다
    {
        if (transform.position.z <= -24f)                                                                                 //카메라의 위치의 Z값이 -24보다 작다면
        {
            transform.position = new Vector3(target.position.x, transform.position.y, -24f);                 //카메라의 위치는 타겟의 x값, 카메라의 y값, z값은 -24로 고정한다
            transform.forward = (target.position - transform.position).normalized;                            //카메라가 바라보는 방향은 플레이어로 고정한다
        }
    }

}
