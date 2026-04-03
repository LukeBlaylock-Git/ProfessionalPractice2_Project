using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public EnemyData Data;
    public PlayerData PData;
    public PlayerTakeDmg PlayerTakeDmg;

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
        if (Hit.collider.CompareTag("PHitbox"))
        {

            CurrentHealth = CurrentHealth - DamageTaken;

            //Debug.Log("Took Damage");

            if (CurrentHealth <= 0)
            {
                FindObjectOfType<AudioManager>().Play("EnemyDead");
                Destroy(gameObject);
                //PlayerTakeDmg.GiveHealth();
            }
        }
    }

    private void IsDead()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            
        }
    }

    private void FixedUpdate()
    {
        //IsDead();
    }

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Destroy(gameObject);
        }
    } 
   testing purposes
   */

}
