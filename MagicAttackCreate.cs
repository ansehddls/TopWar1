using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackCreate : MonoBehaviour
{
    public GameObject magicAttack;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void StartAttack()
    {
        Instantiate(magicAttack, transform.position, Quaternion.identity);
    }
}
