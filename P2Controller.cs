using UnityEngine;

public class ArrowKeyControllerWithCameraFollow : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stabilizationSpeed = 5f;
    public Transform cameraTransform;

    void Update()
    {
        // Mengabaikan pergerakan jika W, A, S, D ditekan
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return;
        }

        // Mendapatkan input gerakan dari Arrow Keys
        float moveX = 0;
        float moveZ = 0;

        if (Input.GetKey(KeyCode.UpArrow))
            moveZ = 1;
        if (Input.GetKey(KeyCode.DownArrow))
            moveZ = -1;
        if (Input.GetKey(KeyCode.LeftArrow))
            moveX = -1;
        if (Input.GetKey(KeyCode.RightArrow))
            moveX = 1;

        // Jika tidak ada input, hentikan pergerakan
        if (moveX == 0 && moveZ == 0)
        {
            return;
        }

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * moveZ + right * moveX).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, stabilizationSpeed * Time.deltaTime);
    }
}
