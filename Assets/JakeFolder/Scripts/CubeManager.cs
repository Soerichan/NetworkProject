using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviourPun
{
    public GroundColorChange[] m_arrayCube;
    public Renderer[] m_arrayRenderer;
    public bool[] m_bPainted;
    public float m_fBlueCude;
    public float m_fYellowCude;
    public UIManager m_uIManager;
    // Start is called before the first frame update
    void Start()
    {
       
        m_arrayCube = GetComponentsInChildren<GroundColorChange>();

        m_arrayRenderer = new Renderer[m_arrayCube.Length];
        m_bPainted= new bool[m_arrayCube.Length];

        for (int i=0;i< m_arrayCube.Length;i++)
        {
            m_arrayCube[i].m_fNumber=i;
            m_arrayRenderer[i]= m_arrayCube[i].gameObject.GetComponent<Renderer>();
        }
    }

    public void Paint(float number,float team)
    {
        Debug.Log("페인트");
        int iNumber = (int)number;

        if (m_bPainted[iNumber] ==false) //안칠해진 큐브
        {

            if (team==0) //노랑팀
            {
               
                photonView.RPC("ApplyPaintCube", RpcTarget.All,number,team,1);
                Debug.Log("페인트 케이스1 호출");

            }
            else //파랑팀
            {
                
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team,2);
                Debug.Log("페인트 케이스2 호출");
            }

          
        }
        else //칠해진 큐브
        {
            if(team==0&&m_arrayCube[iNumber].m_fColor==1) //노랑팀이 파랑 큐브를 밟음
            {
                
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team,3);
                Debug.Log("페인트 케이스3 호출");
            }
            else if(team==1&& m_arrayCube[iNumber].m_fColor==0) //파랑팀이 노랑 큐브를 밟음
            {
               
                
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team,4);
                Debug.Log("페인트 케이스4 호출");
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
                Debug.Log("어플라이페인트 케이스1 ");
                break;

            case 2:
                m_arrayCube[iNumber].m_fColor = 1;
                m_arrayRenderer[iNumber].material.color = Color.blue;
                m_fBlueCude++;
                m_uIManager.m_scoreUI.m_fBlueScore++;
                m_bPainted[iNumber] = true;
                Debug.Log("어플라이페인트 케이스2 ");
                break;
            case 3:
                m_arrayCube[iNumber].m_fColor = 0;
                m_arrayRenderer[iNumber].material.color = Color.yellow;
                m_fYellowCude++;
                m_fBlueCude--;
                m_uIManager.m_scoreUI.m_fYellowScore++;
                m_uIManager.m_scoreUI.m_fBlueScore--;
                Debug.Log("어플라이페인트 케이스3 ");
                break;
            case 4:
                m_arrayCube[iNumber].m_fColor = 1;
                m_arrayRenderer[iNumber].material.color = Color.blue;
                m_fBlueCude++;
                m_fYellowCude--;
                m_uIManager.m_scoreUI.m_fBlueScore++;
                m_uIManager.m_scoreUI.m_fYellowScore--;
                Debug.Log("어플라이페인트 케이스4 ");
                break;

           

        }


    }

}
