using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            CinemachineVirtualCamera playerCamera = GetComponent<CinemachineVirtualCamera>();
            playerCamera.Follow = transform;
          
        }
    }

   
}
