using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public GameObject flare;

    private void Start()
    {
        StartCoroutine(SpawnAfterTime());
    }
   
    IEnumerator SpawnAfterTime()
    {
        while (true)
        {       
            GameObject nb = Instantiate(flare);
            nb.transform.position = new Vector3(Random.Range(-6,6), Random.Range(1.5f,0.5f));
            yield return new WaitForSeconds(10);
        }
    }
}