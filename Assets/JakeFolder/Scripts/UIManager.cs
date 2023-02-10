using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIManager : MonoBehaviourPun
{
    public WinUI m_winUI;
    public LoseUI m_loseUI;
    public DrawUI m_drawUI;

    public ScoreUI m_scoreUI;

    public Joystick m_joystick;
    public PunchButton m_punchButton;
    public DropKickButton m_dropKickButton;





    public void RoundStart()
    {
        m_joystick.RoundStart();
        m_punchButton.RoundStart();
        m_dropKickButton.RoundStart();
    }

    public void Win()
    {
        m_winUI.gameObject.SetActive(true);
    }
   
    public void Lose()
    {
        m_loseUI.gameObject.SetActive(true);
    }
   
    public void Draw()
    {
        m_drawUI.gameObject.SetActive(true);    
    }
}
