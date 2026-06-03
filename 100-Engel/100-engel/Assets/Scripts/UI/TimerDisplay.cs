using UnityEngine;
using TMPro;

/// <summary>
/// Ekranda süreyi gösterir (HUD - sağ üst köşe).
/// Canvas içindeki Timer Text objesine ekle.
/// </summary>
public class TimerDisplay : MonoBehaviour
{
    [Header("UI Referansı")]
    [Tooltip("Süreyi gösteren TextMeshPro elemanı")]
    public TMP_Text timerText;

    void Update()
    {
        // Timer çalışıyorsa süreyi güncelle
        if (TimerManager.Instance != null && timerText != null)
        {
            timerText.text = TimerManager.Instance.GetFormattedTime();
        }
    }
}
