using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimator : MonoBehaviour
{
    private PlayerController m_playerController;
    private Player m_player;
    private Animator m_animator;
    

    private float m_x;
    private float m_y;
    private float m_z;
    private float m_fowardVelocity;



    private void Start()
    {
        m_playerController = GetComponent<PlayerController>();
        m_player = GetComponent<Player>();
        m_animator = GetComponentInChildren<Animator>();
        
       
    }

    public void Move()
    {
        
        m_animator.SetBool("bMove", true);
        m_x = m_player.m_rigidbody.velocity.x * m_player.transform.forward.x;
        m_z = m_player.m_rigidbody.velocity.z * m_player.transform.forward.z;
        m_fowardVelocity = m_x + m_z;
        m_animator.SetFloat("fMoveFB", m_fowardVelocity);
        
    }

    public void Punch()
    {
        m_animator.SetTrigger("bPunch");
       
    }

    public void Dropkick()
    {
        m_animator.SetTrigger("bDropkick");
       
    }

    public void Dizzy()
    {
        m_animator.SetTrigger("bDizzy");
        
    }

    public void Down()
    {
        m_animator.SetTrigger("bDown");
        
    }

    public void Recover()
    {
        m_animator.SetTrigger("bRecover");
        
    }

    public void Idle()
    {
        m_animator.SetTrigger("bIdle");
    }

   
}
