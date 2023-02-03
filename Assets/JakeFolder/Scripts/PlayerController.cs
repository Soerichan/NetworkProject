//using Photon.Pun;
//using Photon.Pun.Demo.Asteroids;
//using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    
    [SerializeField]
    public float m_fPunchRange;
    [SerializeField]
    public float m_fPunchAngle;

    [SerializeField]
    public float m_fDropkickRange;



    State m_state = State.Idle;

    private Coroutine m_coroutinePunch;
    private Coroutine m_coroutineDropkick;
    private Coroutine m_coroutineDizzy;
    private Coroutine m_coroutineDown;
    private Coroutine m_coroutineRecover;

    private LayerMask m_maskPlayer;
    private PlayerController m_playerOpponent;
    public float m_fTeamNumber;

    private void Awake()
    {
        m_player = GetComponent<Player>();
        m_playerAnimator=GetComponent<PlayerAnimator>();
        m_maskPlayer= LayerMask.GetMask("Player");
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
               // Idle();
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
               // Idle();
                //Punch();
                //Dropkick();
                break;
        }
       
    }

    public void Accelate()
    {
        float vInput = Input.GetAxis("Vertical");

        m_player.Accelate(vInput);

        m_playerAnimator.Move();
        m_state = State.Run;

    }

    public void Rotate()
    {
        float hInput = Input.GetAxis("Horizontal");

        m_player.Rotate(hInput);
    }

    public void Punch()
    {
       
            m_playerAnimator.Punch();
            m_state = State.Punch;
            m_coroutinePunch = StartCoroutine(PunchToIdle());
             PunchJudgment();
            Debug.Log("펀치");
            
        
    }

    public void PunchJudgment()
    {
        // 1. 범위내에 있는가
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fPunchRange,m_maskPlayer);

        Debug.Log("펀치저지먼트");

        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log("포문");
            m_playerOpponent = colliders[i].GetComponent<PlayerController>();

            if (m_playerOpponent != this&&m_playerOpponent.m_fTeamNumber!=this.m_fTeamNumber)
            {
                Debug.Log("이프문");
                break;
            }
        }
            

            Vector3 dirToTarget = (m_playerOpponent.transform.position - transform.position).normalized;

            // 2. 각도내에 있는가
            if (Vector3.Dot(transform.forward, dirToTarget) > Mathf.Cos(m_fPunchAngle * 0.5f * Mathf.Deg2Rad))
             {
            
            m_playerOpponent.Punched();
             }

          
        
    }


    public void Punched()
    {
        Dizzy();
        
    }


    

    public void Dropkick()
    {
        
            m_playerAnimator.Dropkick();
            m_state = State.Dropkick;
            m_coroutineDropkick = StartCoroutine(DropkickToIdle());
          
    }

    public void Dizzy()
    {
        if(m_state != State.Dizzy)
        {
        m_coroutineDizzy = StartCoroutine(DizzyToIdle());
        m_state=State.Dizzy;

        }
        Debug.Log("디지");
        m_playerAnimator.Dizzy();
    }

    public void Down()
    {
        m_playerAnimator.Down();
        m_state=State.Down;
        m_coroutineDown = StartCoroutine(DownToIdle());
    }

    public void Recover()
    {
        m_playerAnimator.Recover();
        m_state=State.Recover;
        m_coroutineRecover = StartCoroutine(RecoverToIdle());
    }

    public void Idle()
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
        Debug.Log("디지투아이틀시작");
        yield return new WaitForSeconds(m_fDizzyTime);
        Debug.Log("디지투아이틀끝");
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_fPunchRange);
    }
}
