using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{


	//��������
	public void ReturntoRoom()
	{
		PhotonNetwork.CurrentRoom.IsOpen = true;
		PhotonNetwork.CurrentRoom.IsVisible = true;


		PhotonNetwork.LoadLevel("JswTestingLobby");
	}
}
/*
 ���� �÷��̾��Ʈ Ȧ ¦ �̱����� �������� ������ ���� ���������� �̵� �ִϸ��̼� ���� Ʈ��-�� �ִ� 
 
 */