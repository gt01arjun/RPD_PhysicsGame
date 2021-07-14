using UnityEngine;

public class Platform : MonoBehaviour
{
    private float yaw;
    private float pitch;

    [SerializeField]
    private float _rotateSpeed;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += _rotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            pitch -= _rotateSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}