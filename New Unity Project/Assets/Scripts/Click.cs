using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using GooglePlayGames.BasicApi;
using LitJson;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;

public class Click : MonoBehaviour {
   
    public UnityEngine.UI.Text mDisplay;
    public UnityEngine.UI.Text cDisplay;
    public UnityEngine.UI.Text dcDisplay;
    public double metal = 1000.00f;
    public double crystal = 1000.00f;
    public double darkMatter = 0.00f;
    public ItemManager[] items;
    public Technologies[] tech;
    
    
    //save
    public byte[] saveData;
    
    public GameStatsSaveObject playerData;
    public Texture2D savedImage;
    public string AutoSaveName;
    TimeSpan playtime;
    private Texture2D mScreenImage;
    bool _toSave;
    bool _loadgame;


    public double GetMetal() { return metal; }
    public double GetCrystal() { return crystal; }
    public double GetDarkMatter() { return darkMatter; }



    void Awake()
    {
        AutoSaveName = "AutoSave";
        playtime = new TimeSpan(10, 10, 10);
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
    void Start() {
        DebugConsole.isVisible = true;
        ShowSelectUI();
        

        //savedImage = getScreenshot();
        //jsonstring = File.ReadAllText(Application.dataPath + "/Resources/Player.json");
        //statsData = JsonMapper.ToObject(jsonstring);
        //LoadStats();
    }

    void Update(){
        dcDisplay.text = "Dark Matter: " + CurrencyConverter.Instance.GetCurrencyIntoString(darkMatter);
        mDisplay.text = "Metal: " + CurrencyConverter.Instance.GetCurrencyIntoString(metal);
        cDisplay.text = "Crystal: " + CurrencyConverter.Instance.GetCurrencyIntoString(crystal);

    }
    public void menu()
    {        
        SceneManager.LoadScene("MainMenu");
    }
    public byte[] Save() {
        DebugConsole.Log("Creating save data array", "green");
        int metalmineT1 = GameObject.FindGameObjectWithTag("metalmineT1").GetComponent<ItemManager>().buildingLevel;
        int crystalmineT1 = GameObject.FindGameObjectWithTag("crystalmineT1").GetComponent<ItemManager>().buildingLevel;
        int metalmineT2 = GameObject.FindGameObjectWithTag("metalmineT2").GetComponent<ItemManager>().buildingLevel;
        int crystalmineT2 = GameObject.FindGameObjectWithTag("crystalmineT2").GetComponent<ItemManager>().buildingLevel;
        int metalmineT3 = GameObject.FindGameObjectWithTag("metalmineT3").GetComponent<ItemManager>().buildingLevel;
        int crystalmineT3 = GameObject.FindGameObjectWithTag("crystalmineT3").GetComponent<ItemManager>().buildingLevel;
        int metalUpgradeT1 = GameObject.FindGameObjectWithTag("metalUpgradeT1").GetComponent<Technologies>().techLevel;
        int crystalUpgradeT1 = GameObject.FindGameObjectWithTag("crystalUpgradeT1").GetComponent<Technologies>().techLevel;

        playerData = new GameStatsSaveObject(GetMetal(), GetCrystal(), GetDarkMatter(), metalmineT1, crystalmineT1, metalmineT2, crystalmineT2, metalmineT3, crystalmineT3, metalUpgradeT1, crystalUpgradeT1);
        saveData = ObjectToByteArray(playerData);
        DebugConsole.Log("Returning data array", "green");
        return saveData;
        //playerJson = JsonMapper.ToJson(playerData);
        
       //File.WriteAllText(Application.dataPath + "/Resources/Player.json", playerJson.ToString());
    }

    public void SaveGameButton()
    {
        _loadgame = false;
        _toSave = true;
        OpenSavedGame(AutoSaveName);

    }
    public void LoadGameButton()
    {
        _loadgame = false;
        _toSave = false;
         OpenSavedGame(AutoSaveName);
        

    }
    [Serializable]
    public class GameStatsSaveObject
    {
        public double metal;
        public double crystal;
        public double darkMatter;
        public int metalmineT1;
        public int crystalmineT1;
        public int metalmineT2;
        public int crystalmineT2;
        public int metalmineT3;
        public int crystalmineT3;
        public int metalUpgradeT1;
        public int crystalUpgradeT1;
        
        public GameStatsSaveObject (double metal, double crystal, double darkMatter,int metalT1, int crystalT1, int metalT2, int crystalT2, int metalT3, int crystalT3, int upmT1, int upcT1)
        {
            this.metal = metal;
            this.crystal = crystal;
            this.darkMatter = darkMatter;
            this.metalmineT1 = metalT1;
            this.metalmineT2 = metalT2;
            this.metalmineT3 = metalT3;
            this.crystalmineT1 = crystalT1;
            this.crystalmineT2 = crystalT2;
            this.crystalmineT3 = crystalT3;
            this.metalUpgradeT1 = upmT1;
            this.crystalUpgradeT1 = upcT1;
        }

    }

    public void LoadStats(byte[] data) {
        GameStatsSaveObject s = ByteArrayToObject(data);

        metal = s.metal;
        crystal = s.crystal;
        darkMatter = s.darkMatter;
        GameObject.FindGameObjectWithTag("metalmineT1").GetComponent<ItemManager>().buildingLevel = s.metalmineT1;
        GameObject.FindGameObjectWithTag("crystalmineT1").GetComponent<ItemManager>().buildingLevel = s.crystalmineT1;
        GameObject.FindGameObjectWithTag("metalmineT2").GetComponent<ItemManager>().buildingLevel = s.metalmineT2;
        GameObject.FindGameObjectWithTag("crystalmineT2").GetComponent<ItemManager>().buildingLevel = s.metalmineT2;
        GameObject.FindGameObjectWithTag("metalmineT3").GetComponent<ItemManager>().buildingLevel = s.metalmineT2;
        GameObject.FindGameObjectWithTag("crystalmineT3").GetComponent<ItemManager>().buildingLevel = s.crystalmineT3;
        GameObject.FindGameObjectWithTag("metalUpgradeT1").GetComponent<Technologies>().techLevel = s.metalUpgradeT1;
        GameObject.FindGameObjectWithTag("crystalUpgradeT1").GetComponent<Technologies>().techLevel = s.crystalUpgradeT1;

    }
    //http://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
    // Convert an object to a byte array
    public byte[] ObjectToByteArray(GameStatsSaveObject obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
    // Convert a byte array to an Object
    // Convert a byte array to an Object
    private GameStatsSaveObject ByteArrayToObject(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        GameStatsSaveObject obj = (GameStatsSaveObject)binForm.Deserialize(memStream);

        return obj;
    }


    


    /// <summary>
    /// Save Game Code
    /// </summary>
    /// <param name="filename"></param>
    /// 
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
            _toSave = false;
            _loadgame = true;
            string filename = game.Filename;
            Debug.Log("opening saved game:  " + game);
            if (_toSave && (filename == null || filename.Length == 0))
            {
                filename = "save" + DateTime.Now.ToBinary();
            }
                //open the data.
                ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename,
                    DataSource.ReadCacheOrNetwork,
                    ConflictResolutionStrategy.UseLongestPlaytime,
                    OnSavedGameOpened);
        }
        else
        {
            Debug.LogWarning("Error selecting save game: " + status);
        }
    }

    void OpenSavedGame(string filename)
    {
        
        Debug.Log("Saving progress to the cloud...");
        
        if (filename == null)
        {
            Debug.Log("null...");
            ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI("Save game progress",
                4, true, true, OnSavedGameSelected);
        }
        else
        {
            // save to named file
            Debug.Log("not null...");

            //((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(filename,
                          //  DataSource.ReadCacheOrNetwork,
                          //  ConflictResolutionStrategy.UseLongestPlaytime,
                          //  OnSavedGameOpened);
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
              ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            
            DebugConsole.Log("Save Opened!", "green");
            if (_toSave)
            {
                DebugConsole.Log("Saving", "green");
                if (mScreenImage == null)
                {
                    CaptureScreenshot();
                }
                byte[] pngData = (mScreenImage != null) ? mScreenImage.EncodeToPNG() : null;
                byte[] data = Save();
                TimeSpan playedTime = new TimeSpan(1, 1, 1);
                DebugConsole.Log("byte array aquired", "green");
                SavedGameMetadataUpdate.Builder builder = new
                SavedGameMetadataUpdate.Builder()
                    .WithUpdatedPlayedTime(playedTime)
                    .WithUpdatedDescription("Saved Game at " + DateTime.Now);

                if (pngData != null)
                {
                    Debug.Log("Save image of len " + pngData.Length);
                    builder = builder.WithUpdatedPngCoverImage(pngData);
                }
                else
                {
                    DebugConsole.Log("No image", "green");
                }
                SavedGameMetadataUpdate updatedMetadata = builder.Build();
                DebugConsole.Log("build", "green");
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updatedMetadata, data, OnSavedGameWritten);
                //PlayGamesPlatform.Instance.SavedGame.CommitUpdate(game, updatedMetadata, data, OnSavedGameWritten);

            }
            else
            {
                DebugConsole.Log("loading", "green");
                AutoSaveName = game.Filename;
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
                //PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
                DebugConsole.Log("Data red", "green");


            }
            DebugConsole.Log("Save Opened!2", "green");
        }
        else
        {
            DebugConsole.Log("Save Open failed.", "red");
        }
    }


    //WRITE

    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        
        DebugConsole.Log("SaveGame Script....", "green");
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        DebugConsole.Log("1", "green");
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        DebugConsole.Log("2", "green");
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            DebugConsole.Log("Saved.", "green");
        }
        else
        {
            DebugConsole.Log("Save failed.", "red");
        }
    }


    
    public Texture2D getScreenshot()
    {
        // Create a 2D texture that is 1024x700 pixels from which the PNG will be
        // extracted
        Texture2D screenShot = new Texture2D(1024, 700);

        // Takes the screenshot from top left hand corner of screen and maps to top
        // left hand corner of screenShot texture
        screenShot.ReadPixels(
            new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
        return screenShot;
    }


    //READ


    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
            DebugConsole.Log("Save Opened Loading game save.", "green");
            LoadStats(data);

        }
        else
        {
            DebugConsole.Log("Save Not red...", "red");
            // handle error
        }
    }


    public void CaptureScreenshot()
    {
        mScreenImage = new Texture2D(Screen.width, Screen.height);
        mScreenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        mScreenImage.Apply();
        Debug.Log("Captured screen: " + mScreenImage);
    }

}


