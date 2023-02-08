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
        m_uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
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
        int iNumber = (int)number;

        if (m_bPainted[iNumber] ==false)
        {

            if (team==0)
            {
                m_arrayCube[iNumber].m_fColor = 0;
                m_arrayRenderer[iNumber].material.color = Color.yellow;
                m_fYellowCude++;
                m_uIManager.m_scoreUI.m_fYellowScore++;
                photonView.RPC("ApplyPaintCube", RpcTarget.All,number,team);


            }
            else
            {
                m_arrayCube[iNumber].m_fColor = 1;
                m_arrayRenderer[iNumber].material.color = Color.blue;
                m_fBlueCude++;
                m_uIManager.m_scoreUI.m_fBlueScore++;
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team);
            }

            m_bPainted[iNumber] = true;
        }
        else
        {
            if(team==0&&m_arrayCube[iNumber].m_fColor==1)
            {
                m_arrayCube[iNumber].m_fColor = 0;
                m_arrayRenderer[iNumber].material.color = Color.yellow;
                m_fYellowCude++;
                m_fBlueCude--;
                m_uIManager.m_scoreUI.m_fYellowScore++;
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team);
            }
            else if(team==1&& m_arrayCube[iNumber].m_fColor==0)
            {
               
                m_arrayCube[iNumber].m_fColor = 1;
                m_arrayRenderer[iNumber].material.color = Color.blue;
                m_fBlueCude++;
                m_fYellowCude--;
                m_uIManager.m_scoreUI.m_fBlueScore++;
                photonView.RPC("ApplyPaintCube", RpcTarget.All, number, team);
            }
        }
    }


    [PunRPC]
    public void ApplyPaintCube(float number,float team)
    {
        int iNumber = (int)number;

        if (team==0)
        {
            m_arrayRenderer[iNumber].material.color = Color.yellow;
        }
       else
        {
            m_arrayRenderer[iNumber].material.color = Color.blue;
        }
    }

}
