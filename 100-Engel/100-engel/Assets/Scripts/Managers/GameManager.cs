using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Oyunun genel akışını yöneten ana yönetici - Singleton pattern.
/// Oyun durumlarını (menü, oynuyor, duraklatıldı, bitti) yönetir.
/// Sahnedeki boş bir GameObject'e (_GameManager) ekle.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ==============================
    // OYUN DURUMLARI
    // ==============================
    public enum GameState
    {
        Menu,       // Ana menüde
        Playing,    // Oyun devam ediyor
        Paused,     // Oyun duraklatıldı
        Finished    // Parkur tamamlandı
    }

    [Header("Mevcut Durum")]
    public GameState currentState = GameState.Menu;

    [Header("UI Referansları")]
    [Tooltip("FinishUI scriptinin olduğu obje")]
    public FinishUI finishUI;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // MainScene'de başlıyorsak otomatik olarak oyunu başlat
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            StartGame();
        }
    }

    /// <summary>
    /// Oyunu başlatır
    /// </summary>
    public void StartGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;

        // Timer'ı başlat
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.StartTimer();
        }

        // Fare imlecini kilitle ve gizle (3D oyun için)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Oyun başladı!");
    }

    /// <summary>
    /// Oyuncu parkuru tamamladığında çağrılır
    /// </summary>
    public void FinishGame()
    {
        if (currentState == GameState.Finished) return; // Çift tetiklenmeyi önle
        currentState = GameState.Finished;

        // Timer'ı durdur
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.StopTimer();
        }
        else
        {
            Debug.LogWarning("FinishGame: TimerManager bulunamadı!");
        }

        // Süreyi kaydet
        float finalTime = TimerManager.Instance != null ? TimerManager.Instance.GetElapsedTime() : 0f;
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SaveBestTime(finalTime);
        }
        else
        {
            Debug.LogWarning("FinishGame: ScoreManager bulunamadı!");
        }

        // Fare imlecini serbest bırak (butonlara tıklayabilsin)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Bitiş ekranını göster
        if (finishUI != null)
        {
            finishUI.ShowFinishScreen(finalTime);
        }
        else
        {
            Debug.LogError("FinishGame: finishUI referansı atanmamış! Inspector'da GameManager objesine FinishUI sürükleyin.");
        }

        // Oyunu duraklat - Time.timeScale=0 yapıyoruz ama UI butonları çalışır
        // Çünkü Button.OnClick() timeScale'den bağımsız çalışır
        Time.timeScale = 0f;

        Debug.Log("Oyun bitti! Süre: " + TimerManager.FormatTime(finalTime));
    }

    /// <summary>
    /// Oyunu duraklatır
    /// </summary>
    public void PauseGame()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.Paused;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.PauseTimer();
        }
    }

    /// <summary>
    /// Oyuna devam eder
    /// </summary>
    public void ResumeGame()
    {
        if (currentState != GameState.Paused) return;

        currentState = GameState.Playing;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.ResumeTimer();
        }
    }

    /// <summary>
    /// Oyunu yeniden başlatır (sahneyi yeniden yükler)
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Ana menüye döner
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
