using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene

public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How long to wait before restarting the game")]
    public float waitTime = 2.0f;

    private void OnCollisionEnter(Collision collision)
    {
        // First check if we collided with the player
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            // Destroy the player
            Destroy(collision.gameObject);

            // Call the function ResetGame after waitTime
            // has passed
            Invoke("ResetGame", waitTime);
        }
    }

    /// <summary>
    /// Will restart the currently loaded level
    /// </summary>
    private void ResetGame()
    {
        // Get the current level's name
        string sceneName = SceneManager.GetActiveScene().name;

        // Restarts the current level
        SceneManager.LoadScene(sceneName);
    }
}
