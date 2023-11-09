using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackTwoCreate : MonoBehaviour
{
    public GameObject fireParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ParticleCreate()
    {
        Instantiate(fireParticle);
        Instantiate(fireParticle);
        Instantiate(fireParticle);
        Instantiate(fireParticle);
        Instantiate(fireParticle);
        fireParticle.transform.localScale = Vector3.one * 3f;
    }
}
