using UnityEngine;

/// <summary>
/// En iyi süre (rekor) yöneticisi - Singleton pattern.
/// PlayerPrefs kullanarak en iyi süreyi kaydeder ve okur.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    // PlayerPrefs key'i
    private const string BEST_TIME_KEY = "BestTime";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Yeni süreyi kaydet (eğer mevcut rekordan daha iyiyse)
    /// </summary>
    /// <param name="time">Tamamlanan süre (saniye)</param>
    /// <returns>Yeni rekor kırıldıysa true döner</returns>
    public bool SaveBestTime(float time)
    {
        float currentBest = PlayerPrefs.GetFloat(BEST_TIME_KEY, float.MaxValue);

        if (time < currentBest)
        {
            PlayerPrefs.SetFloat(BEST_TIME_KEY, time);
            PlayerPrefs.Save();
            Debug.Log($"YENİ REKOR! Süre: {TimerManager.FormatTime(time)}");
            return true;
        }

        Debug.Log($"Rekor kırılamadı. Süre: {TimerManager.FormatTime(time)}, Rekor: {TimerManager.FormatTime(currentBest)}");
        return false;
    }

    /// <summary>
    /// Kayıtlı en iyi süreyi döndürür
    /// </summary>
    public float GetBestTime()
    {
        return PlayerPrefs.GetFloat(BEST_TIME_KEY, 0f);
    }

    /// <summary>
    /// Daha önce kaydedilmiş bir rekor var mı?
    /// </summary>
    public bool HasBestTime()
    {
        return PlayerPrefs.HasKey(BEST_TIME_KEY);
    }

    /// <summary>
    /// En iyi süreyi formatlanmış string olarak döndürür
    /// </summary>
    public string GetFormattedBestTime()
    {
        if (!HasBestTime())
            return "Henüz rekor yok!";

        return TimerManager.FormatTime(GetBestTime());
    }

    /// <summary>
    /// Rekor verisini siler (debug amaçlı)
    /// </summary>
    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey(BEST_TIME_KEY);
        PlayerPrefs.Save();
        Debug.Log("Rekor silindi!");
    }
}
