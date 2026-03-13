using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target; //Set to Player
    public float SmoothSpeed = 5f;

    private Vector3 Offset;

    void Start()
    {
        Offset = transform.position - Target.position;
    }

    void LateUpdate()
    {
        Vector3 DesiredPosition = Target.position + Offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position,DesiredPosition,SmoothSpeed * Time.deltaTime);
        transform.position = SmoothedPosition;
    }
}
