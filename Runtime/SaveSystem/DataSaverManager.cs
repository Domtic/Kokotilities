
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class DataSaverManager : SingletonAsComponent<DataSaverManager>
{
    //Event that notifies when all the data from the file was loaded, subscribe to it 
    //anywhere you neeed to be notified that the data was loaded
    public static Action OnFinishedLoading;
    [Header("File Storing config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    [SerializeField] private bool SaveOnScriptableOrMono;
    //This list is gonna store all the objects that are saveable
    private List<IPersistant> dataPersistenceObject;
    //private FileDataHandler dataHandler;

    //This is the class that will contain all the information 
    //change this yo your liking, just make sure this scripts keeps the same logic and structure
    //most likely you will have an error when importing these assets because this class doesnt exist in your project
    ///[SerializeField]
    //private GameData gameData;

    //Access to this script with its singleton
    public static DataSaverManager Instance => (DataSaverManager)_Instance;

    //Call this from your game manager or the initializer that will
    //have control of when the game should start loading
    public void OnInitGameLoading()
    {
        //Loads info from the file and game scene
      //  this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObject = FindAllDataPersistenceObjects();
     //   LoadGame();
    }

    private List<IPersistant> FindAllDataPersistenceObjects()
    {
        IEnumerable<IPersistant> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IPersistant>();
        return new List<IPersistant>(dataPersistenceObjects);
    }

    /*
    public void newGame()
    {
        Debug.Log("Creating new game Data");
        PlayerSquad _SquadData = new PlayerSquad();
        this.gameData = new GameData(_SquadData);
    }
    public void LoadGame()
    {
        Debug.Log("Trying to load data from: " + Application.persistentDataPath);
        if (SaveOnScriptableOrMono)
            this.gameData = dataHandler.LoadInfoToScriptableOrMonoBehaviour();
        else
            this.gameData = dataHandler.LoadInfoToClass();

        //if not game data loaded, go to new Game

        if (this.gameData == null)
        {
            Debug.Log("No data was found.");
            newGame();
        }

        Debug.Log("Data was found");
        //push all the data to the correspondent scripts/scriptable objects
        foreach (IPersistant dataOBJ in dataPersistenceObject)
        {
            dataOBJ.LoadData(gameData);
        }

        Debug.Log("Data was loaded, notifying");
        OnFinishedLoading?.Invoke();
    }

    public void SaveGame()
    {
        foreach (IPersistant dataOBJ in dataPersistenceObject)
        {
            dataOBJ.SaveData(gameData);
        }
        Debug.Log("Saving Data");
        dataHandler.Save(gameData);
    }
    */
    private void OnApplicationQuit()
    {
        //this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObject = FindAllDataPersistenceObjects();
        //SaveGame();
    }


    #region DEBUGGING AND CHEATS
    //DEBUGGING

    [Button("Load from File", EButtonEnableMode.Editor)]
    public void TestLoad()
    {

       // this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObject = FindAllDataPersistenceObjects();
       // LoadGame();
    }


    [Button("Save currentState")]
    public void TestSave()
    {
       // this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObject = FindAllDataPersistenceObjects();
        //gameData = GameDataHolder.Instance.GetGameData();
       // SaveGame();
    }

    #endregion

}
