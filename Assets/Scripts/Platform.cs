using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;

    private void Update()
    {
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, 0f, (int)transform.rotation.z - Time.deltaTime * _rotateSpeed);
        }

        else if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, 0f, (int)transform.rotation.z + Time.deltaTime * _rotateSpeed);
        }
    }
}