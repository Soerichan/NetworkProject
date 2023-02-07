using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngineInternal;
using static UnityEngine.ParticleSystem;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{

    public NavMeshAgent agent;
    public float speed;
    public GameObject player;
    public ParticleSystem particle;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Move();
    }


    public void Move()
    {
        if (GameObject.Find("Player"))
        { 
            agent.destination = player.transform.position;
            //transform.Translate(player.transform.position * Time.deltaTime * speed, Space.Self);
        }
        else return;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            particle.Play();
        }
        else
        {
            particle.Clear();
        }
    }
}
