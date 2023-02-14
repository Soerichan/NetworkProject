using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public Collider m_collider;
    private LayerMask m_mask;

    void Start()
    {       
        m_collider = GetComponent<Collider>();
        m_mask = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
       if (other.gameObject.layer != m_mask)
            return;

       PlayerController player= other.gameObject.GetComponent<PlayerController>();
       player.Die();
    }
}
