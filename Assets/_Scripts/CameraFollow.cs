using UnityEngine;
public class CameraFollow: MonoBehaviour{

    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    void LateUpdate()
    {
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;

        Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(0, desiredPosition.y, desiredPosition.z);
    }
}