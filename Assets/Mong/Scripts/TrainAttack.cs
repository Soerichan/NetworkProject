using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAttack : MonoBehaviour
{
    public GameObject[] TrainbPosition = new GameObject[2];
    public GameObject train;
    public float dragonTime;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TrainAttackStart());
        train.transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
    }

    public IEnumerator TrainAttackStart()
    {
        yield return new WaitForSeconds(dragonTime);
        int q = Random.Range(0, 2);
        train.transform.position = TrainbPosition[q].transform.position;
        train.transform.rotation = TrainbPosition[q].transform.rotation;
        StopAllCoroutines();
    }
}
