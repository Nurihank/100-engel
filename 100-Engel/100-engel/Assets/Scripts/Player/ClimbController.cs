using UnityEngine;

/// <summary>
/// Tırmanma mekaniği - "Climbable" tag'lı objelere temas edildiğinde
/// karakterin duvar boyunca yukarı tırmanmasını sağlar.
/// Bu scripti karakter objesine (PlayerArmature) ekle.
/// </summary>
public class ClimbController : MonoBehaviour
{
    [Header("Tırmanma Ayarları")]
    [Tooltip("Tırmanma hızı")]
    public float climbSpeed = 3f;

    [Tooltip("Tırmanırken yatay hareket hızı (sağ-sol)")]
    public float climbHorizontalSpeed = 2f;

    private CharacterController characterController;
    private bool isClimbing = false;
    private float originalGravity;

    // ThirdPersonController referansı (Starter Assets)
    private MonoBehaviour thirdPersonController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Starter Assets'teki ThirdPersonController'ı bul
        thirdPersonController = GetComponent("ThirdPersonController") as MonoBehaviour;
    }

    void Update()
    {
        if (isClimbing)
        {
            HandleClimbing();
        }
    }

    /// <summary>
    /// Tırmanma sırasında karakter hareketini yönetir
    /// </summary>
    private void HandleClimbing()
    {
        // W tuşu: Yukarı tırman, S tuşu: Aşağı in
        float vertical = Input.GetAxis("Vertical");
        // A-D tuşları: Sağ-sol hareket
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 climbMovement = new Vector3(
            horizontal * climbHorizontalSpeed,
            vertical * climbSpeed,
            0f
        ) * Time.deltaTime;

        characterController.Move(climbMovement);

        // Space tuşu ile tırmanmayı bırak (zıplayarak)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopClimbing();
        }
    }

    /// <summary>
    /// Climbable tag'lı objeye temas edildiğinde tırmanmayı başlat
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            StartClimbing();
        }
    }

    /// <summary>
    /// Climbable objeden ayrıldığında tırmanmayı bitir
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            StopClimbing();
        }
    }

    private void StartClimbing()
    {
        isClimbing = true;

        // ThirdPersonController'ı devre dışı bırak (çakışmasın)
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = false;
        }
    }

    private void StopClimbing()
    {
        isClimbing = false;

        // ThirdPersonController'ı tekrar aktif et
        if (thirdPersonController != null)
        {
            thirdPersonController.enabled = true;
        }
    }
}
