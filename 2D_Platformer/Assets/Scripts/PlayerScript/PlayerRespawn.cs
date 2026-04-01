using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public PlayerData PData;
    [SerializeField] float CurrentHealth;

    [Header("Default Kill Zone")]
    public float KillZoneY = -10f;

    [Header("Spawn")]
    public Transform SpawnPoint; //Drag a object here to assign it as the respawn point.

    private Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        if (PData != null)
        {
            CurrentHealth = PData.MaxHealth; //Getting the health from Player data.
        }
        else
        {
            CurrentHealth = 3;
        }
    }

    private void Update()
    {
        if (transform.position.y < KillZoneY)
        {
            Respawn();
        }

        if (CurrentHealth <= 0) {
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
