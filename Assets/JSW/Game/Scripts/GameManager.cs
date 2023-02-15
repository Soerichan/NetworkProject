
namespace jsw
{
    using UnityEngine;
    using Photon.Pun;
    using Photon.Pun.UtilityScripts;
    using Photon.Realtime;
    using Hashtable = ExitGames.Client.Photon.Hashtable;
    using UnityEngine.SceneManagement;
    using System.Collections;
    using TMPro;

    using UnityEngine.UIElements;
    using Cinemachine;
    using System.Collections.Generic;

    public class GameManager : MonoBehaviourPunCallbacks
	{
		[SerializeField]
		private TMP_Text infoText;

		[SerializeField]
		private TMP_Text timer;
		private float sec;
		private int min;
		private int timerStop=0;
		[SerializeField]
		private List<Image> images = new List<Image>();

		[SerializeField]
		private GameObject[] TeamStartPosition;

		[SerializeField]
		private RoundManager m_roundManager;

		private void Start()
		{
			if (PhotonNetwork.InRoom)
			{
				Hashtable props = new Hashtable() { { GameData.PLAYER_LOAD, true } };
				PhotonNetwork.LocalPlayer.SetCustomProperties(props);
			}
			else // 게임씬 테스트용 빠른 접속
			{
				PhotonNetwork.ConnectUsingSettings();
				infoText.text = "";
			}
		}
		private void Update()
		{
			MainTimer();


        }
		public void MainTimer()
		{
            sec += timerStop*Time.deltaTime;
            if (sec > 60) { min++; sec = 0; }
            timer.text = "" + min + ":" + (int)sec;
        }
		#region PUN Callback

		public override void OnConnectedToMaster()
		{
			PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions() { MaxPlayers = 8 }, null);
		}

		public override void OnJoinedRoom()
		{
			StartCoroutine(TestGameDelay());
		}



		public override void OnDisconnected(DisconnectCause cause)
		{
			Debug.Log(string.Format("Disconnected : {0}", cause.ToString()));
			SceneManager.LoadScene("LobbyScene");
		}

		public override void OnLeftRoom()
		{
			Debug.Log("OnLeftRoom");
			SceneManager.LoadScene("LobbyScene");
		}

		public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
		{
			if (changedProps.ContainsKey(GameData.PLAYER_LOAD))
			{
				if (CheckAllPlayerLoadLevel())
				{
					StartCoroutine(StartCountDown());
				}
				else
				{
					PrintInfo("wait players " + PlayersLoadLevel() + " / " + PhotonNetwork.PlayerList.Length);
				}
			}
		}

		public override void OnMasterClientSwitched(Player newMasterClient)
		{
			if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
			{
				//마스터가 튕겼을때 새로 반응 추가
			}
		}

		#endregion

		private void GameStart()
		{
            timerStop = 1;
            
			if (PhotonNetwork.LocalPlayer.GetPlayerNumber() % 2 == 0)
			{
                float angularStart = (360.0f / PhotonNetwork.CurrentRoom.PlayerCount) * (PhotonNetwork.LocalPlayer.GetPlayerNumber()*0.5f);
                float x = 5.0f * Mathf.Sin(angularStart * Mathf.Deg2Rad);
                float z = 5.0f * Mathf.Cos(angularStart * Mathf.Deg2Rad);
                Vector3 position = new Vector3(x, 0.0f, z);
                Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);
                PhotonNetwork.Instantiate("PlayerPrefab",TeamStartPosition[0].transform.position+ position, rotation, 0);
			}
			else
			{
                float angularStart = (360.0f / PhotonNetwork.CurrentRoom.PlayerCount) * ((PhotonNetwork.LocalPlayer.GetPlayerNumber()-1)*0.5f);
                float x = 5.0f * Mathf.Sin(angularStart * Mathf.Deg2Rad);
                float z = 5.0f * Mathf.Cos(angularStart * Mathf.Deg2Rad);
                Vector3 position = new Vector3(x, 0.0f, z);
                Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);
                PhotonNetwork.Instantiate("PlayerPrefab", TeamStartPosition[1].transform.position+position, rotation, 0);
			}


			m_roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
			StartCoroutine(ReadyCoroutine());



			
        }

		
		private IEnumerator ReadyCoroutine()
		{
			yield return new WaitForSeconds(3f);
            m_roundManager.RoundStart();
        }

		private void TestGameStart()
		{
			timerStop = 1;
            if (PhotonNetwork.LocalPlayer.GetPlayerNumber() % 2 == 0)
            {
                float angularStart = (360.0f / PhotonNetwork.CurrentRoom.PlayerCount) * (PhotonNetwork.LocalPlayer.GetPlayerNumber() * 0.5f);
                float x = 5.0f * Mathf.Sin(angularStart * Mathf.Deg2Rad);
                float z = 5.0f * Mathf.Cos(angularStart * Mathf.Deg2Rad);
                Vector3 position = new Vector3(x, 0.0f, z);
                Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);
                PhotonNetwork.Instantiate("PlayerY", TeamStartPosition[0].transform.position + position, rotation, 0);
            }
            else
            {
                float angularStart = (360.0f / PhotonNetwork.CurrentRoom.PlayerCount) * ((PhotonNetwork.LocalPlayer.GetPlayerNumber() - 1) * 0.5f);
                float x = 5.0f * Mathf.Sin(angularStart * Mathf.Deg2Rad);
                float z = 5.0f * Mathf.Cos(angularStart * Mathf.Deg2Rad);
                Vector3 position = new Vector3(x, 0.0f, z);
                Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);
                PhotonNetwork.Instantiate("PlayerB", TeamStartPosition[1].transform.position + position, rotation, 0);
            }
			
        }

		private IEnumerator StartCountDown()
		{
			PrintInfo("All Player Loaded, Start Count Down");
			yield return new WaitForSeconds(1.0f);

			for (int i = GameData.COUNTDOWN; i > 0; i--)
			{
				PrintInfo("Count Down " + i);
				yield return new WaitForSeconds(1.0f);
			}

			PrintInfo("Start Game!");
			GameStart();

			yield return new WaitForSeconds(1f);
			infoText.text = "";
		}

		private IEnumerator TestGameDelay()
		{
			yield return new WaitForSeconds(1.0f);
			TestGameStart();
		}

		private bool CheckAllPlayerLoadLevel()
		{
			return PlayersLoadLevel() == PhotonNetwork.PlayerList.Length;
		}

		private int PlayersLoadLevel()
		{
			int count = 0;
			foreach (Player p in PhotonNetwork.PlayerList)
			{
				object playerLoadedLevel;
				if (p.CustomProperties.TryGetValue(GameData.PLAYER_LOAD, out playerLoadedLevel))
				{
					if ((bool)playerLoadedLevel)
					{
						count++;
					}
				}
			}

			return count;
		}

		private void PrintInfo(string info)
		{
			Debug.Log(info);
			infoText.text = info;
		}

		
	}
}