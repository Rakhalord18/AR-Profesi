using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceController : MonoBehaviour
{
    public void TNI(){
        GameData.Jenis_Pekerjaan = "TNI";
    }
    public void Polisi(){
        GameData.Jenis_Pekerjaan = "Polisi";
    }
    public void Dokter(){
        GameData.Jenis_Pekerjaan = "Dokter";
    }
    public void Guru(){
        GameData.Jenis_Pekerjaan = "Guru";
    }
    public void Pemadam(){
        GameData.Jenis_Pekerjaan = "Pemadam";
    }
    public void Pilot(){
        GameData.Jenis_Pekerjaan = "Pilot";
    }


    public void Pria(){
        GameData.JenisKelamin = "Pria";
        SceneManager.LoadScene("Character"); 
    }
    
    public void Wanita(){
        GameData.JenisKelamin = "Wanita";
        SceneManager.LoadScene("Character"); 
    }
}