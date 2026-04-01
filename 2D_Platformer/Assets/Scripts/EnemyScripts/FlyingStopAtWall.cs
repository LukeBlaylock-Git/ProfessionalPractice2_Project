using UnityEngine;

public class FlyingStopAtWall : MonoBehaviour
{
    public EnemyData Data;
    public PlayerData PData;

    private Rigidbody2D RB;
    [Header("Stats")]
    [SerializeField] int CurrentHealth;
    [SerializeField] int Damage = 1;
    [SerializeField] int DamageTaken = 10;



    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();

        if (Data != null)
        {
            CurrentHealth = Data.MaxHealth; //Getting the health from data.
            Damage = Data.ContactDamage;
            DamageTaken = PData.Damage;
        }
        else
        {
            CurrentHealth = 1;
            Damage = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D Hit)
    {
        if (Hit.collider.CompareTag("Ground"))
        {
            CurrentHealth = CurrentHealth - Data.MaxHealth;
            Debug.Log("HitWall");

            if (CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
