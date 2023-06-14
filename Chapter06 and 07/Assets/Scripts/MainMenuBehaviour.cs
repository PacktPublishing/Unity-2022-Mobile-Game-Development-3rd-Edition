using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene

public class MainMenuBehaviour : MonoBehaviour
{
    /// <summary>
    /// Will load a new scene upon being called
    /// </summary>
    /// <param name="levelName">The name of the level we want
    /// to go to</param>
    public void LoadLevel(string levelName)
    {
        if (UnityAdController.showAds)
        {
            /* Show an ad */
            UnityAdController.ShowAd();
        }

        SceneManager.LoadScene(levelName);
    }

    public void DisableAds()
    {
        UnityAdController.showAds = false;

        /* Used to store that we shouldn't show ads */
        PlayerPrefs.SetInt("Show Ads", 0);
    }

    protected virtual void Start()
    {
        /* Initialize the showAds variable */
        bool showAds = (PlayerPrefs.GetInt("Show Ads", 1) == 1);
        UnityAdController.showAds = showAds;
    }

}
