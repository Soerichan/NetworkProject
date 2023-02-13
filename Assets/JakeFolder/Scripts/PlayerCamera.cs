using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCamera : MonoBehaviourPun
{

    PlayerController[] m_player;
    CinemachineVirtualCamera m_virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_virtualCamera = GetComponent<CinemachineVirtualCamera>();
        

       
    }

    public void RoundStart()
    {
        m_player = FindObjectsOfType<PlayerController>();

        for (int i = 0; i < m_player.Length; i++)
        {
            if (m_player[i].photonView.IsMine)
            {
                m_virtualCamera.m_Follow = m_player[i].gameObject.transform;
            }
        }
    }
   
}
