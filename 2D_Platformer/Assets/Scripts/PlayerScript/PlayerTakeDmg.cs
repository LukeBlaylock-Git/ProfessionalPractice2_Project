using UnityEngine;

public class PlayerTakeDmg : MonoBehaviour
{

    public EnemyData Data;
    public PlayerData PData;
    public PlayerRespawn Respawn;

    private Rigidbody2D RB;
    [Header("Stats")]
    [SerializeField] float CurrentHealth;
    [SerializeField] int Damage = 10;
    [SerializeField] int DamageTaken = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();

        if (Data != null)
        {
            CurrentHealth = PData.MaxHealth; //Getting the health from Player data.
            DamageTaken = Data.ContactDamage;
            Damage = PData.Damage;
        }
        else
        {
            CurrentHealth = 3;
            Damage = 1;
            DamageTaken = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D Hit)
    {
        if (Hit.collider.CompareTag("Enemy"))
        {
            CurrentHealth = CurrentHealth - DamageTaken;
            Debug.Log("Player Took Damage");
            Debug.Log(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                CurrentHealth = PData.MaxHealth;
                Respawn.Respawn();
            }
        }
    }
}
