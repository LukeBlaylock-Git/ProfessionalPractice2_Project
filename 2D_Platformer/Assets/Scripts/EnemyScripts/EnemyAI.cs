using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyData Data;

    [Header("Scene References")]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform WallCheck;

    private Rigidbody2D RB;
    [Header("Stats")]
    [SerializeField] int CurrentHealth;
    [SerializeField] int Direction = 1; // 1 is right, -1 is left


    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();

        if (Data != null)
        {
            CurrentHealth = Data.MaxHealth; //Getting the health from data.
        }
        else
        {
            CurrentHealth = 1;
        }
    }
    void FixedUpdate()
    {
        if (Data == null) return; //if data is empty, nothing happens, prevents things from breaking

        RB.linearVelocity = new Vector2(Direction * Data.MoveSpeed, RB.linearVelocity.y);

        bool GroundAhead = Physics2D.Raycast(GroundCheck.position, Vector2.down, Data.GroundCheckDistance, Data.GroundMask);

        bool WallAhead = Physics2D.Raycast(WallCheck.position, Vector2.right * Direction, Data.WallCheckDistance, Data.GroundMask);

        if (GroundAhead == false || WallAhead == true)
        {
            Flip();
        } 
    }
    public void TakeDamage(int Damage)
    {
        CurrentHealth = CurrentHealth - Damage;

        if(CurrentHealth <= 0)
        {
            Destroy(gameObject); //Additional features such as scoring will go here later.
        }
    }
    void Flip()
    {
        Direction = Direction * -1; //Inverts our enemy

        Vector3 Scale = transform.localScale;
        Scale.x = Mathf.Abs(Scale.x) * Direction;
        transform.localScale = Scale;
    }
}
