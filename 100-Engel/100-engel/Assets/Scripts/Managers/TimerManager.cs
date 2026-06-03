using UnityEngine;
using System;

/// <summary>
/// Zaman sayacı yöneticisi - Singleton pattern.
/// Oyun süresini takip eder ve formatlanmış süre döndürür.
/// Sahnedeki _GameManager objesine veya ayrı bir objeye ekle.
/// </summary>
public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    // Geçen süre (saniye cinsinden)
    private float elapsedTime = 0f;

    // Sayaç çalışıyor mu?
    private bool isRunning = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        // Sayaç çalışıyorsa süreyi artır
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Sayacı başlatır
    /// </summary>
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
        Debug.Log("Timer başladı!");
    }

    /// <summary>
    /// Sayacı durdurur
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
        Debug.Log($"Timer durdu! Süre: {GetFormattedTime()}");
    }

    /// <summary>
    /// Sayacı duraklatır (süreyi sıfırlamadan)
    /// </summary>
    public void PauseTimer()
    {
        isRunning = false;
    }

    /// <summary>
    /// Duraklatılmış sayacı devam ettirir
    /// </summary>
    public void ResumeTimer()
    {
        isRunning = true;
    }

    /// <summary>
    /// Geçen süreyi saniye olarak döndürür
    /// </summary>
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    /// <summary>
    /// Geçen süreyi "00:00.00" formatında döndürür
    /// Örnek: 1 dakika 23.45 saniye → "01:23.45"
    /// </summary>
    public string GetFormattedTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(elapsedTime);
        return string.Format("{0:D2}:{1:D2}.{2:D2}",
            time.Minutes,
            time.Seconds,
            time.Milliseconds / 10);
    }

    /// <summary>
    /// Verilen süreyi formatlanmış stringe çevirir (statik yardımcı)
    /// </summary>
    public static string FormatTime(float timeInSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}.{2:D2}",
            time.Minutes,
            time.Seconds,
            time.Milliseconds / 10);
    }
}
