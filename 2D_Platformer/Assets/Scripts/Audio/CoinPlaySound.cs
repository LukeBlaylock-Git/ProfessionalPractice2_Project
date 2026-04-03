using UnityEngine;

public class CoinPlaySound : MonoBehaviour
{
    public string soundName = "CoinPickup";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play(soundName);
            Destroy(gameObject);
        }
    }
}
