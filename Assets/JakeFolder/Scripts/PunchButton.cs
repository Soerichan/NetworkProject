using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchButton : MonoBehaviour
{
    
   [SerializeField]
   private PlayerController m_playerController;

    
    public void ClickPunchButton()
    {
        m_playerController.Punch();
    }



}
