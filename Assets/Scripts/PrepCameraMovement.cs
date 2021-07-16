using UnityEngine;

public class PrepCameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _rotateSpeed;

    private float yaw;
    private float pitch;

    private void Start()
    {
        yaw = 0.0f;
        pitch = 0.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
            return;

        transform.position += Input.GetAxis("Horizontal") * transform.right * Time.deltaTime * _moveSpeed;
        transform.position += Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * _moveSpeed;

        float yDir = 0;

        if (Input.GetKey("q")) yDir = 1;
        if (Input.GetKey("e")) yDir = -1;
        transform.position += transform.up * _moveSpeed * yDir * Time.deltaTime;

        yaw += _rotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        pitch -= _rotateSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

       // yaw = Mathf.Clamp(yaw, -30f, 30f);
       // pitch = Mathf.Clamp(pitch, -30f, 30f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}