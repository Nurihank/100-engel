using UnityEngine;

/// <summary>
/// İki nokta arasında gidip gelen hareketli platform.
/// Bu scripti hareketli platform objesine ekle.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    [Tooltip("Platformun gideceği hedef nokta (dünya koordinatı)")]
    public Vector3 targetPosition;

    [Tooltip("Hareket hızı")]
    public float speed = 2f;

    [Tooltip("Hedef noktada bekleme süresi (saniye)")]
    public float waitTime = 0.5f;

    private Vector3 startPosition;
    private bool movingToTarget = true;
    private float waitTimer = 0f;

    void Start()
    {
        // Başlangıç pozisyonunu kaydet
        startPosition = transform.position;
    }

    void Update()
    {
        // Bekleme süresi varsa bekle
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        // Hedef noktaya doğru hareket et
        Vector3 destination = movingToTarget ? targetPosition : startPosition;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        // Hedefe ulaştıysa yön değiştir
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            movingToTarget = !movingToTarget;
            waitTimer = waitTime;
        }
    }

    /// <summary>
    /// Oyuncu platformun üstüne bindiğinde, karakteri platformun child'ı yap
    /// Böylece platform hareket ettiğinde oyuncu da birlikte hareket eder
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// Oyuncu platformdan indiğinde parent'ı kaldır
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    /// <summary>
    /// Editor'de hedef noktayı görselleştir (sarı çizgi)
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(startPosition, targetPosition);
            Gizmos.DrawWireSphere(targetPosition, 0.3f);
            Gizmos.DrawWireSphere(startPosition, 0.3f);
        }
        else
        {
            Gizmos.DrawLine(transform.position, targetPosition);
            Gizmos.DrawWireSphere(targetPosition, 0.3f);
        }
    }
}
