using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Default Kill Zone")]
    public float KillZoneY = -10f;

    [Header("Spawn")]
    public Transform SpawnPoint; //Drag a object here to assign it as the respawn point.

    private Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y < KillZoneY)
        {
            Respawn();
        }
    }
    public void Respawn()
    {
        if (SpawnPoint != null)
            transform.position = SpawnPoint.position;
        else transform.position = Vector3.zero; //Fallback, this shouldnt ever be needed, but just incase.

        RB.linearVelocity = Vector2.zero;
        RB.angularVelocity = 0f;
    }
}
