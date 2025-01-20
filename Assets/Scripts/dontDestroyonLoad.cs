using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script untuk memastikan objek tidak dihancurkan saat pergantian scene
public class dontDestroyonLoad : MonoBehaviour
{
    // Dipanggil sebelum objek aktif pertama kali
    void Awake()
    {
        // Mencegah penghancuran objek ini saat pergantian scene
        DontDestroyOnLoad(this.gameObject);
    }
}
