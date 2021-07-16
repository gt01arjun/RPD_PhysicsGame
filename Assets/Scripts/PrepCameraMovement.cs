using UnityEngine;

public class PrepCameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _rotateSpeed;

    private float yaw;
    private float pitch;

    private float x;
    private float y;
    private float z;

    [SerializeField]
    private float _xMin;
    [SerializeField]
    private float _xMax;

    [SerializeField]
    private float _yMin;
    [SerializeField]
    private float _yMax;

    [SerializeField]
    private float _zMin;
    [SerializeField]
    private float _zMax;

    private bool _hasMoved;

    private void Start()
    {
        yaw = 0.0f;
        pitch = 0.0f;
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        _hasMoved = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
            return;

        transform.position += Input.GetAxis("Horizontal") * transform.right * Time.deltaTime * _moveSpeed;
        transform.position += Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * _moveSpeed;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _hasMoved = true;
        }

        float yDir = 0;

        if (Input.GetKey("q")) yDir = 1;
        if (Input.GetKey("e")) yDir = -1;

        transform.position += transform.up * _moveSpeed * yDir * Time.deltaTime;

        x = Mathf.Clamp(transform.position.x, _xMin, _xMax);
        y = Mathf.Clamp(transform.position.y, _yMin, _yMax);
        z = Mathf.Clamp(transform.position.z, _zMin, _zMax);

        transform.position = new Vector3(x, y, z);

        if (_hasMoved)
        {
            yaw += _rotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            pitch -= _rotateSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}