using UnityEngine;

public class WakeOnDistance : MonoBehaviour
{
    [SerializeField] private Transform targets; //Will wakeup all flying enemies
    private GameObject player;
    [SerializeField] private float dist = 50f;  // Setting distance   

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < dist)
        {
            GetComponent<Rigidbody2D>().WakeUp();
            FindObjectOfType<AudioManager>().Play("PlayerWalk");
        }

    }
}

//Reference
// https://discussions.unity.com/t/how-to-set-object-in-game-to-awaken-on-distance-from-character/581867/9 
//This was used as a base for this