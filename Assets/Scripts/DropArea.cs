using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    private CandleBehaviour cb; // Referensi ke skrip CandleBehaviour untuk mengakses properti dan metode lilin
    private void Start()
    {
        // Mencari objek dengan tag "candle" dan mendapatkan komponen CandleBehaviour-nya
        cb = GameObject.FindGameObjectWithTag("candle").GetComponent<CandleBehaviour>();
    }
    private void OnMouseEnter()
    {
        // Ketika kursor mouse memasuki area tertentu, status dragging lilin diubah menjadi false
        cb.isDragging = false;
    }
}
