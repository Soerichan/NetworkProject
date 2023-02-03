using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RealPlayerController : MonoBehaviour
{
    public Rigidbody rigid;
    public float speed;
    public NavMeshAgent agent;
    public ParticleSystem particle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigid= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            particle.Play();
            Debug.Log("¾Æ±ººÎµúÈû");
        }
        else
        { 
            particle.Stop();
            Debug.Log("¾Æ±º¾ÈºÎµúÈû");
        }
    }

}
