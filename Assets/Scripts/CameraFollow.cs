using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    //public float smoothSpeed = 0.125f;
    //public Vector3 offset;

    void LateUpdate()
    {
       /* //Vector3 desiredPosition = target.position + offset;
        Vector3 desiredPosition = Vector3.zero;

        desiredPosition.x = target.position.x;
        desiredPosition.y = target.position.y;
        desiredPosition.z = transform.position.z;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;*/ 

         transform.LookAt(transform.position + target.forward);


        //transform.rotation = Quaternion.Euler(target.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
    }
}