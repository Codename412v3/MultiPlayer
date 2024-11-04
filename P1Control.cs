using UnityEngine;

public class WASDControllerWithCameraFollow : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stabilizationSpeed = 5f;
    public Transform cameraTransform;

    void Update()
    {
        // Mengabaikan pergerakan jika Arrow Keys ditekan
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            return;
        }

        // Mendapatkan input gerakan dari W, A, S, D
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Jika tidak ada input, hentikan pergerakan
        if (horizontal == 0 && vertical == 0)
        {
            return;
        }

        // Menghitung arah gerakan berdasarkan orientasi kamera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * vertical + right * horizontal).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, stabilizationSpeed * Time.deltaTime);
    }
}
