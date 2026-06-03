using UnityEngine;

/// <summary>
/// Karakter düştüğünde son checkpoint'e geri taşır.
/// Bu scripti karakter objesine (PlayerArmature) ekle.
/// </summary>
public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Ayarları")]
    [Tooltip("Bu Y değerinin altına düşerse karakter ölür")]
    public float deathY = -20f;

    [Tooltip("Respawn sırasında ekran kararma efekti süresi")]
    public float fadeTime = 0.5f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Her frame karakter pozisyonunu kontrol et
        // Belirli bir Y değerinin altına düştüyse respawn yap
        if (transform.position.y < deathY)
        {
            Respawn();
        }
    }

    /// <summary>
    /// DeathZone trigger'ına girdiğinde de respawn tetiklenir
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Respawn();
        }
    }

    /// <summary>
    /// Karakteri son checkpoint konumuna geri taşır
    /// </summary>
    public void Respawn()
    {
        // CheckpointManager'dan son kayıt noktasını al
        Vector3 respawnPosition = CheckpointManager.Instance.GetCheckpoint();

        // CharacterController aktifken transform.position değiştirilemez
        // Bu yüzden geçici olarak devre dışı bırak
        characterController.enabled = false;
        transform.position = respawnPosition;
        characterController.enabled = true;

        Debug.Log("Oyuncu respawn oldu: " + respawnPosition);
    }
}
