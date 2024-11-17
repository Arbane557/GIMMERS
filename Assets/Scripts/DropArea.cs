using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    private CandleBehaviour cb;
    private void Start()
    {
        cb = GameObject.FindGameObjectWithTag("candle").GetComponent<CandleBehaviour>();
    }
    private void OnMouseEnter()
    {
        cb.isDragging = false;
    }
}
