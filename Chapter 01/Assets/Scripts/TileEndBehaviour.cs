using UnityEngine;

/// <summary>
/// Handles spawning a new tile and destroying this one
/// upon the player reaching the end
/// </summary>
public class TileEndBehaviour : MonoBehaviour
{
    [Tooltip("How much time to wait before destroying " + 
             "the tile after reaching the end")]
    public float destroyTime = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        // First check if we collided with the player
        if (other.gameObject.GetComponent<PlayerBehaviour>())
        {
            // If we did, spawn a new tile
            var gm = GameObject.FindObjectOfType<GameManager>();
            gm.SpawnNextTile();

            // And destroy this entire tile after a short delay
            Destroy(transform.parent.gameObject, destroyTime);
        }
    }
}
