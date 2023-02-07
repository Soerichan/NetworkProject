using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviourPun
{
    

    public float m_fLimitTime;
    public TimerUI m_timer;
    public ScoreUI m_scoreUI;
    public UIManager m_UImanager;
    public WinUI m_winUI;
    public LoseUI m_loseUI;
    public DrawUI m_drawUI;

    

    private void Awake()
    {
        if (photonView.IsMine != true)
        {

            Destroy(this);
        }
    }

    private void Start()
    {
        RoundStart();
    }

    public void RoundStart()
    {
       
        m_timer.m_slider.maxValue = m_fLimitTime;
        m_timer.m_slider.value = m_fLimitTime;
        
       
        StartCoroutine(RoundCoroutine());
        StartCoroutine(TimeCoroutine());
    }

    public void RoundEnd()
    {
        StopAllCoroutines();
        WinJudgment();
    }

    public IEnumerator RoundCoroutine()
    {
        yield return new WaitForSeconds(m_fLimitTime);
    }
    public IEnumerator TimeCoroutine()
    {
        while (true)
        {
            m_timer.m_slider.value-=0.5f;

            m_scoreUI.m_fBlueScore++;
            m_scoreUI.m_fYellowScore++;
            yield return new WaitForSeconds(0.5f);
        }
        
        
    }

    public void WinJudgment()
    {
        if(m_scoreUI.m_fBlueScore>m_scoreUI.m_fYellowScore)
        {
            BlueWin();
        }
        else if (m_scoreUI.m_fBlueScore > m_scoreUI.m_fYellowScore)
        {
            YellowWin();
        }
        else
        {
            Draw();
        }
    }

    public void BlueWin()
    {

    }
    public void YellowWin()
    {

    }
    public void Draw()
    {
        
    }
}
