using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGetter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;

    string Key;

    //public Dictionary<string, GameObject> GetDic = new Dictionary<string, GameObject>();

    public PoolManager poolManager;

    public SoundManager soundManager;
    // Update is called once per frame
    //private void PoolAdd()
    //{
    //    Key = "공격사운드";
    //    GetDic.Add(Key, poolManager.Get(prefabs[0]));
    //    Key = "피격사운드";
    //    GetDic.Add(Key, poolManager.Get(prefabs[1]));
    //    Key = "걸음사운드";
    //    GetDic.Add(Key, poolManager.Get(prefabs[2]));
    //    Key = "공격파티클";
    //    GetDic.Add(Key, poolManager.Get(prefabs[3]));
    //    Key = "피격파티클";
    //    GetDic.Add(Key, poolManager.Get(prefabs[4]));
    //}
    public void PoolGet(string get)
    {
        Key = get;
        poolManager.NameGet(Key);
    }
    private void Start()
    {
        //PoolAdd();
        soundManager = FindObjectOfType<SoundManager>();
    }

}
