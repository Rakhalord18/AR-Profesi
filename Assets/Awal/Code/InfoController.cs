using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
   public GameObject infoPanelTniPria;
    public GameObject infoPanelGuruPria;
    public GameObject infoPanelPemadamPria;
    public GameObject infoPanelPolisiPria;
    public GameObject infoPanelDokterPria;
    public GameObject infoPanelPilotPria;

    public GameObject infoPanelTniWanita;
    public GameObject infoPanelGuruWanita;
    public GameObject infoPanelPemadamWanita;
    public GameObject infoPanelPolisiWanita;
    public GameObject infoPanelDokterWanita;
    public GameObject infoPanelPilotWanita;


   public void ShowInfoPanel()
    {
        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "TNI"){
            infoPanelTniPria.SetActive(true);
        }
        
        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Guru"){
            infoPanelGuruPria.SetActive(true);
        }

        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Pemadam"){
            infoPanelPemadamPria.SetActive(true);
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Polisi"){
            infoPanelPolisiPria.SetActive(true);
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Dokter"){
            infoPanelDokterPria.SetActive(true);
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Pilot"){
            infoPanelPilotPria.SetActive(true);
        }
        


        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "TNI"){
            infoPanelTniWanita.SetActive(true);
        }

        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Guru"){
            infoPanelGuruWanita.SetActive(true);
        }

        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Pemadam"){
            infoPanelPemadamWanita.SetActive(true);
        }
    
         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Polisi"){
            infoPanelPolisiWanita.SetActive(true);
        }

         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Dokter"){
            infoPanelDokterWanita.SetActive(true);
        }

         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Pilot"){
            infoPanelPilotWanita.SetActive(true);
        }
    }
}