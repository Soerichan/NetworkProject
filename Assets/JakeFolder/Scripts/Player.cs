using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviourPun//, IPunObservable
{
    public Rigidbody m_rigidbody;

    [SerializeField]
    public float m_fMovePower;
    [SerializeField]
    public float m_fRotateSpeed;
    [SerializeField]
    public float m_fMaxSpeed;
    public float m_fMaxSpeedCopy;
    [SerializeField]
    public float m_fResistance;

    public Vector3 m_resistanceVector;

    [SerializeField]
    public float m_fMaxHP;
    [SerializeField]
    public float m_fNowHP;
    [SerializeField]
    public float m_fHPRegenTime;

    private Coroutine m_coroutineHPRegen;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_fMaxSpeedCopy = m_fMaxSpeed;
    }

    private void Start()
    {
        m_fNowHP = m_fMaxHP;

       
        m_coroutineHPRegen = StartCoroutine(HPRegenCoroutine());
       
    }

    public void Accelate(float power)
    {
        m_rigidbody.AddForce(power * transform.forward * m_fMovePower * Time.deltaTime, ForceMode.Force);

        if (m_rigidbody.velocity.magnitude > m_fMaxSpeed)
        {
            m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_fMaxSpeed;
        }

        if (m_rigidbody.velocity.magnitude > 0)
        {
            m_resistanceVector = -1 * m_rigidbody.velocity.normalized;
            m_rigidbody.AddForce(m_fResistance * m_resistanceVector * Time.deltaTime, ForceMode.Force);
        }
    }

    public void Move(Vector2 InputDir)
    {
        Vector2 moveInput = InputDir;

        bool isMove = moveInput.sqrMagnitude != 0;

        if (true == isMove)
        {

            Vector3 fowardVec = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z).normalized;
            Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z).normalized;
            Vector3 moveVec = fowardVec * moveInput.y + rightVec * moveInput.x;
            m_rigidbody.AddForce(moveVec * m_fMovePower * Time.deltaTime);
            transform.forward = moveVec.normalized;
           
        }
       
    }

    public void Rotate(float speed)
    {
        transform.Rotate(Vector3.up, speed * m_fRotateSpeed * Time.deltaTime);
    }

    public void Punch()
    {
       
    }

    public void Dropkick()
    {

    }

    public void Hurt()
    {
        if (m_fNowHP > 2)
        {
            m_fNowHP--;
            ApplyState();
            

            //나중에 게임엔드에서 맥스스피드를 맥스스피드카피로 복구
        }
    }

   
    private void HPRegen()
    {
       
            if (m_fNowHP < m_fMaxHP)
            {
                m_fNowHP++;
            }

        ApplyState();
        
    }


    public void ApplyState()
    {
        m_fMaxSpeed = m_fMaxSpeed * (m_fNowHP / m_fMaxHP);
    }

    private IEnumerator HPRegenCoroutine()
    {
        while (true)
        { 
        yield return new WaitForSeconds(m_fHPRegenTime);

        HPRegen();
        }
    }

   
}
