using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [Header("���� �����")]
    public AudioSource attackSound;
    public AudioSource takeHitSound;
    public AudioSource paintSound;
    [Header("���� ��ü")]
    public AudioSource dragon;
    public AudioSource uptrain;
    public AudioSource downtrain;
    [Header("����")]
    public AudioClip dragonSound;
    public AudioClip trainSound;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            dragon.clip = dragonSound;
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            uptrain.clip = trainSound;
            downtrain.clip = trainSound;
        }
        else
        {

        }
    }

}
