using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimator : MonoBehaviourPun
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

        if (!photonView.IsMine)
        {

            Destroy(this);
        }
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
        m_animator.SetBool("bIdle", false);
        m_animator.SetTrigger("tPunch");
       
    }

    public void Dropkick()
    {
        m_animator.SetBool("bIdle", false);
        m_animator.SetTrigger("tDropkick");
       
    }

    public void Dizzy()
    {
        m_animator.SetBool("bIdle", false);
        m_animator.SetTrigger("tDizzy");
        
    }

    public void Down()
    {
        m_animator.SetBool("bIdle", false);
        m_animator.SetTrigger("tDown");
        
    }

    public void Recover()
    {
        m_animator.SetBool("bIdle", false);
        m_animator.SetTrigger("tRecover");
        
    }

    public void Idle()
    {
       
        m_animator.SetBool("bIdle",true);
        

       
    }

   
}
