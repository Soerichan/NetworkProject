using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem attackParticle;
    public ParticleSystem takeHitParticle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        { 
            attackParticle.Play();
        }
    }

}
