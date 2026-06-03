using UnityEngine;

/// <summary>
/// Objeyi kendi ekseni etrafında döndürür.
/// Bu scripti dönen engel objelerine ekle.
/// </summary>
public class RotatingObstacle : MonoBehaviour
{
    [Header("Dönme Ayarları")]
    [Tooltip("X ekseni etrafında dönme hızı (derece/saniye)")]
    public float rotateSpeedX = 0f;

    [Tooltip("Y ekseni etrafında dönme hızı (derece/saniye)")]
    public float rotateSpeedY = 100f;

    [Tooltip("Z ekseni etrafında dönme hızı (derece/saniye)")]
    public float rotateSpeedZ = 0f;

    void Update()
    {
        // Her frame belirlenen hızda döndür
        transform.Rotate(
            rotateSpeedX * Time.deltaTime,
            rotateSpeedY * Time.deltaTime,
            rotateSpeedZ * Time.deltaTime
        );
    }
}
