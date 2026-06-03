using UnityEngine;

/// <summary>
/// Her checkpoint objesine eklenir.
/// Oyuncu bu trigger'a girdiğinde CheckpointManager'a bildirir.
/// </summary>
public class CheckpointTrigger : MonoBehaviour
{
    [Header("Görsel Geri Bildirim")]
    [Tooltip("Checkpoint aktif olduğunda kullanılacak materyal (isteğe bağlı)")]
    public Material activatedMaterial;

    // Bu checkpoint daha önce aktif edildi mi?
    private bool isActivated = false;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Oyuncu checkpoint'e ulaştığında tetiklenir
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Sadece "Player" tag'lı obje tetikler
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;

            // CheckpointManager'a bu checkpoint'in pozisyonunu bildir
            // Oyuncuyu biraz yukarıda spawn etmek için Y'ye +1 ekliyoruz
            Vector3 spawnPos = transform.position + Vector3.up * 1f;
            CheckpointManager.Instance.SetCheckpoint(spawnPos);

            // Görsel geri bildirim: Materyali değiştir (aktif edildiğini göster)
            if (activatedMaterial != null && objectRenderer != null)
            {
                objectRenderer.material = activatedMaterial;
            }

            Debug.Log($"Checkpoint aktif: {gameObject.name}");
        }
    }
}
