using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [SerializeField]
    private Sprite newSprite;
    private GameObject candle;

    private void Start()
    {
        StartCoroutine(decay());
    }
    private void OnMouseDown()
    {
        candle = GameObject.FindGameObjectWithTag("candle");
        Picked();
    }
    public void Picked()
    {
        candle.GetComponent<CandleBehaviour>().currentHeal += 5;
        Destroy(this.gameObject);
    }
    IEnumerator decay()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
