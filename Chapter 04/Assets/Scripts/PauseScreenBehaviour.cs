using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager

public class PauseScreenBehaviour : MainMenuBehaviour
{
    /// <summary>
    /// If our game is currently paused
    /// </summary>
    public static bool paused;

    [Tooltip("Reference to the pause menu object to turn on/off")]
    public GameObject pauseMenu;

    [Tooltip("Reference to the on screen controls menu")]
    public GameObject onScreenControls;

    /// <summary>
    /// Reloads our current level, effectively "restarting" the
    /// game
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Will turn our pause menu on or off
    /// </summary>
    /// <param name="isPaused">is the game currently paused</param>
    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;

        /* If the game is paused, timeScale is 0, otherwise 1 */
        Time.timeScale = (paused) ? 0 : 1; 

        pauseMenu.SetActive(paused);
        onScreenControls.SetActive(!paused);
    }

    void Start()
    {
        /* Must be reset in Start or else game will be paused upon
         * restart */
        SetPauseMenu(false);
    }
}
