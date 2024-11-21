using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public GameObject[] newObject;

    public void SpawnNewObject()
    {
        StartCoroutine(SpawnAfterTime());
    }

    IEnumerator SpawnAfterTime()
    {
        yield return new WaitForSeconds(20);
        GameObject nb = Instantiate(newObject[Random.Range(0,newObject.Length)], this.transform) as GameObject;
        nb.transform.localPosition = new UnityEngine.Vector3(Random.Range(-2f,2f),0.08f,0);
    }
}