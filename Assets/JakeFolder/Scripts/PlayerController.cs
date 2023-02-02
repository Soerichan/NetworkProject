//using Photon.Pun;
//using Photon.Pun.Demo.Asteroids;
//using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Idle, Punch, Dropkick, Dizzy, Down, Recover, Run };


public class PlayerController : MonoBehaviour//Pun
{
    private Player m_player;
    private PlayerAnimator m_playerAnimator;

    [SerializeField]
    public float m_fPuchTime;
    [SerializeField]
    public float m_fDropkickTime;
    [SerializeField]
    public float m_fDizzyTime;
    [SerializeField]
    public float m_fDownTime;
    [SerializeField]
    public float m_fRecoverTime;

    State m_state = State.Idle;

    private Coroutine m_coroutinePunch;
    private Coroutine m_coroutineDropkick;
    private Coroutine m_coroutineDizzy;
    private Coroutine m_coroutineDown;
    private Coroutine m_coroutineRecover;


    private void Awake()
    {
        m_player = GetComponent<Player>();
        m_playerAnimator=GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        //if (!photonView.IsMine)
         //   Destroy(this);
         
    }
    private void Update()
    {

       switch(m_state)
        {
            case State.Idle:
                Accelate();
                Rotate();
                Idle();
               // Punch();
               // Dropkick();
                break;

            case State.Punch:
                
                break;

            case State.Dropkick:
               
                break;

            case State.Dizzy:
                Rotate();
                break;

            case State.Down:
                Rotate();
                break;

            case State.Recover:
                Rotate();
                break;

            case State.Run:
                Accelate();
                Rotate();
                Idle();
              //  Punch();
              // Dropkick();
                break;
        }
       
    }

    private void Accelate()
    {
        float vInput = Input.GetAxis("Vertical");

        m_player.Accelate(vInput);

        m_playerAnimator.Move();
        m_state = State.Run;

    }

    private void Rotate()
    {
        float hInput = Input.GetAxis("Horizontal");

        m_player.Rotate(hInput);
    }

    private void Punch()
    {
        m_playerAnimator.Punch();
        m_state=State.Punch;
        m_coroutinePunch = StartCoroutine(PunchToIdle());
    }

    private void Dropkick()
    {
        m_playerAnimator.Dropkick();
        m_state=State.Dropkick;
        m_coroutineDropkick = StartCoroutine(DropkickToIdle());
    }

    public void Dizzy()
    {
        m_playerAnimator.Dizzy();
        m_state=State.Dizzy;
        m_coroutineDizzy = StartCoroutine(DizzyToIdle());
    }

    public void Down()
    {
        m_playerAnimator.Down();
        m_state=State.Down;
        m_coroutineDown = StartCoroutine(DownToIdle());
    }

    private void Recover()
    {
        m_playerAnimator.Recover();
        m_state=State.Recover;
        m_coroutineRecover = StartCoroutine(RecoverToIdle());
    }

    private void Idle()
    {
        if(m_player.m_rigidbody.velocity==new Vector3(0,0,0))
        {
            m_playerAnimator.Idle();
            m_state = State.Idle;
        }
        
    }
    private IEnumerator PunchToIdle()
    {

        yield return new WaitForSeconds(m_fPuchTime);
       m_state = State.Idle;
        m_playerAnimator.Idle();
    }

    private IEnumerator DropkickToIdle()
    {
        yield return new WaitForSeconds(m_fDropkickTime);
        m_state = State.Idle;
        m_playerAnimator.Idle();
    }

    private IEnumerator DizzyToIdle()
    {
        yield return new WaitForSeconds(m_fDizzyTime);
        m_state = State.Idle;
        m_playerAnimator.Idle();
    }

    private IEnumerator DownToIdle()
    {
        yield return new WaitForSeconds(m_fDownTime);
        m_state = State.Recover;
        m_playerAnimator.Recover();
        Recover();
    }

    private IEnumerator RecoverToIdle()
    {
        yield return new WaitForSeconds(m_fRecoverTime);
        m_state = State.Idle;
        m_playerAnimator.Idle();
    }
}
