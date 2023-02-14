using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Jake;
using Unity.VisualScripting;
using ObjectPool;
using System.Globalization;

public enum State { Idle, Punch, Dropkick, Dizzy, Down, Recover, Run ,Die};


public class PlayerController : MonoBehaviourPun
{
    [Header("Player")]
    public  Player           m_player;
    private PlayerAnimator   m_playerAnimator;
    public  GameObject       m_gFX;
    public  Transform        m_respawnPosition;
    State                    m_state = State.Idle;
    public  float            m_fTeamNumber;
    private PlayerController m_playerOpponent;
    private LayerMask        m_maskPlayer;
    public  Jake.MasterGroundChecker m_masterGroundChecker;
    


    [Header("Config")]
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
    public float m_fRespawnTime;
    
    [SerializeField]
    public float m_fPunchRange;
    [SerializeField]
    public float m_fPunchAngle;
    [SerializeField]
    public float m_fPunchPower;

    [SerializeField]
    public float m_fDropkickRange;
    [SerializeField]
    public float m_fDropkickAngle;
    [SerializeField]
    public float m_fDropkickPower;

    public GameObject[] StartPos=new GameObject[2];


    [Header("Coroutine")]
    private Coroutine m_coroutinePunch;
    private Coroutine m_coroutineDropkick;
    private Coroutine m_coroutineDizzy;
    private Coroutine m_coroutineDown;
    private Coroutine m_coroutineRecover;
    private Coroutine m_coroutineHPRegen;
    private Coroutine m_respawnCoroutine;


    [Header("Manager")]
    public PoolManager      m_poolManager;
    public SoundManager     m_soundManager;
    public ParticleManager  m_particleManager;
    public CubeManager      m_cubeManager;



    private void Awake()
    {
        m_player            = GetComponent<Player>();
        m_playerAnimator    = GetComponent<PlayerAnimator>();
        m_maskPlayer        = LayerMask.NameToLayer("Player");
    }

    private void Start()
    {
        m_cubeManager       = GameObject.Find("Map").GetComponent<CubeManager>();
        m_poolManager       = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        m_respawnPosition   = GameObject.Find("RespawnPosition").transform;
        
        m_fTeamNumber       = PhotonNetwork.LocalPlayer.GetPlayerNumber() % 2 == 0 ? 0 : 1;
        
        StartPos            = new GameObject[2];
        StartPos[0]         = GameObject.Find("YellowStartPos").GameObject();
        StartPos[1]         = GameObject.Find("BlueStartPos").GameObject();
        m_respawnPosition   = StartPos[(int)m_fTeamNumber].transform;

    }
    private void Update()
    {
        if (photonView.IsMine != true)
            return;

        switch (m_state)
        {
            case State.Idle:
                Accelate();
                Rotate();              
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
                break;

            case State.Die:
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
        m_soundManager.attackSound.Play();
        m_particleManager.attackParticle.Play();
      
        if (photonView.IsMine != true)
            return;

        m_playerAnimator.Punch();
        m_state = State.Punch;
        m_coroutinePunch = StartCoroutine(PunchToIdle());
          
        photonView.RPC("PunchJudgment", RpcTarget.MasterClient, transform.position, transform.forward,m_fTeamNumber);
            

            


    }

    [PunRPC]
    public void PunchJudgment(Vector3 vec,Vector3 forward , float team)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 1. 범위내에 있는가
            Collider[] colliders = Physics.OverlapSphere(vec, m_fPunchRange, m_maskPlayer);
            if (colliders.Length==0)
            {
                return;
            }

                for (int i = 0; i < colliders.Length; i++)
                 {
               
                m_playerOpponent = colliders[i].GetComponent<PlayerController>();

                     if (m_playerOpponent.m_fTeamNumber != team)
                         break;
                     
                 }

            Vector3 dirToTarget = (m_playerOpponent.transform.position - vec).normalized;

            // 2. 각도내에 있는가
            if (Vector3.Dot(forward, dirToTarget) > Mathf.Cos(m_fPunchAngle * 0.5f * Mathf.Deg2Rad))
            {
                m_playerOpponent.Punched(dirToTarget);
            }
        }    
    }


    public void Punched(Vector3 vec)
    {
        m_soundManager.takeHitSound.Play();    
        m_particleManager.takeHitParticle.Play();

        if (photonView.IsMine != true)
            return;

        Dizzy();
        m_player.Hurt();
        m_player.m_rigidbody.AddForce(vec*m_fPunchPower, ForceMode.Impulse);
    }


    

    public void Dropkick()
    {
        m_soundManager.attackSound.Play();
        m_particleManager.attackParticle.Play();

        if (photonView.IsMine != true)
            return;

        m_playerAnimator.Dropkick();
        m_state = State.Dropkick;
        m_coroutineDropkick = StartCoroutine(DropkickToIdle());
           
        photonView.RPC("DropkickJubgment", RpcTarget.MasterClient, transform.position, transform.forward, m_fTeamNumber);
    }

    [PunRPC]
    public void DropkickJubgment(Vector3 vec, Vector3 forward, float team)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 1. 범위내에 있는가
            Collider[] colliders = Physics.OverlapSphere(vec, m_fDropkickRange, m_maskPlayer);
            if (colliders.Length == 0)
            {
                return;
            }

            for (int i = 0; i < colliders.Length; i++)
            {

                m_playerOpponent = colliders[i].GetComponent<PlayerController>();

                if (m_playerOpponent.m_fTeamNumber != team)
                {

                    break;
                }
            }

            // 2. 각도내에 있는가
            Vector3 dirToTarget = (m_playerOpponent.transform.position - vec).normalized;
            if (Vector3.Dot(forward, dirToTarget) > Mathf.Cos(m_fDropkickAngle * 0.5f * Mathf.Deg2Rad))
            {
                m_playerOpponent.Dropkicked(dirToTarget);                
            }
        }
    }

    public void Dropkicked(Vector3 vec)
    {
        m_soundManager.takeHitSound.Play();
        m_particleManager.takeHitParticle.Play();

        if (photonView.IsMine != true)
            return;

        Down();
        m_player.Hurt();
        transform.forward = -vec;
        m_player.m_rigidbody.AddForce(vec * m_fDropkickPower, ForceMode.Impulse);
    }

    public void Dizzy()
    {
        if (photonView.IsMine != true)
            return;

        if (m_state != State.Dizzy)
        {
        m_coroutineDizzy = StartCoroutine(DizzyToIdle());
        m_state=State.Dizzy;

        }
        
        m_playerAnimator.Dizzy();
    }

    public void Down()
    {
        if (photonView.IsMine != true)
            return;

        m_playerAnimator.Down();
        m_state=State.Down;
        m_coroutineDown = StartCoroutine(DownToIdle());
    }

    public void Recover()
    {
        if (photonView.IsMine != true)
            return;

        m_playerAnimator.Recover();
        m_state=State.Recover;
        m_coroutineRecover = StartCoroutine(RecoverToIdle());
    }

    public void Idle()
    {
        if (photonView.IsMine != true)
            return;

        if (m_player.m_rigidbody.velocity==new Vector3(0,0,0))
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


    public void GroundCheckCall(Collider other)
    {
        if (photonView.IsMine != true)
            return;
      
        GroundColorChange newGroundColorChange = other.gameObject.GetComponent<GroundColorChange>();
        float number = newGroundColorChange.m_fNumber;
        photonView.RPC("GroundCheckJudgment", RpcTarget.MasterClient, number, m_fTeamNumber);

    }

    [PunRPC]
    public void GroundCheckJudgment(float number, float team)
    {
         m_cubeManager.Paint(number, team);
    }


    private void OnDrawGizmos()
    {
        if (photonView.IsMine != true)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_fPunchRange);
    }


  

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine != true)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Dragon"))
        {
            Vector3 vec = transform.position - other.transform.position;
            GetHitByDragon(vec);
        }
    }

    public void GetHitByDragon(Vector3 vec)
    {
        if (photonView.IsMine != true|| m_state == State.Dizzy)
            return;
       
        Dizzy();
        m_player.Hurt();
        m_player.m_rigidbody.AddForce(vec * m_fDropkickPower, ForceMode.Impulse);
    }

  

    public void Die()
    {
        if (photonView.IsMine != true)
            return;

        m_particleManager.fireParticle.Play();
        m_soundManager.walkSound.Play();        
        m_state = State.Die;
        m_gFX.SetActive(false);
        m_masterGroundChecker.gameObject.SetActive(false);
        m_respawnCoroutine=StartCoroutine(RespawnCoroutine());
    }

  

    public IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(m_fRespawnTime);
        m_masterGroundChecker.gameObject.SetActive(true);
        m_gFX.SetActive(true);
        transform.position = m_respawnPosition.position;
        m_state = State.Idle;
        StopCoroutine(m_respawnCoroutine);
    }
}
