using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviourPun
{
    

    public float m_fLimitTime;
    public TimerUI m_timer;
    
    public UIManager m_UIManager;
    
    public List<Photon.Realtime.Player> m_listPlayer;
    

    

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            RoundStart();
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            m_listPlayer[i] = PhotonNetwork.PlayerList[i];
        }
    }

    public void RoundStart()
    {
        if (PhotonNetwork.IsMasterClient)
        { 
        m_timer.m_slider.maxValue = m_fLimitTime;
        m_timer.m_slider.value = m_fLimitTime;


        StartCoroutine(RoundCoroutine());
        StartCoroutine(TimeCoroutine());
        }
    }

    public void RoundEnd()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StopAllCoroutines();
            WinJudgment();
        }
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

           
            yield return new WaitForSeconds(0.5f);
        }
        
        
    }

    public void WinJudgment()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (m_UIManager.m_scoreUI.m_fBlueScore > m_UIManager.m_scoreUI.m_fYellowScore)
            {
                BlueWin();
            }
            else if (m_UIManager.m_scoreUI.m_fBlueScore > m_UIManager.m_scoreUI.m_fYellowScore)
            {
                YellowWin();
            }
            else
            {
                Draw();
            }
        }
    }

    
    public void BlueWin()
    {
        for (int i = 0; i < m_listPlayer.Count; i++)
        {
            if(m_listPlayer[i].GetPlayerNumber()%2==0)
            {

            }
            else
            {

            }
        }
    }
    public void YellowWin()
    {
        for (int i = 0; i < m_listPlayer.Count; i++)
        {
            if (m_listPlayer[i].GetPlayerNumber() % 2 == 0)
            {

            }
            else
            {

            }
        }
    }
    public void Draw()
    {
        photonView.RPC("Draw", RpcTarget.All);
    }

    [PunRPC]
    public void BlueWinUICall()
    {
        bool win = PhotonNetwork.LocalPlayer.GetPlayerNumber() % 2 == 0 ? false : true;
        if(win)
        {
            m_UIManager.Win();
        }
        else
        {
            m_UIManager.Lose();
        }
    }

    [PunRPC]
    public void YellowWinUICall()
    {
        bool win = PhotonNetwork.LocalPlayer.GetPlayerNumber() % 2 == 0 ? true: false;
        if (win)
        {
            m_UIManager.Win();
        }
        else
        {
            m_UIManager.Lose();
        }
    }


    [PunRPC]
    public void DrawUICall()
    {
        m_UIManager.Draw();
    }


    /* 
        public void ±Û¾¾»ö±ò ¹Ù²ÜÇÔ¼ö()
    {
        ³­ ÇÒ ¼ö ÀÖ´Ù! 
        Àü±¤ÆÇ¿¡ ±Û¾¾ »ö±ò = ÀÌ±äÆÀ¿¡ »ö±ò·Î ´ëÀÔ;
    }

    public void ÆÇÁ¤ÇÔ¼ö()
    {
        if (ÀÌ±äÆÀ)
            ±Û¾¾»ö±ò ¹Ù²Ü ÇÔ¼ö()
            WinPosition.win()
        else (Áø ÆÀ)
            WinPosition.Lose();
    }

     */
}
