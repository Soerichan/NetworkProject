using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    public enum PlayerType { Player, Enermy };
    public PlayerType playerType;
    //트리거를 원형으로 교체 해볼예정
    private void OnTriggerEnter(Collider other)
    {
        // 나중에 이벤트로 땅에 닿았으면 색칠 해주는걸로 변경 예정 
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (playerType == PlayerType.Player)
                other.gameObject.GetComponent<GroundColorChange>().renderer.material.color = Color.red;
            else
                other.gameObject.GetComponent<GroundColorChange>().renderer.material.color = Color.blue;
        }
    }

}
