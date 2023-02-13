using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAttack : MonoBehaviour
{
    public GameObject[] TrainbPosition = new GameObject[2];
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

        train.m_rigidbody.velocity = transform.forward * m_fVelocity;
    }




    public IEnumerator TrainAttackStart()
    {

        yield return new WaitForSeconds(10f);


        while (true)
        {
            yield return new WaitForSeconds(dragonTime);
            int q = Random.Range(0, 2);
            train.transform.position = TrainbPosition[q].transform.position;
            train.transform.rotation = TrainbPosition[q].transform.rotation;
            train.m_rigidbody.velocity = train.transform.forward * m_fVelocity;
        }
    }

}
