using UnityEngine;

public class CameraFollowFreeLook : MonoBehaviour
{
    public Transform target; // Target untuk di-follow, misalnya karakter pemain
    public float distance = 5.0f; // Jarak antara kamera dan target
    public float height = 2.0f; // Tinggi kamera relatif terhadap target
    public float rotationSpeed = 5.0f; // Kecepatan rotasi kamera

    private float currentX = 0.0f; // Rotasi X (horizontal)
    private float currentY = 0.0f; // Rotasi Y (vertikal)
    public float yMinLimit = -40f; // Batas minimum sudut vertikal
    public float yMaxLimit = 80f;  // Batas maksimum sudut vertikal

    void Start()
    {
        // Mengunci cursor ke tengah layar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Mengambil input mouse
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            
            // Membatasi sudut vertikal
            currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

            // Mengatur rotasi kamera berdasarkan input
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            
            // Mengatur posisi kamera
            Vector3 position = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);
            
            // Menerapkan posisi dan rotasi ke kamera
            transform.position = position;
            transform.LookAt(target);
        }
    }
}
