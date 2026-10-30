using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Mengatur animasi masuk (entry transition) sekali saja di awal untuk UI Image atau Sprite.
/// Sangat membantu mempercantik kemunculan elemen UI secara dinamis dengan kurva easing yang menarik.
/// </summary>
public class EntryImageAnimator : MonoBehaviour
{
    public enum EntryType
    {
        FadeInOnly,
        ScaleUpOnly,
        SlideInOnly,
        FadeAndScale,
        FadeAndSlide,
        ScaleAndSlide,
        AllCombined
    }

    public enum SlideDirection
    {
        FromLeft,
        FromRight,
        FromTop,
        FromBottom,
        CustomOffset
    }

    public enum EaseType
    {
        Linear,
        SmoothStep,
        EaseOutBack,    // Memantul melebihi target sedikit (Juicy!)
        EaseOutBounce,  // Memantul jatuh (seperti bola basket jatuh)
        EaseOutElastic  // Efek karet/pegas lentur
    }

    [Header("Animation Settings")]
    [Tooltip("Tipe animasi masuk yang diinginkan.")]
    [SerializeField] private EntryType entryType = EntryType.FadeAndScale;

    [Tooltip("Kurva pergerakan animasi (Easing).")]
    [SerializeField] private EaseType easeType = EaseType.EaseOutBack;

    [Tooltip("Durasi animasi masuk (dalam detik).")]
    [SerializeField] private float duration = 0.8f;

    [Tooltip("Jeda waktu sebelum animasi masuk dimulai (dalam detik).")]
    [SerializeField] private float startDelay = 0.2f;

    [Tooltip("Gunakan unscaled time agar tetap bergerak walau game sedang di-pause.")]
    [SerializeField] private bool useUnscaledTime = false;

    [Tooltip("Mulai animasi otomatis saat objek diaktifkan.")]
    [SerializeField] private bool playOnEnable = true;

    [Header("Slide Transition Settings")]
    [Tooltip("Dari arah mana gambar akan meluncur masuk (hanya berlaku jika tipe animasi menggunakan 'Slide').")]
    [SerializeField] private SlideDirection slideFrom = SlideDirection.FromBottom;

    [Tooltip("Jarak meluncur jika menggunakan Custom Offset.")]
    [SerializeField] private Vector3 customSlideOffset = new Vector3(0f, -500f, 0f);

    [Header("Events")]
    [Tooltip("Event yang terpicu secara otomatis ketika animasi masuk telah selesai sepenuhnya.")]
    public UnityEvent OnAnimationComplete;

    // Status Awal
    private Vector3 targetScale;
    private Vector3 targetPosition;
    private float targetAlpha = 1f;

    private Vector3 startPosition;
    private Vector3 startScale;
    private float startAlpha = 0f;

    // Cache komponen
    private RectTransform rectTransform;
    private Image uiImage;
    private SpriteRenderer spriteRenderer;
    private CanvasGroup canvasGroup;

    private Coroutine activeAnimCoroutine;

    private void Awake()
    {
        // Cache komponen
        rectTransform = GetComponent<RectTransform>();
        uiImage = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Catat posisi akhir (target asli)
        targetScale = transform.localScale;
        
        if (rectTransform != null)
        {
            targetPosition = rectTransform.anchoredPosition;
        }
        else
        {
            targetPosition = transform.localPosition;
        }

        // Catat alpha akhir
        if (canvasGroup != null)
        {
            targetAlpha = canvasGroup.alpha;
        }
        else if (uiImage != null)
        {
            targetAlpha = uiImage.color.a;
        }
        else if (spriteRenderer != null)
        {
            targetAlpha = spriteRenderer.color.a;
        }
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayEntryAnimation();
        }
    }

    /// <summary>
    /// Mulai jalankan animasi masuk secara manual dari script lain
    /// </summary>
    public void PlayEntryAnimation()
    {
        if (activeAnimCoroutine != null)
        {
            StopCoroutine(activeAnimCoroutine);
        }
        
        InitializeStates();
        activeAnimCoroutine = StartCoroutine(AnimateEntrySequence());
    }

    /// <summary>
    /// Siapkan titik mulai animasi berdasarkan jenis transisi yang dipilih
    /// </summary>
    private void InitializeStates()
    {
        // 1. Skala Mulai
        if (entryType == EntryType.ScaleUpOnly || 
            entryType == EntryType.FadeAndScale || 
            entryType == EntryType.ScaleAndSlide || 
            entryType == EntryType.AllCombined)
        {
            startScale = Vector3.zero;
        }
        else
        {
            startScale = targetScale;
        }

        // 2. Alpha Mulai
        if (entryType == EntryType.FadeInOnly || 
            entryType == EntryType.FadeAndScale || 
            entryType == EntryType.FadeAndSlide || 
            entryType == EntryType.AllCombined)
        {
            startAlpha = 0f;
        }
        else
        {
            startAlpha = targetAlpha;
        }

        // 3. Posisi Mulai
        if (entryType == EntryType.SlideInOnly || 
            entryType == EntryType.FadeAndSlide || 
            entryType == EntryType.ScaleAndSlide || 
            entryType == EntryType.AllCombined)
        {
            Vector3 offset = GetSlideOffset();
            startPosition = targetPosition + offset;
        }
        else
        {
            startPosition = targetPosition;
        }

        // Terapkan kondisi awal secara instan sebelum animasi dimulai
        ApplyState(startPosition, startScale, startAlpha);
    }

    private Vector3 GetSlideOffset()
    {
        float canvasWidth = Screen.width;
        float canvasHeight = Screen.height;

        // Cari tahu ukuran canvas terdekat jika ada
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null && rectTransform != null)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            if (canvasRect != null)
            {
                canvasWidth = canvasRect.rect.width;
                canvasHeight = canvasRect.rect.height;
            }
        }

        switch (slideFrom)
        {
            case SlideDirection.FromLeft:
                return new Vector3(-canvasWidth, 0f, 0f);
            case SlideDirection.FromRight:
                return new Vector3(canvasWidth, 0f, 0f);
            case SlideDirection.FromTop:
                return new Vector3(0f, canvasHeight, 0f);
            case SlideDirection.FromBottom:
                return new Vector3(0f, -canvasHeight, 0f);
            case SlideDirection.CustomOffset:
                return customSlideOffset;
            default:
                return Vector3.zero;
        }
    }

    private void ApplyState(Vector3 pos, Vector3 scale, float alpha)
    {
        transform.localScale = scale;

        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = pos;
        }
        else
        {
            transform.localPosition = pos;
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
        }
        else if (uiImage != null)
        {
            Color c = uiImage.color;
            c.a = alpha;
            uiImage.color = c;
        }
        else if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = alpha;
            spriteRenderer.color = c;
        }
    }

    private IEnumerator AnimateEntrySequence()
    {
        // Tunggu jeda awal
        if (startDelay > 0f)
        {
            if (useUnscaledTime)
                yield return new WaitForSecondsRealtime(startDelay);
            else
                yield return new WaitForSeconds(startDelay);
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            
            // Hitung nilai normalisasi (0 sampai 1)
            float t = Mathf.Clamp01(elapsed / duration);

            // Terapkan Easing Curve
            float easedT = ApplyEasing(t);

            // Interpolasi posisi, skala, dan alpha
            Vector3 currentPos = Vector3.LerpUnclamped(startPosition, targetPosition, easedT);
            Vector3 currentScale = Vector3.LerpUnclamped(startScale, targetScale, easedT);
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t); // Alpha selalu linear agar tidak berkedip aneh

            ApplyState(currentPos, currentScale, currentAlpha);

            yield return null;
        }

        // Terapkan status akhir secara absolut untuk mencegah pergeseran kecil
        ApplyState(targetPosition, targetScale, targetAlpha);

        // Picu Event selesainya animasi
        OnAnimationComplete?.Invoke();
        activeAnimCoroutine = null;
    }

    private float ApplyEasing(float t)
    {
        switch (easeType)
        {
            case EaseType.Linear:
                return t;
            case EaseType.SmoothStep:
                return t * t * (3f - 2f * t);
            case EaseType.EaseOutBack:
                const float c1 = 1.70158f;
                const float c3 = c1 + 1f;
                return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
            case EaseType.EaseOutBounce:
                return EaseOutBounce(t);
            case EaseType.EaseOutElastic:
                return EaseOutElastic(t);
            default:
                return t;
        }
    }

    // Fungsi Matematika Easing
    private float EaseOutBounce(float x)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;
        if (x < 1f / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2f / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5f / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }

    private float EaseOutElastic(float x)
    {
        const float c4 = (2f * Mathf.PI) / 3f;
        return x == 0f ? 0f : x == 1f ? 1f 
               : Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * c4) + 1f;
    }
}
