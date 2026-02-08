using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemies/EnemyData")] //Allows us when we "right click" to create the new enemy.
public class EnemyData : ScriptableObject
{
    [Header("Stats")] //Generic stats for this enemy
    public int MaxHealth = 1;
    public float MoveSpeed = 2f;
    public int ContactDamage = 1;

    [Header("Patrol Config")] //Generic patrol settings for this enemy
    public float GroundCheckDistance = 0.6f;
    public float WallCheckDistance = 0.2f;

    [Header("Physics")] //For Raycasting.
    public LayerMask GroundMask;
}
