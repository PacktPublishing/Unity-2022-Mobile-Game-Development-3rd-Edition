using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene
using System.Collections.Generic; // List
using Facebook.Unity; // FB
using UnityEngine.UI; // Image
using TMPro; //TextMeshProUGUI

public class MainMenuBehaviour : MonoBehaviour
{
    [Header("Object References")] 
    public GameObject mainMenu; 
    public GameObject facebookLogin;

    [Tooltip("Will display the user's Facebook profile pic")] 
    public Image profilePic;

    [Tooltip("The text object used to display the greeting")] 
    public TextMeshProUGUI greeting;

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


#region Facebook

    public void Awake()
    {
        /* We only call FB Init once, so check if it has been called
        /* already */
        if (!FB.IsInitialized)
        {
            FB.Init(OnInitComplete, OnHideUnity);
        }
    }

    /// <summary>
    /// Once initialized, will inform if logged in on Facebook
    /// </summary>
    private void OnInitComplete()
    {
        if(FB.IsInitialized)
        {
            if (FB.IsLoggedIn)
            {
                print("Logged into Facebook");

                /* Close Login and open Main Menu */
                ShowMainMenu();
            }
        }
        else
        {
            print("Failed to init Facebook SDK; open as guest");
            ShowMainMenu();
        }

    }

    /// <summary>
    /// Called whenever Unity loses focus
    /// </summary>
    /// <param name="active">If the game is currently active</param>
    private void OnHideUnity(bool active)
    {
        /* Set TimeScale based on if the game is paused */
        Time.timeScale = (active) ? 1 : 0; 
    }

    /// <summary>
    /// Attempts to log in on Facebook
    /// </summary>
    public void FacebookLogin()
    {
        List<string> permissions = new List<string>();

        /* Add permissions we want to have here */
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, FacebookCallback);
    }

    /// <summary>
    /// Called once facebook has logged in, or not
    /// </summary>
    /// <param name="result">The result of our login request</param> 
    private void FacebookCallback(IResult result)
    {
        if (result.Error == null)
        {
            OnInitComplete();
        }
        else
        {
            print(result.Error);
        }
    }

    public void ShowMainMenu()
    {
        if (facebookLogin != null && mainMenu != null)
        {
            facebookLogin.SetActive(false); 
            mainMenu.SetActive(true);

            if (FB.IsLoggedIn)
            {
                /* Get information from Facebook profile */
                FB.API("/me?fields=name", 
                       HttpMethod.GET, 
                       SetName); 
                FB.API("/me/picture?width=256&height=256", 
                       HttpMethod.GET, 
                       SetProfilePic);
            }
        }
    }

    private void SetName(IResult result)
    {
        if (result.Error != null)
        {
            print(result.Error); return;
        }

        string playerName = result.ResultDictionary["name"].ToString();

        if (greeting != null)
        {
            greeting.text = "Hello, " + playerName + "!";
            greeting.gameObject.SetActive(true);
        }

    }

    private void SetProfilePic(IGraphResult result)
    {
        if (result.Error != null)
        {
            print(result.Error); return;
        }

        // Variable setup
        int texWidth = result.Texture.width;
        int texHeight = result.Texture.height;
        Rect rect = new Rect(0, 0, texWidth, texHeight);
        Vector2 pivot = Vector2.zero;
        Texture2D texture = result.Texture;

        // Create the profile pic
        Sprite fbImage = Sprite.Create(texture, rect, pivot);
        
        if (profilePic != null)
        {
            profilePic.sprite = fbImage;
            profilePic.gameObject.SetActive(true);
        }
    }

    #endregion




}
