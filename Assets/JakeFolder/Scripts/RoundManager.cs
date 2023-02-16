using Cinemachine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviourPun
{
   

    public float m_fLimitTime;
    public TimerUI m_timer;
    
    public UIManager m_UIManager;
    public WinManager m_WinManager;

    public PlayerCamera m_playerCamera;


    private void Awake()
    {
        
    }

    private void Start()
    {
       
        
     
    }

 

    public void RoundStart()
    {
        if (PhotonNetwork.IsMasterClient)
        { 
       
     
        photonView.RPC("RemoteRoundStart", RpcTarget.All);
        StartCoroutine(RoundCoroutine());
        
        }
    }

    [PunRPC]
    public void RemoteRoundStart()
    {

        m_timer.m_slider.maxValue = m_fLimitTime;
        m_timer.m_slider.value = m_fLimitTime;
        m_UIManager.RoundStart();
        m_playerCamera.RoundStart();
        StartCoroutine(TimeCoroutine());
        


    }


    public void RoundEnd()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StopAllCoroutines();
            WinJudgment();
            StartCoroutine(GoToResultScene());
        }
        
    }

    public IEnumerator RoundCoroutine()
    {
        yield return new WaitForSeconds(m_fLimitTime);
        RoundEnd();
    }
    public IEnumerator GoToResultScene()
    {
        yield return new WaitForSeconds(3.0f);
        PhotonNetwork.LoadLevel(3);

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
                Debug.Log(string.Format("BlueScore:{0},YellowScore:{1}", m_UIManager.m_scoreUI.m_fBlueScore, m_UIManager.m_scoreUI.m_fYellowScore));
                BlueWin();
            }
            else if (m_UIManager.m_scoreUI.m_fBlueScore < m_UIManager.m_scoreUI.m_fYellowScore)
            {
                Debug.Log(string.Format("BlueScore:{0},YellowScore:{1}", m_UIManager.m_scoreUI.m_fBlueScore, m_UIManager.m_scoreUI.m_fYellowScore));
                YellowWin();
            }
            else
            {
                Debug.Log(string.Format("BlueScore:{0},YellowScore:{1}", m_UIManager.m_scoreUI.m_fBlueScore, m_UIManager.m_scoreUI.m_fYellowScore));
                Draw();
            }
        }
    }

    
    public void BlueWin()
    {
        
        m_WinManager.teamWin = false;
        photonView.RPC("BlueWinUICall", RpcTarget.All);
    }
    public void YellowWin()
    {
        m_WinManager.teamWin = true;
        photonView.RPC("YellowWinUICall", RpcTarget.All);
    }
    public void Draw()
    {
        photonView.RPC("DrawUICall", RpcTarget.All);
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
}
