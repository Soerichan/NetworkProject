using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchButton : MonoBehaviour
{
    
  
   private PlayerController[] m_playerController;
    private PlayerController m_player;

    private void Start()
    {
        
    }

    public void RoundStart()
    {
        m_playerController = FindObjectsOfType<PlayerController>();


        for (int i = 0; i < m_playerController.Length; i++)
        {
            if (m_playerController[i].photonView.IsMine)
            {
                m_player = m_playerController[i];
            }
        }
    }

   
    public void ClickPunchButton()
    {
        m_player.Punch();
    }



}
