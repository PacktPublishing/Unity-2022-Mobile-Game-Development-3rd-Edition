using System; // DateTime
using UnityEngine;
using UnityEngine.Advertisements; /* Advertisement class */

public class UnityAdController : MonoBehaviour, IUnityAdsShowListener
{
    /// <summary>
    /// A static reference to this object
    /// </summary>
    public static UnityAdController instance;

    /// <summary>
    /// If we should show ads or not
    /// </summary>
    public static bool showAds = true;

    // Nullable type
    public static DateTime? nextRewardTime = null;


    /// <summary>
    /// For holding the obstacle for continuing the game
    /// </summary>
    public static ObstacleBehaviour obstacle;


    /// <summary>
    /// Replace with your actual gameId
    /// </summary>
    private string gameId = "4861409";

    /// <summary>
    /// If the game is in test mode or not
    /// </summary>
    private bool testMode = true;


    /// <summary>
    /// Unity Ads must be initialized or else ads will not work
    /// properly
    /// </summary> 
    private void Start()
    {
        /* No need to initialize if it already is done */
        if (!Advertisement.isInitialized)
        {
            instance = this;
            // Use the functions provided by this to allow custom
            Advertisement.Initialize(gameId, testMode);
        }
    }

    /// <summary>
    /// Will get the appropriate Ad ID for the platform we are on
    /// </summary>
    /// <returns>A usable Ad ID</returns>
    private static string GetAdID()
    {
        string adID = "Interstitial_";

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            adID += "iOS";
        }
        else
        {
            adID += "Android";
        }

        return adID;
    }

    /// <summary>
    /// Will load and display an ad on the screen
    /// </summary>
    public static void ShowAd()
    {
        // Load an Ad to play
        Advertisement.Load(GetAdID());

        // Display it after it is loaded
        Advertisement.Show(GetAdID(), instance);
    }

    #region IUnityAdsShowListener Methods

    /// <summary>
    /// This callback method handles logic for the ad starting to play.
    /// </summary>
    /// <param name="placementId">The identifier for the Ad Unit showing the content.</param>
    public void OnUnityAdsShowStart(string placementId)
    {
        /* Pause game while ad is shown */
        PauseScreenBehaviour.paused = true; 
        Time.timeScale = 0f;
    }

    /// <summary>
    /// This callback method handles logic for the ad finishing.
    /// </summary>
    /// <param name="placementId">The identifier for the Ad Unit showing the content</param>
    /// <param name="showCompletionState">Indicates the final state of the ad (whether the ad was skipped or completed).</param>
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        /* If there is an obstacle, we can remove it to continue the
           game */
        if (obstacle != null && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            obstacle.Continue();
        }

        /* Unpause game when ad is over */
        PauseScreenBehaviour.paused = false;
        Time.timeScale = 1f;
    }

    /* This callback method handles logic for the user clicking on the ad. */
    public void OnUnityAdsShowClick(string placementId) { }

    /* This callback method handles logic for the Ad Unit failing to show. */
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    #endregion

    public static void ShowRewardAd()
    {
        nextRewardTime = DateTime.Now.AddSeconds(15);

        ShowAd();
    }

}