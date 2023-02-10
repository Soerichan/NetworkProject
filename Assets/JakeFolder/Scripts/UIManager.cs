using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UIManager : MonoBehaviourPun
{
    public WinUI m_winUI;
    public LoseUI m_loseUI;
    public DrawUI m_drawUI;

    public ScoreUI m_scoreUI;

    public Joystick m_joystick;
    public PunchButton m_punchButton;
    public DropKickButton m_dropKickButton;

    public GameObject m_loadingUI;
    public GameObject m_startTextUI;
    //public GameObject m_hurryTextUI;
    public TextMeshProUGUI m_countdown;



    public void RoundStart()
    {
        m_joystick.RoundStart();
        m_punchButton.RoundStart();
        m_dropKickButton.RoundStart();
        StartCoroutine(UICoroutine());
    }

    public void RoundEnd()
    {
        StopAllCoroutines();
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

    public IEnumerator UICoroutine()
    {
        
        yield return new WaitForSeconds(3f);
        m_loadingUI.gameObject.SetActive(false);
        m_startTextUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_startTextUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(285f);
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        int count = 10;
        m_countdown.gameObject.SetActive(true);
        m_countdown.text = count.ToString();

        while (true)
        {
            yield return new WaitForSeconds(1f);
            count--;
            m_countdown.text = count.ToString();
        }
    }
}
