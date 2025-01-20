using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Transform dari objek burung yang akan diikuti oleh kamera
    public Transform bird;

    // Kecepatan transisi kamera saat mengikuti burung
    public float smoothSpeed = 0.125f;

    // Offset posisi kamera relatif terhadap burung
    public Vector3 offset;

    // Dipanggil setelah semua Update selesai pada frame yang sama
    private void LateUpdate()
    {
        // Jika objek burung tidak ada (null), tidak ada yang dilakukan
        if (bird == null) return;

        // Hitung posisi yang diinginkan kamera berdasarkan posisi burung dan offset
        Vector3 desiredPosition = bird.position + offset;

        // Haluskan transisi posisi kamera menggunakan interpolasi linear 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Tetapkan posisi kamera ke posisi yang sudah dihaluskan
        transform.position = smoothedPosition;
    }
}
