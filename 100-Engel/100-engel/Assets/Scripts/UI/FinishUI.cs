using UnityEngine;
using TMPro;

/// <summary>
/// Parkur tamamlandığında gösterilen bitiş ekranı.
/// Canvas içindeki FinishPanel objesine ekle.
/// </summary>
public class FinishUI : MonoBehaviour
{
    [Header("UI Referansları")]
    [Tooltip("Bitiş paneli (varsayılan kapalı olmalı)")]
    public GameObject finishPanel;

    [Tooltip("Tamamlanan süreyi gösteren text")]
    public TMP_Text timeText;

    [Tooltip("En iyi süreyi gösteren text")]
    public TMP_Text bestTimeText;

    [Tooltip("Yeni rekor kırıldığında gösterilecek text/obje")]
    public GameObject newRecordObject;

    void Start()
    {
        // Paneli oyun başında gizle
        if (finishPanel != null)
        {
            finishPanel.SetActive(false);
        }

        if (newRecordObject != null)
        {
            newRecordObject.SetActive(false);
        }
    }

    /// <summary>
    /// Bitiş ekranını gösterir
    /// GameManager.FinishGame() tarafından çağrılır
    /// </summary>
    /// <param name="time">Tamamlanan süre (saniye)</param>
    public void ShowFinishScreen(float time)
    {
        Debug.Log("ShowFinishScreen çağrıldı.");

        if (finishPanel != null)
        {
            finishPanel.SetActive(true);
            Debug.Log("finishPanel SetActive(true) yapıldı.");
        }
        else
        {
            Debug.LogError("FinishUI: finishPanel referansı atanmamış! Inspector'da FinishUI üzerindeki Finish Panel alanını doldurun.");
        }

        // Süreyi göster
        if (timeText != null)
        {
            timeText.text = "Süren: " + TimerManager.FormatTime(time);
        }
        else
        {
            Debug.LogWarning("FinishUI: timeText atanmamış.");
        }

        // En iyi süreyi göster
        if (bestTimeText != null && ScoreManager.Instance != null)
        {
            bestTimeText.text = "En İyi: " + ScoreManager.Instance.GetFormattedBestTime();
        }

        // Yeni rekor kırıldıysa göster
        if (newRecordObject != null && ScoreManager.Instance != null)
        {
            float currentBest = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
            newRecordObject.SetActive(time <= currentBest);
        }
    }

    /// <summary>
    /// "Tekrar Oyna" butonuna tıklandığında
    /// Inspector'da butonun OnClick() eventine ata
    /// </summary>
    public void OnRetryClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }

    /// <summary>
    /// "Ana Menüye Dön" butonuna tıklandığında
    /// Inspector'da butonun OnClick() eventine ata
    /// </summary>
    public void OnMenuClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GoToMainMenu();
        }
    }
}
