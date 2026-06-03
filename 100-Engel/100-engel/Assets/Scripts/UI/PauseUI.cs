using UnityEngine;

/// <summary>
/// Oyun içi duraklatma (Pause) menüsünü yönetir.
/// MainScene'deki Canvas objesine veya PausePanel objesine eklenebilir.
/// </summary>
public class PauseUI : MonoBehaviour
{
    [Header("UI Referansları")]
    [Tooltip("Açılıp kapanacak olan menü paneli")]
    public GameObject pausePanel;

    void Start()
    {
        // Oyun başladığında duraklatma menüsü kapalı olsun
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    void Update()
    {
        // Oyun bittiyse ESC ile pause açma
        if (GameManager.Instance != null && GameManager.Instance.currentState == GameManager.GameState.Finished)
            return;

        // ESC tuşuna basıldığında oyunu duraklat veya devam ettir
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance != null)
        {
            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                OnPauseButtonClicked();
            }
            else if (GameManager.Instance.currentState == GameManager.GameState.Paused)
            {
                OnResumeButtonClicked();
            }
        }
    }

    /// <summary>
    /// Ekranda sürekli duran "Menü/Duraklat" butonuna tıklandığında çağrılır
    /// </summary>
    public void OnPauseButtonClicked()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            GameManager.Instance.PauseGame();
            
            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Duraklatma menüsündeki "Oyuna Dön / Çarpı" butonuna tıklandığında çağrılır
    /// </summary>
    public void OnResumeButtonClicked()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState == GameManager.GameState.Paused)
        {
            GameManager.Instance.ResumeGame();
            
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
        }
    }

    /// <summary>
    /// "Tekrar Oyna" butonuna tıklandığında çağrılır
    /// </summary>
    public void OnRetryButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }

    /// <summary>
    /// "Ana Menüye Dön" butonuna tıklandığında çağrılır
    /// </summary>
    public void OnMainMenuButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GoToMainMenu();
        }
    }
}
