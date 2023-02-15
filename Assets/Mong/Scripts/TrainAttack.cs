using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAttack : MonoBehaviour
{
    public GameObject TrainbPosition;
    public Subway train;
    public float dragonTime;
    public float m_fVelocity;

    // Update is called once per frame
  

  

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(this);
        }
        StartCoroutine(TrainAttackStart());

        train.m_rigidbody.velocity = transform.right * m_fVelocity;
    }




    public IEnumerator TrainAttackStart()
    {

        yield return new WaitForSeconds(10f);


        while (true)
        {
            yield return new WaitForSeconds(dragonTime);
          
            train.transform.position = TrainbPosition.transform.position;
        
            train.m_rigidbody.velocity = transform.right * m_fVelocity;
        }
    }

}
