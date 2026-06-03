using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Ana menü arayüz yöneticisi.
/// MainMenu sahnesindeki Canvas objesine ekle.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [Header("UI Referansları")]
    [Tooltip("En iyi süre text'i")]
    public TMP_Text bestTimeText;

    void Start()
    {
        // Fare imlecini göster (menüde olduğumuz için)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // En iyi süreyi göster
        UpdateBestTimeDisplay();
    }

    /// <summary>
    /// "Oyuna Başla" butonuna tıklandığında çağrılır
    /// Inspector'da butonun OnClick() eventine bu fonksiyonu ata
    /// </summary>
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// "Çıkış" butonuna tıklandığında çağrılır (isteğe bağlı)
    /// </summary>
    public void OnQuitButtonClicked()
    {
        Debug.Log("Oyun kapatılıyor...");
        Application.Quit();

        // Editor'de test ederken çalışması için:
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    /// <summary>
    /// En iyi süre gösterimini günceller
    /// </summary>
    private void UpdateBestTimeDisplay()
    {
        if (bestTimeText == null) return;

        if (PlayerPrefs.HasKey("BestTime"))
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime");
            bestTimeText.text = "En İyi Süre: " + TimerManager.FormatTime(bestTime);
        }
        else
        {
            bestTimeText.text = "Henüz rekor yok!";
        }
    }
}
