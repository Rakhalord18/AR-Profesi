using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Toggle silentToggle;

    void Start()
    {
        // Inisialisasi volume slider dan toggle silent berdasarkan nilai yang tersimpan
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        silentToggle.onValueChanged.AddListener(SetSilent);
    }


    //Fungsi untuk tombol start game
    public void StartGame(){
        SceneManager.LoadScene("Game"); 
    }

    //Fungsi untuk tombol settings
    public void OpenSettings(){
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    //Fungsi untuk tombol kembali ke menu utama dari settings
    public void BackToMainMenu(){
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    //Fungsi untuk tombol keluar game
    public void ExitGame(){
        Application.Quit();
    }

    //Fungsi untuk mengatur volume berdasarkan slider
    public void SetVolume(float volume){
        audioSource.volume = volume;
    }

    //Fungsi untuk mengaktifkan atau menonaktifkan mode silent
    public void SetSilent(bool isSilent){
        audioSource.mute = isSilent;
    }
}
