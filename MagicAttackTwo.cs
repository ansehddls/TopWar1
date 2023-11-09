using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackTwo : MonoBehaviour
{
    
    int effectAttack = 35;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-21, 21), 0.01f, Random.Range(-21, 21));
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = Vector3.one * 3f;
        box.center = Vector3.zero;
        box.isTrigger = true;
        
    }
    void Update()
    {
        if (GameManager.GM.isEnd)
        {
            Destroy(gameObject);
        }
        if (!gameObject.GetComponent<ParticleSystem>().isPlaying)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            GameManager.GM.PlayerDamaged(effectAttack);
        }
    }


}
