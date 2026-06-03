using UnityEngine;

/// <summary>
/// Checkpoint (kontrol noktası) yöneticisi - Singleton pattern.
/// Son geçilen checkpoint pozisyonunu kaydeder.
/// Sahnedeki boş bir GameObject'e (_CheckpointManager) ekle.
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    // ==============================
    // SINGLETON PATTERN
    // ==============================
    // Bu sayede oyun boyunca tek bir CheckpointManager olur
    // ve her yerden CheckpointManager.Instance ile erişilir.
    public static CheckpointManager Instance { get; private set; }

    [Header("Başlangıç Ayarları")]
    [Tooltip("Oyunun başladığı pozisyon (ilk spawn noktası)")]
    public Vector3 startPosition = Vector3.zero;

    [Header("UI Ayarları")]
    [Tooltip("Ekranda belirecek 'Checkpoint Alındı' yazısı")]
    public TMPro.TMP_Text checkpointTextUI;

    [Tooltip("Yazının ekranda kalma süresi (saniye)")]
    public float displayDuration = 5f;

    // Son kaydedilen checkpoint pozisyonu
    private Vector3 lastCheckpointPosition;

    // Geçilen checkpoint sayısı
    private int checkpointsPassed = 0;

    void Awake()
    {
        // Singleton kurulumu: Zaten bir instance varsa bu objeyi yok et
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Başlangıç pozisyonunu ilk checkpoint olarak ayarla
        lastCheckpointPosition = startPosition;

        // Oyun başında yazıyı gizle
        if (checkpointTextUI != null)
        {
            checkpointTextUI.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Yeni bir checkpoint'e ulaşıldığında çağrılır
    /// </summary>
    /// <param name="position">Checkpoint'in dünya pozisyonu</param>
    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        checkpointsPassed++;
        Debug.Log($"Checkpoint kaydedildi! ({checkpointsPassed}. checkpoint) - Pozisyon: {position}");

        // Ekranda yazıyı göster
        if (checkpointTextUI != null)
        {
            checkpointTextUI.text = "CHECKPOINT ALINDI!";
            checkpointTextUI.gameObject.SetActive(true);
            StopAllCoroutines(); // Eğer yazı zaten ekrandaysa süreyi sıfırla
            StartCoroutine(HideCheckpointTextRoutine());
        }
    }

    private System.Collections.IEnumerator HideCheckpointTextRoutine()
    {
        yield return new WaitForSeconds(displayDuration);
        if (checkpointTextUI != null)
        {
            checkpointTextUI.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Son kaydedilen checkpoint pozisyonunu döndürür
    /// </summary>
    public Vector3 GetCheckpoint()
    {
        return lastCheckpointPosition;
    }

    /// <summary>
    /// Geçilen checkpoint sayısını döndürür
    /// </summary>
    public int GetCheckpointsPassed()
    {
        return checkpointsPassed;
    }

    /// <summary>
    /// Tüm checkpoint verilerini sıfırlar (oyun yeniden başladığında)
    /// </summary>
    public void ResetCheckpoints()
    {
        lastCheckpointPosition = startPosition;
        checkpointsPassed = 0;
    }
}
