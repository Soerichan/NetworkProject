using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public bool teamWin; //True==bluewin,false==yellowWin
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(10.0f);
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;

        PhotonNetwork.LoadLevel(0);
        
        Destroy(this);
    }
    
}
