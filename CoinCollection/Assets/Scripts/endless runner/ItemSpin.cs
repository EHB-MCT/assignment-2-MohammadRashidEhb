using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 90f;

    public enum RotationAxis { X, Y, Z }
    [SerializeField] private RotationAxis rotationAxis = RotationAxis.Z;

    private void Update()
    {
        Vector3 rotationVector = Vector3.zero;

        switch (rotationAxis)
        {
            case RotationAxis.X:
                rotationVector = new Vector3(rotateSpeed * Time.deltaTime, 0f, 0f);
                break;
            case RotationAxis.Y:
                rotationVector = new Vector3(0f, rotateSpeed * Time.deltaTime, 0f);
                break;
            case RotationAxis.Z:
                rotationVector = new Vector3(0f, 0f, rotateSpeed * Time.deltaTime);
                break;
        }

        transform.Rotate(rotationVector);
    }
}
