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
        if (Hit.collider.CompareTag("EnemyHead"))
        {
            //GiveHealth();
        }

        if (Hit.collider.CompareTag("Enemy"))
        {
            CurrentHealth = CurrentHealth - DamageTaken;
            Debug.Log("Player Took Damage");
            Debug.Log(CurrentHealth);

            if (CurrentHealth <= 0)
            {   
                FindObjectOfType<AudioManager>().Play("PlayerRespawn");
                CurrentHealth = PData.MaxHealth;
                Respawn.Respawn();
            }
        }

        if (Hit.collider.CompareTag("DeathPlane"))
        {
            CurrentHealth = CurrentHealth - 50;
            Debug.Log("Player Died");
            //Debug.Log(CurrentHealth);

            if (CurrentHealth <= 0)
            {   
                FindObjectOfType<AudioManager>().Play("PlayerRespawn");
                CurrentHealth = PData.MaxHealth;
                Respawn.Respawn();
            }
        }
    }

    public void GiveHealth()
    {
        CurrentHealth = CurrentHealth + 1;
        Debug.Log(CurrentHealth);
    }
}
