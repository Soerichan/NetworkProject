using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKickButton : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_playerController;


    public void ClickDropkickButton()
    {
        m_playerController.Dropkick();
    }

}
