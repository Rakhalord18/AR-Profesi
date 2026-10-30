using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script serbaguna untuk memberikan animasi sederhana pada UI Image atau Sprite 2D
/// tanpa memerlukan Animator Controller. Mempermudah kustomisasi langsung dari Inspector.
/// </summary>
public class SimpleImageAnimator : MonoBehaviour
{
    public enum AnimationType
    {
        None,
        Pulse,          // Membesar & mengecil secara halus
        Float,          // Melayang naik-turun (hover effect)
        Rotate,         // Berputar terus-menerus
        FadeBreathe,    // Pudar/bernafas (transparansi berubah-ubah)
        Shake           // Menggigil/getar halus (sangat bagus untuk tombol penting)
    }

    [Header("Animation Configuration")]
    [Tooltip("Tipe animasi yang ingin digunakan.")]
    [SerializeField] private AnimationType animType = AnimationType.Float;

    [Tooltip("Kecepatan gerakan animasi.")]
    [SerializeField] private float speed = 2.0f;

    [Tooltip("Kekuatan atau jarak animasi (skala, jarak melayang, sudut putar, dll).")]
    [SerializeField] private float magnitude = 0.1f;

    [Tooltip("Jeda waktu sebelum animasi dimulai (detik).")]
    [SerializeField] private float startDelay = 0f;

    [Tooltip("Gunakan unscaled time agar animasi tetap berjalan saat game sedang di-pause (Time.timeScale = 0).")]
    [SerializeField] private bool useUnscaledTime = false;

    [Tooltip("Mulai animasi secara otomatis saat objek aktif.")]
    [SerializeField] private bool playOnEnable = true;

    [Header("Custom Axis (Untuk Float / Rotate)")]
    [Tooltip("Sumbu pergerakan melayang (untuk Float) atau sumbu putar (untuk Rotate).")]
    [SerializeField] private Vector3 animationAxis = new Vector3(0f, 1f, 0f); // Default Y-axis untuk Float

    // Penyimpanan status awal objek agar tidak bergeser secara permanen
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private float originalAlpha = 1f;

    // Cache komponen
    private RectTransform rectTransform;
    private Image uiImage;
    private SpriteRenderer spriteRenderer;
    private CanvasGroup canvasGroup;

    private float timer = 0f;
    private bool isAnimating = false;
    private float delayTimer = 0f;

    private void Awake()
    {
        // Mendapatkan cache komponen yang diperlukan
        rectTransform = GetComponent<RectTransform>();
        uiImage = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Menyimpan nilai awal (default)
        originalScale = transform.localScale;
        originalRotation = transform.localEulerAngles;

        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
        else
        {
            originalPosition = transform.localPosition;
        }

        // Menyimpan alpha awal
        if (canvasGroup != null)
        {
            originalAlpha = canvasGroup.alpha;
        }
        else if (uiImage != null)
        {
            originalAlpha = uiImage.color.r;
        }
        else if (spriteRenderer != null)
        {
            originalAlpha = spriteRenderer.color.a;
        }
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            ResetToOriginals();
            timer = 0f;
            delayTimer = startDelay;
            isAnimating = true;
        }
    }

    private void OnDisable()
    {
        ResetToOriginals();
        isAnimating = false;
    }

    private void Update()
    {
        if (!isAnimating) return;

        // Menghitung jeda awal
        if (delayTimer > 0f)
        {
            delayTimer -= GetDeltaTime();
            return;
        }

        // Jalankan timer animasi
        timer += GetDeltaTime() * speed;

        // Memproses animasi berdasarkan tipe yang dipilih
        switch (animType)
        {
            case AnimationType.Pulse:
                ApplyPulse();
                break;
            case AnimationType.Float:
                ApplyFloat();
                break;
            case AnimationType.Rotate:
                ApplyRotate();
                break;
            case AnimationType.FadeBreathe:
                ApplyFadeBreathe();
                break;
            case AnimationType.Shake:
                ApplyShake();
                break;
        }
    }

    private float GetDeltaTime()
    {
        return useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
    }

    private void ApplyPulse()
    {
        // Skala membesar & mengecil menggunakan Gelombang Sinus
        float pulseFactor = Mathf.Sin(timer);
        float newScale = 1f + (pulseFactor * magnitude);
        transform.localScale = originalScale * newScale;
    }

    private void ApplyFloat()
    {
        // Posisi melayang maju mundur di sumbu tertentu
        float floatFactor = Mathf.Sin(timer);
        Vector3 offset = animationAxis.normalized * (floatFactor * magnitude);

        // Jika komponen UI Canvas
        if (rectTransform != null)
        {
            // Untuk UI, magnitude dalam satuan piksel terasa lebih nyaman
            rectTransform.anchoredPosition = originalPosition + offset * 100f;
        }
        else
        {
            // Untuk Objek 2D Game World
            transform.localPosition = originalPosition + offset;
        }
    }

    private void ApplyRotate()
    {
        // Berputar terus menerus pada sumbu putar yang ditentukan
        float dt = GetDeltaTime();
        transform.Rotate(animationAxis.normalized * (speed * 50f * dt * magnitude), Space.Self);
    }

    private void ApplyFadeBreathe()
    {
        // Transparansi bernafas (memudar lalu kembali solid)
        float fadeFactor = (Mathf.Sin(timer) + 1f) * 0.5f; // Normalisasi ke rentang 0 - 1
        float targetAlpha = Mathf.Lerp(originalAlpha * (1f - magnitude), originalAlpha, fadeFactor);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = targetAlpha;
        }
        else if (uiImage != null)
        {
            Color c = uiImage.color;
            c.a = targetAlpha;
            uiImage.color = c;
        }
        else if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = targetAlpha;
            spriteRenderer.color = c;
        }
    }

    private void ApplyShake()
    {
        // Efek getar acak
        float shakeX = Mathf.Sin(timer * 1.5f) * magnitude;
        float shakeY = Mathf.Cos(timer * 1.2f) * magnitude;
        Vector3 shakeOffset = new Vector3(shakeX, shakeY, 0f);

        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = originalPosition + shakeOffset * 100f;
        }
        else
        {
            transform.localPosition = originalPosition + shakeOffset;
        }
    }

    /// <summary>
    /// Mengembalikan objek ke kondisi default saat animasi dihentikan atau di-disable
    /// </summary>
    public void ResetToOriginals()
    {
        transform.localScale = originalScale;
        transform.localEulerAngles = originalRotation;

        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }
        else
        {
            transform.localPosition = originalPosition;
        }

        // Kembalikan alpha
        if (canvasGroup != null)
        {
            canvasGroup.alpha = originalAlpha;
        }
        else if (uiImage != null)
        {
            Color c = uiImage.color;
            c.a = originalAlpha;
            uiImage.color = c;
        }
        else if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = originalAlpha;
            spriteRenderer.color = c;
        }
    }

    /// <summary>
    /// Mengubah jenis animasi secara runtime
    /// </summary>
    public void ChangeAnimation(AnimationType newType, float newSpeed = 2f, float newMagnitude = 0.1f)
    {
        ResetToOriginals();
        animType = newType;
        speed = newSpeed;
        magnitude = newMagnitude;
        timer = 0f;
    }
}
