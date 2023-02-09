namespace Jake
{

    using Photon.Pun;
    using Photon.Pun.UtilityScripts;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MasterGroundChecker : MonoBehaviourPun
    {

     
        
       
        public PlayerController m_playerController;

        private void Start()
        {
            m_playerController = GetComponentInParent<PlayerController>();
           
        }
        private void OnTriggerEnter(Collider other)
        {


            // 나중에 이벤트로 땅에 닿았으면 색칠 해주는걸로 변경 예정 
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                m_playerController.GroundCheckCall(other);
               
            }
        }

        
    }





}
