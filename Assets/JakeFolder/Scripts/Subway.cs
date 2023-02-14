using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Subway : MonoBehaviourPun, IPunObservable
{
    public Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }
}
