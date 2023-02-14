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
        public GameObject[] winCharacter = new GameObject[4];
        public GameObject[] loseCharacter = new GameObject[4];

    
        public bool bluewin;

        private void Start()
        {

            if (bluewin)
            {
                Win();
                Lose();
            }
            else
            {
                Shuffle();
            }
        }

        public void Win()
        {
            for (int i = 0; i < 4; i++)
            {
                winCharacter[i].transform.position = winposition[i].transform.position;
            }
        }

        public void Lose()
        {
            for (int i = 0; i < 4; i++)
            {
                loseCharacter[i].transform.position = Loseposition[i].transform.position;
            }
        }

        public void Shuffle()
        {
            for (int i = 0; i < 4; i++)
            {
                winCharacter[i].transform.position = Loseposition[i].transform.position;
                loseCharacter[i].transform.position = winposition[i].transform.position;
            }
        }






    }
}
