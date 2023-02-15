namespace jsw
{
    using Photon.Pun;
    using Photon.Pun.UtilityScripts;
    using Photon.Realtime;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class RoomPanel : MonoBehaviour
    {
        [SerializeField]
        private RectTransform playerContent;
        [SerializeField]
        private RectTransform playerRedContent;
        [SerializeField]
        private RectTransform playerBlueContent;

        [SerializeField]
        private PlayerEntry playerEntryPrefab;
        [SerializeField]
        private Button redTeambutton;
        [SerializeField]
        private Button blueTeambutton;
        [SerializeField]
        private Button startButton;

        private List<PlayerEntry> playerEntryList;

        private void Awake()
        {
            playerEntryList = new List<PlayerEntry>();
        }

        public void UpdateRoomState()
        {
            foreach (PlayerEntry entry in playerEntryList)
            {
                Destroy(entry.gameObject);
            }
            playerEntryList.Clear();

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                PlayerEntry entry;

                if (player.GetPlayerNumber() % 2 == 0)
                {
                    entry = Instantiate(playerEntryPrefab, playerRedContent);
                }
                else
                {
                    entry = Instantiate(playerEntryPrefab, playerBlueContent);
                }


                entry.Initailize(player.ActorNumber, player.NickName);
                object isPlayerReady;
                if (player.CustomProperties.TryGetValue(GameData.PLAYER_READY, out isPlayerReady))
                {
                    entry.SetPlayerReady((bool)isPlayerReady);
                }
                playerEntryList.Add(entry);
            }
        }

        public void LocalPlayerPropertiesUpdated()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            startButton.gameObject.SetActive(CheckPlayerReady());
        }

        public bool CheckPlayerReady()
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                object isPlayerReady;
                if (player.CustomProperties.TryGetValue(GameData.PLAYER_READY, out isPlayerReady))
                {
                    if (!(bool)isPlayerReady)
                        return false;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        public void OnYellowTeamButtonClicked()
        {//겹쳤을때나 최대수를 넘겼을경우 예외처리 필요
            PhotonNetwork.CurrentRoom.Players[PhotonNetwork.LocalPlayer.GetPlayerNumber()].SetPlayerNumber(PhotonNetwork.LocalPlayer.GetPlayerNumber()+1);
            UpdateRoomState();
        }
        public void OnBlueTeamButtonClicked()
        {
            PhotonNetwork.CurrentRoom.Players[PhotonNetwork.LocalPlayer.GetPlayerNumber()].SetPlayerNumber(PhotonNetwork.LocalPlayer.GetPlayerNumber() + 1);
            UpdateRoomState();
        }
        public void OnStartButtonClicked()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            int rand = Random.Range(1, 3);
            PhotonNetwork.LoadLevel(2);
        }
        public void OnShuffleButtonClicked()
        {
            int maxPlayer = PhotonNetwork.CurrentRoom.PlayerCount;
            int container;
            int[] ints = new int[maxPlayer];
            for (int i = 0; i < maxPlayer; i++)
            { ints[i] = i; }
            for (int i = 0; i < maxPlayer; i++)
            {
                container = ints[i];
                int randomIndex = Random.Range(i, maxPlayer);
                ints[i] = ints[randomIndex];
                ints[randomIndex] = container;
            }
            for (int i = 0; i < maxPlayer; i++)
            { PhotonNetwork.CurrentRoom.GetPlayer(i).SetPlayerNumber(ints[i]); }
            UpdateRoomState();

        }
        public void OnLeaveRoomClicked()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}