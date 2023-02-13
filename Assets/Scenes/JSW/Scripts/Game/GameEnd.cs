using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{


	//리턴투룸
	public void ReturntoRoom()
	{
		PhotonNetwork.CurrentRoom.IsOpen = true;
		PhotonNetwork.CurrentRoom.IsVisible = true;


		PhotonNetwork.LoadLevel("JswTestingLobby");
	}
}
/*
 포톤 플레이어리스트 홀 짝 이긴팀을 윈포지션 진팀을 루즈 포지션으로 이동 애니메이션 불윈 트루-윈 애니 
 
 */