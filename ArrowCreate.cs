using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCreate : MonoBehaviour
{
    public GameObject ArrowFactory;                                                                                     //화살을 만들어낼 공장
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartCol()
    {
        GameObject Arrow = Instantiate(ArrowFactory, GetComponentInParent<Transform>().position + Vector3.up, GetComponentInParent<Transform>().rotation);
        Arrow.transform.forward = GetComponentInParent<Transform>().forward;
    }

}
