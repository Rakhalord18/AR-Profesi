using UnityEngine;
using Mediapipe.Unity;
using Mediapipe.Tasks.Vision.PoseLandmarker;
using Mediapipe.Tasks.Components.Containers;
// using System.Diagnostics;

public class AvatarController : MonoBehaviour
{
    public PoseLandmarkerResultAnnotationController controller;
    public Animator animator;
    public GameObject avatarObject;


    public Animator animatorTNIPRIA;
    public GameObject avatarObjectTNIPRIA;

    public Animator animatorTNIWANITA;
    public GameObject avatarObjectTNIWANITA;


    public Animator animatorGURUPRIA;
    public GameObject avatarObjectGURUPRIA;

    public Animator animatorGURUWANITA;
    public GameObject avatarObjectGURUWANITA;
    

    public Animator animatorPEMADAMPRIA;
    public GameObject avatarObjectPEMADAMPRIA;
    public Animator animatorPEMADAMWANITA;
    public GameObject avatarObjectPEMADAMWANITA;


    public Animator animatorPOLISIPRIA;
    public GameObject avatarObjectPOLISIPRIA;
    public Animator animatorPOLISIWANITA;
    public GameObject avatarObjectPOLISIWANITA;


    public Animator animatorDOKTERPRIA;
    public GameObject avatarObjectDOKTERPRIA;
    public Animator animatorDOKTERWANITA;
    public GameObject avatarObjectDOKTERWANITA;


    public Animator animatorPILOTPRIA;
    public GameObject avatarObjectPILOTPRIA;
    public Animator animatorPILOTWANITA;
    public GameObject avatarObjectPILOTWANITA;


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

    public AudioSource audioSource;
    public AudioClip AudioClip;


// Tambahkan variabel ini di bagian atas class untuk mengatur sensitivitas pergerakan
public float moveSpeed = 10f;
public Vector3 multiplier = new Vector3(10f, 10f, 10f); // Skala pergerakan
public Vector3 offset = new Vector3(-5f, -5f, 0f);    // Untuk memposisikan avatar di tengah screen
    Transform rUpperArm, rLowerArm;
    Transform lUpperArm, lLowerArm;

    Transform rUpperLeg, rLowerLeg;
    Transform lUpperLeg, lLowerLeg;

    Transform spine;

    private bool isInfoPanelOpen = false;

    void Start()
    {
        controller.gameObject.SetActive(false);

        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "TNI"){
            avatarObject = avatarObjectTNIPRIA;
            animator = animatorTNIPRIA;
        }
        
        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Guru"){
            avatarObject = avatarObjectGURUPRIA;
            animator = animatorGURUPRIA;
        }

        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Pemadam"){
            avatarObject = avatarObjectPEMADAMPRIA;
            animator = animatorPEMADAMPRIA;
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Polisi"){
            avatarObject = avatarObjectPOLISIPRIA;
            animator = animatorPOLISIPRIA;
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Dokter"){
            avatarObject = avatarObjectDOKTERPRIA;
            animator = animatorDOKTERPRIA;
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Pilot"){
            avatarObject = avatarObjectPILOTPRIA;
            animator = animatorPILOTPRIA;
        }
        


        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "TNI"){
            avatarObject = avatarObjectTNIWANITA;
            animator = animatorTNIWANITA;
        }

        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Guru"){
            avatarObject = avatarObjectGURUWANITA;
            animator = animatorGURUWANITA;
        }

        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Pemadam"){
            avatarObject = avatarObjectPEMADAMWANITA;
            animator = animatorPEMADAMWANITA;
        }
    
         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Polisi"){
            avatarObject = avatarObjectPOLISIWANITA;
            animator = animatorPOLISIWANITA;
        }

         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Dokter"){
            avatarObject = avatarObjectDOKTERWANITA;
            animator = animatorDOKTERWANITA;
        }

         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Pilot"){
            avatarObject = avatarObjectPILOTWANITA;
            animator = animatorPILOTWANITA;
        }


        // tangan
        rUpperArm = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
        rLowerArm = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);

        lUpperArm = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        lLowerArm = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);

        // kaki
        rUpperLeg = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        rLowerLeg = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);

        lUpperLeg = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        lLowerLeg = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);

        // badan
        spine = animator.GetBoneTransform(HumanBodyBones.Spine);
    }

bool isPlayingAudio = false;
void Update()
{
    if (isInfoPanelOpen)
        return;

    if (controller == null || animator == null)
        return;

    

    var result = controller.GetLatestResult();

    if (result.poseLandmarks == null || result.poseLandmarks.Count == 0)
        {
            avatarObject.SetActive(false);
            isPlayingAudio = false;
        return;
        }
        else
        {
            avatarObject.SetActive(true);
                if (!isPlayingAudio)
            {
                audioSource.Stop();
            audioSource.PlayOneShot(AudioClip);
            isPlayingAudio = true;
                
            }
        }

    var lm = result.poseLandmarks[0].landmarks;

    if (lm == null || lm.Count <= 16) return;

    // ======================
    // 🔥 AMBIL TITIK YANG DIINGINKAN
    // ======================

    // Kepala (nose)
    Vector3 headPos = new Vector3(
        lm[0].x,
        lm[0].y,
        lm[0].z
    );
    // Debug.Log("Head Position: " + headPos);

    // Ujung tangan kiri
    Vector3 leftHandPos = new Vector3(
        lm[15].x,
        lm[15].y,
        lm[15].z
    );

    // Ujung tangan kanan
    Vector3 rightHandPos = new Vector3(
        lm[16].x,
        lm[16].y,
        lm[16].z
    );

    // // Ujung kaki kiri
    // Vector3 leftFootPos = new Vector3(
    //     lm[31].x,
    //     lm[31].y,
    //     lm[31].z
    // );

    // // Ujung kaki kanan
    // Vector3 rightFootPos = new Vector3(
    //     lm[32].x,
    //     lm[32].y,
    //     lm[32].z
    // );

    // Titik tengah badan (antara bahu kiri & kanan)
    Vector3 centerPos = new Vector3(
        (lm[11].x + lm[12].x) / 2f,
        (lm[11].y + lm[12].y) / 2f,
        (lm[11].z + lm[12].z) / 2f
    );

    Vector3 targetPosition = new Vector3(
        centerPos.x * multiplier.x + offset.x,
        (1f - centerPos.y) * multiplier.y + offset.y, // Balik sumbu Y
        -centerPos.z * multiplier.z // Z seringkali butuh penyesuaian tergantung kebutuhan depth
    );

    avatarObject.transform.position = Vector3.Lerp(
        avatarObject.transform.position, 
        targetPosition, 
        Time.deltaTime * moveSpeed
    );
    
    try
    {
    RotateBoneRight(rUpperArm, lm[12], lm[14], Vector3.right);
    RotateBoneLeft(lUpperArm, lm[11], lm[13], Vector3.left);
    RotateBoneRight(rLowerArm, lm[14], lm[16], Vector3.right);
    RotateBoneLeft(lLowerArm, lm[13], lm[15], Vector3.left);
    
    // Kaki
    // RotateBoneRight(rUpperLeg, lm[24], lm[26], Vector3.right);
    // RotateBoneLeft(lUpperLeg, lm[23], lm[25], Vector3.left);
    // RotateBoneRight(rLowerLeg, lm[26], lm[28], Vector3.right);
    // RotateBoneLeft(lLowerLeg, lm[25], lm[27], Vector3.left);
     }
    catch (System.Exception ex)
    {
        Debug.LogError("Error in position logging: " + ex.Message);
    }
    //Tangan

//     Debug.DrawRay(rUpperArm.position, rUpperArm.right * 0.2f, Color.red);   // X axis
// Debug.DrawRay(rUpperArm.position, rUpperArm.up * 0.2f, Color.green);    // Y axis
// Debug.DrawRay(rUpperArm.position, rUpperArm.forward * 0.2f, Color.blue);// Z axis
    // RotateBone(rLowerArm, lm[14], lm[16], Vector3.right);
    // Debug.Log("lm12: " + lm[12]);
    // Debug.Log("lm14: " + lm[14]);
    // Debug.Log("lm16: " + lm[16]);

     // Badan

    // ======================
    // 🔥 TAMPILKAN VALUE
    // ======================

    // Debug.Log("Kepala      : " + headPos);
    // Debug.Log("Tangan Kanan: " + rightHandPos);
    // Debug.Log("Tangan Kiri : " + leftHandPos);
    // Debug.Log("Kaki Kiri   : " + leftFootPos);
    // Debug.Log("Kaki Kanan  : " + rightFootPos);
    // Debug.Log("Titik Tengah: " + centerPos);
}
void RotateBoneRight(Transform bone, NormalizedLandmark start, NormalizedLandmark end, Vector3 forwardAxis)
    {
        if (bone == null) return;

        // Hitung arah dari landmark A ke B
        Vector3 direction = new Vector3(
            (end.x - start.x), // Mirroring untuk hadap kamera
            -(end.y - start.y), 
            (end.z - start.z)
        );

        if (direction != Vector3.zero)
        {
            // Menentukan rotasi berdasarkan arah vektor
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up); // Atur 'up' sesuai kebutuhan (misal: Vector3.up)
            Quaternion correction = Quaternion.Euler(90, 0, 0);
            // Mixamo seringkali butuh penyesuaian sumbu (offset 90 derajat)
            // tergantung pada "T-Pose" awal avatar
            targetRotation *= correction;
                        // 🔥 ambil arah hasil rotasi
            // Debug.Log("Target Rotation (Euler): " + targetRotation.eulerAngles);
            // Debug.Log("Target correction (Euler): " + correction.eulerAngles);
            bone.rotation = Quaternion.Lerp(bone.rotation, targetRotation, Time.deltaTime * 10f) ;
        }
    }

void RotateBoneLeft(Transform bone, NormalizedLandmark start, NormalizedLandmark end, Vector3 forwardAxis)
    {
        if (bone == null) return;

        // Hitung arah dari landmark A ke B
        Vector3 direction = new Vector3(
            (end.x - start.x), // Mirroring untuk hadap kamera
            -(end.y - start.y), 
            (end.z - start.z)
        );

        if (direction != Vector3.zero)
        {
            // Menentukan rotasi berdasarkan arah vektor
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up); // Atur 'up' sesuai kebutuhan (misal: Vector3.up)
            Quaternion correction = Quaternion.Euler(90, 0, 0);
            // Mixamo seringkali butuh penyesuaian sumbu (offset 90 derajat)
            // tergantung pada "T-Pose" awal avatar
            targetRotation *= correction;
                        // 🔥 ambil arah hasil rotasi
            // Debug.Log("Target Rotation (Euler): " + targetRotation.eulerAngles);
            // Debug.Log("Target correction (Euler): " + correction.eulerAngles);
            bone.rotation = Quaternion.Lerp(bone.rotation, targetRotation, Time.deltaTime * 10f) ;
        }
    }
// void RotateBone(Transform bone, NormalizedLandmark a, NormalizedLandmark b)
//     {
//         if (bone == null) return;

//         Vector3 dir = new Vector3(
//             b.x - a.x,
//             -(b.y - a.y),
//             b.z - a.z
//         );

//         if (dir == Vector3.zero) return;

//         bone.rotation = Quaternion.Lerp(
//             bone.rotation,
//             Quaternion.LookRotation(dir, Vector3.up),
//             Time.deltaTime * 5f
//         );
//     }
    // void Update()
    // {
    //     if (controller == null || animator == null) return;

    //     var result = controller.GetLatestResult();
    //     if (result.poseLandmarks == null || result.poseLandmarks.Count == 0)
    //     {
    //         float timer = 0f;

    //         if (result.poseLandmarks == null || result.poseLandmarks.Count == 0)
    //         {
    //             timer += Time.deltaTime;

    //             if (timer > 1f)
    //                 avatarObject.SetActive(false);

    //             return;
    //         }
    //         else
    //         {
    //             timer = 0f;
    //             avatarObject.SetActive(true);
    //         }
    //         return;
    //     }
    //     else
    //     {
    //         // ✅ ada orang
    //         avatarObject.SetActive(true);
    //     }

    //     var lm = result.poseLandmarks[0].landmarks;
    //     if (lm == null || lm.Count <= 28) return;

    //      // ======================
    // // 🔥 MENAMPILKAN VALUE TIAP TITIK
    // // ======================
    // for (int i = 0; i < lm.Count; i++)
    // {
    //     Debug.Log(
    //         "Landmark " + i +
    //         " | X: " + lm[i].x +
    //         " | Y: " + lm[i].y +
    //         " | Z: " + lm[i].z
    //     );
    // }


    //     // ======================
    //     // 🔥 TANGAN
    //     // ======================
    //     RotateBone(rUpperArm, lm[12], lm[14]);
    //     RotateBone(rLowerArm, lm[14], lm[16]);

    //     RotateBone(lUpperArm, lm[11], lm[13]);
    //     RotateBone(lLowerArm, lm[13], lm[15]);

    //     // ======================
    //     // 🔥 KAKI
    //     // ======================
    //     RotateBone(rUpperLeg, lm[24], lm[26]);
    //     RotateBone(rLowerLeg, lm[26], lm[28]);

    //     RotateBone(lUpperLeg, lm[23], lm[25]);
    //     RotateBone(lLowerLeg, lm[25], lm[27]);

    //     // ======================
    //     // 🔥 BADAN
    //     // ======================
    //     Vector3 bodyDir = new Vector3(
    //         lm[12].x - lm[11].x,
    //         -(lm[12].y - lm[11].y),
    //         lm[12].z - lm[11].z
    //     );

    //     if (bodyDir != Vector3.zero)
    //     {
    //         spine.rotation = Quaternion.Lerp(
    //             spine.rotation,
    //             Quaternion.LookRotation(bodyDir, Vector3.up),
    //             Time.deltaTime * 3f
    //         );
    //     }
    // }

    public void ShowInfoPanel()
    {
        isInfoPanelOpen = true;

        avatarObject.SetActive(false);

        if(controller != null)
            controller.gameObject.SetActive(false);
            
        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "TNI"){
            infoPanelTniPria.SetActive(true);
            avatarObject.SetActive(false);
        }
        
        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Guru"){
            infoPanelGuruPria.SetActive(true);
            avatarObject.SetActive(false);
        }

        if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Pemadam"){
            infoPanelPemadamPria.SetActive(true);
            avatarObject.SetActive(false);
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Polisi"){
            infoPanelPolisiPria.SetActive(true);
            avatarObject.SetActive(false);
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Dokter"){
            infoPanelDokterPria.SetActive(true);
            avatarObject.SetActive(false);
        }

         if(GameData.JenisKelamin == "Pria" && GameData.Jenis_Pekerjaan == "Pilot"){
            infoPanelPilotPria.SetActive(true);
            avatarObject.SetActive(false);
        }
        


        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "TNI"){
            infoPanelTniWanita.SetActive(true);
            avatarObject.SetActive(false);
        }

        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Guru"){
            infoPanelGuruWanita.SetActive(true);
            avatarObject.SetActive(false);
        }

        if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Pemadam"){
            infoPanelPemadamWanita.SetActive(true);
            avatarObject.SetActive(false);
        }
    
         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Polisi"){
            infoPanelPolisiWanita.SetActive(true);
            avatarObject.SetActive(false);
        }

         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Dokter"){
            infoPanelDokterWanita.SetActive(true);
            avatarObject.SetActive(false);
        }

         if(GameData.JenisKelamin == "Wanita" && GameData.Jenis_Pekerjaan == "Pilot"){
            infoPanelPilotWanita.SetActive(true);
            avatarObject.SetActive(false);
        }
    }

    public void HideInfoPanel()
    {
        isInfoPanelOpen = false;

        if(controller != null)
            controller.gameObject.SetActive(true);

        avatarObject.SetActive(true);

        infoPanelTniPria.SetActive(false);
        infoPanelGuruPria.SetActive(false);
        infoPanelPemadamPria.SetActive(false);
        infoPanelPolisiPria.SetActive(false);
        infoPanelDokterPria.SetActive(false);
        infoPanelPilotPria.SetActive(false);

        infoPanelTniWanita.SetActive(false);
        infoPanelGuruWanita.SetActive(false);
        infoPanelPemadamWanita.SetActive(false);
        infoPanelPolisiWanita.SetActive(false);
        infoPanelDokterWanita.SetActive(false);
        infoPanelPilotWanita.SetActive(false);

        controller.gameObject.SetActive(false);
    }
}