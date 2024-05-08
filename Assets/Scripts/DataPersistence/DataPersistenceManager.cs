using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Singleton class that manages the data persistence
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObjects;

    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one DataPersistenceManager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, this.fileName, this.useEncryption);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load the game data from the file using the data handler
        this.gameData = this.dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data to load. Creating a new game");
            NewGame();
        }

        // Loading data to all data persistance objects in the game
        foreach (IDataPersistance dataPersistanceObject in this.dataPersistanceObjects)
        {
            dataPersistanceObject.LoadData(this.gameData);
        }
    }

    public void SaveGame()
    {
        // Passing the data to other scripts to save their data
        foreach (IDataPersistance dataPersistanceObject in this.dataPersistanceObjects)
        {
            dataPersistanceObject.SaveData(ref this.gameData);
        }

        //Save the game data to the file using the data handler
        dataHandler.Save(this.gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
