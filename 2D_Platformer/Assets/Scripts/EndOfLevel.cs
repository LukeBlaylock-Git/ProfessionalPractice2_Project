using UnityEngine;
using UnityEngine.SceneManagement;
public class EndOfLevel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D Hit)
    {
        if (Hit.collider.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
