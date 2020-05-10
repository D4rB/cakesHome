using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position,desirePosition,smoothSpeed);
        transform.position = smoothPosition;
    }

}
