using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataSavingManager : MonoBehaviour
{
    [Header("Data storage configuration")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataSaving> dataSavingObjects;
    private FileDataHandler fileDataHandler;
    public static DataSavingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found multiple instances of DataSavingManager");
        }
        Instance = this;
    }

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataSavingObjects = GetDataSavingObjects();
        LoadGameData();
    }

    // Method to load saved data
    private void LoadGameData()
    {
        gameData = fileDataHandler.Load();

        // Load saved data from PlayerPrefs
        if (gameData == null)
        {
            Debug.Log("No saved data found, initializing new GameData object");
            gameData = new GameData();
        }

        // Call the LoadGameData method on all objects in the scene that implement the IDataSaving interface
        foreach (IDataSaving dataSavingObject in dataSavingObjects)
        {
            dataSavingObject.LoadGameData(gameData);
        }
    }

    // Method to save data
    private void SaveGameData()
    {
        // Call the SaveGameData method on all objects in the scene that implement the IDataSaving interface
        foreach (IDataSaving dataSavingObject in dataSavingObjects)
        {
            dataSavingObject.SaveGameData(ref gameData);
        }

        // Save the GameData object to a file
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        // Save data when the application is closed
        SaveGameData();
    }

    private List<IDataSaving> GetDataSavingObjects()
    {
        // Find all objects in the scene that implement the IDataSaving interface
        IEnumerable<IDataSaving> dataSavingObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaving>();

        return new List<IDataSaving>(dataSavingObjects);
    }
}
