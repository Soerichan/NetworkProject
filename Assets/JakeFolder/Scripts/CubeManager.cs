using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviourPun
{
    public GroundColorChange[] m_arrayCube;
    public Renderer[]          m_arrayRenderer;
    public bool[]              m_bPainted;
    public float               m_fBlueCude;
    public float               m_fYellowCude;
    public UIManager           m_uIManager;

    void Start()
    {
       
        m_arrayCube     = GetComponentsInChildren<GroundColorChange>();
        m_arrayRenderer = new Renderer[m_arrayCube.Length];
        m_bPainted      = new bool[m_arrayCube.Length];

        for (int i=0;i< m_arrayCube.Length;i++)
        {
            m_arrayCube[i].m_fNumber = i;
            m_arrayRenderer[i]       = m_arrayCube[i].gameObject.GetComponent<Renderer>();
        }
    }

    public void Paint(float number,float team)
    {
        
        int iNumber = (int)number;

        if (m_bPainted[iNumber] ==false) //¾ÈÄ¥ÇØÁø Å¥ºê
        {
            if (team==0) //³ë¶ûÆÀ
            {               
                photonView.RPC("ApplyPaintCube", RpcTarget.All,number,team,1);              
            }
            else //ÆÄ¶ûÆÀ
            {                
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team,2);
            }          
        }
        else //Ä¥ÇØÁø Å¥ºê
        {
            if(team==0&&m_arrayCube[iNumber].m_fColor==1) //³ë¶ûÆÀÀÌ ÆÄ¶û Å¥ºê¸¦ ¹âÀ½
            {               
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team,3);
            }
            else if(team==1&& m_arrayCube[iNumber].m_fColor==0) //ÆÄ¶ûÆÀÀÌ ³ë¶û Å¥ºê¸¦ ¹âÀ½
            {                              
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team,4);
            }
        }
    }


    [PunRPC]
    public void ApplyPaintCube(float number,float team,int caseNum)
    {
        Debug.Log("ApplyPaintCube");
        int iNumber = (int)number;
        
        switch(caseNum)
        {
            case 1:
                m_arrayCube[iNumber].m_fColor = 0;
                m_arrayRenderer[iNumber].material.color = Color.yellow;
                m_fYellowCude++;
                m_uIManager.m_scoreUI.m_fYellowScore++;
                m_bPainted[iNumber] = true;
                break;

            case 2:
                m_arrayCube[iNumber].m_fColor = 1;
                m_arrayRenderer[iNumber].material.color = Color.blue;
                m_fBlueCude++;
                m_uIManager.m_scoreUI.m_fBlueScore++;
                m_bPainted[iNumber] = true;
                break;
            case 3:
                m_arrayCube[iNumber].m_fColor = 0;
                m_arrayRenderer[iNumber].material.color = Color.yellow;
                m_fYellowCude++;
                m_fBlueCude--;
                m_uIManager.m_scoreUI.m_fYellowScore++;
                m_uIManager.m_scoreUI.m_fBlueScore--;
                break;
            case 4:
                m_arrayCube[iNumber].m_fColor = 1;
                m_arrayRenderer[iNumber].material.color = Color.blue;
                m_fBlueCude++;
                m_fYellowCude--;
                m_uIManager.m_scoreUI.m_fBlueScore++;
                m_uIManager.m_scoreUI.m_fYellowScore--;
                break;
        }


    }

}
