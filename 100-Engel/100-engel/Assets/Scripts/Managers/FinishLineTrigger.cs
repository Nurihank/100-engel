using UnityEngine;

/// <summary>
/// Bitiş çizgisi trigger'ı.
/// Oyuncu buraya ulaştığında GameManager.FinishGame() tetiklenir.
/// FinishLine objesine ekle ve Box Collider → Is Trigger işaretle.
/// </summary>
public class FinishLineTrigger : MonoBehaviour
{
    private bool hasFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        // Sadece bir kez tetiklensin
        if (other.CompareTag("Player") && !hasFinished)
        {
            hasFinished = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.FinishGame();
            }

            Debug.Log("PARKUR TAMAMLANDI!");
        }
    }
}
