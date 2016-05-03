using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class MainMenu : MonoBehaviour {
    private bool _isFirstMenu = true;
 
    public string testAchievement = "CgkI87fn0oQMEAIQAQ";
    public string resourcesAchievement = "CgkI87fn0oQMEAIQAg";
    public string leaderboard = "CgkI87fn0oQMEAIQBg";

    

    // Use this for initialization
    void Awake()
    {
        
        DebugConsole.isVisible = true;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       // enables saving game progress.
       .EnableSavedGames()
       .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        DebugConsole.Log("Activation...", "RED");
        // authenticate user:
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
            if (success)
            {
                Debug.Log("Login Succesful!");
                DebugConsole.Log("Login Succesful!", "RED");
            }
            else
            {
                Debug.Log("Login failed.");
                DebugConsole.Log("Login failed.", "RED");
            }
        });

    }

    void Start () {
                  
    }	
	// Update is called once per frame
	void Update () {	
	}
    void OnGUI() {
        FirstMenu();        
    }
    void FirstMenu() {
        if (_isFirstMenu)
        {
            if (GUI.Button(new Rect(Screen.width /2 -75, Screen.height / 2 - 200, 150, 50), "Game"))
            {

                SceneManager.LoadScene("PRoject");
            }
            if (GUI.Button(new Rect(Screen.width/ 2 - 75, Screen.height / 2 - 150, 150, 50), "Show Saved Games"))
            {
                DebugConsole.Log("UI", "RED");
                ShowSelectUI();
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 50), "Show Leaderboard"))
            {
                Social.ShowLeaderboardUI();
                DebugConsole.Log("leaderboard", "RED");
            }
           
            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 50), "Show Achievements"))
            {
                Social.ShowAchievementsUI();
            }
            

            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2, 150, 50), "Post points to leaderboard/login"))
            {
                Social.ReportScore(13555, leaderboard, (bool success) => {
                    // handle success or failure
                });
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2  +50, 150, 50), "Unlock Achievement"))
            {

                PlayGamesPlatform.Instance.IncrementAchievement(
                resourcesAchievement, 5, (bool success) => {
            // handle success or failure
        });
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 130, 150, 50), "Quit"))
            {

                Application.Quit();
            }

        }

    }

    public void ShowSelectUI()
    {
        uint maxNumToDisplay = 3;
        bool allowCreateNew = true;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }
    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
        }
        else
        {
            // handle cancel or error
        }
    }





}
