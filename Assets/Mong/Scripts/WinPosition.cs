using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace Mong
{
    public class WinPosition : MonoBehaviour
    {
        public GameObject[] winposition = new GameObject[4];
        public GameObject[] Loseposition = new GameObject[4];
        public GameObject[] Characters = new GameObject[8];

        public WinManager win;
        
        public bool bluewin;

        private void Start()
        {
            win=FindObjectOfType<WinManager>();
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
            for (int i=0;i<PhotonNetwork.CurrentRoom.PlayerCount;i++) 
            {
                Characters[i].SetActive(true);
            }
            bluewin = !win.teamWin;

            if (bluewin)
            {
                BlueWin();
                
            }
            else
            {
                YellowWin();
            }
            StartCoroutine(win.DestroyThis());
            
        }

        public void BlueWin()
        {
            for (int i = 0; i < 4; i++)
            {
                Characters[2 * i].transform.position = Loseposition[i].transform.position;
                Characters[2 * i + 1].transform.position = winposition[i].transform.position;
            }
        }

        public void YellowWin()
        {
            for (int i = 0; i < 4; i++)
            {
                
                Characters[2 * i].transform.position = winposition[i].transform.position;
                Characters[2 * i + 1].transform.position = Loseposition[i].transform.position;
            }
        }

       






    }
}
