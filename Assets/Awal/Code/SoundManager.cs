using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image soundOnIcon;
    [SerializeField] private Image soundOffIcon;

    private bool muted;

    private void Start()
    {
        // Ambil status mute yang tersimpan
        muted = PlayerPrefs.GetInt("muted", 0) == 1;

        // Terapkan status audio
        AudioListener.pause = muted;

        // Update icon
        UpdateButtonIcon();
    }

    public void OnButtonPress()
    {
        // Toggle mute
        muted = !muted;

        // Terapkan ke semua audio
        AudioListener.pause = muted;

        // Simpan status
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
        PlayerPrefs.Save();

        // Update icon
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (muted)
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
        else
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
    }
}