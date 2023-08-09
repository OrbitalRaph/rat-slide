using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// Gère la sauvegarde des données du jeu.
/// </summary>
public class DataSavingManager : MonoBehaviour
{
    [Header("Data storage configuration")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataSaving> dataSavingObjects;
    private FileDataHandler fileDataHandler;
    public static DataSavingManager Instance { get; private set; }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataSavingObjects = GetDataSavingObjects();
        LoadGameData();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Cette méthode est appelée lorsque la scène est chargée.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataSavingObjects = GetDataSavingObjects();
        LoadGameData();
    }

    /// <summary>
    /// Récupère les données du jeu et de l'envoie aux objets qui en ont besoin.
    /// </summary>
    public void LoadGameData()
    {
        gameData = fileDataHandler.Load();

        // créer un objet GameData si aucun n'est trouvé
        if (gameData == null)
        {
            Debug.Log("No saved data found, initializing new GameData object");
            gameData = new GameData();
        }

        // Appelle la méthode LoadGameData sur tous les objets de la scène qui implémentent l'interface IDataSaving
        foreach (IDataSaving dataSavingObject in dataSavingObjects)
        {
            dataSavingObject.LoadGameData(gameData);
        }
    }

    /// <summary>
    /// Sauvegarde les données du jeu et demande aux objets qui en ont besoin de sauvegarder leurs données.
    /// </summary>
    public void SaveGameData()
    {
        // Appelle la méthode SaveGameData sur tous les objets de la scène qui implémentent l'interface IDataSaving
        foreach (IDataSaving dataSavingObject in dataSavingObjects)
        {
            dataSavingObject.SaveGameData(ref gameData);
        }

        // Sauvegarde les données du jeu
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        // Sauvegarde les données du jeu lorsque l'application est fermée
        SaveGameData();
    }

    /// <summary>
    /// Trouve tous les objets de la scène qui implémentent l'interface IDataSaving.
    /// </summary>
    private List<IDataSaving> GetDataSavingObjects()
    {
        // Trouve tous les objets de la scène qui implémentent l'interface IDataSaving
        IEnumerable<IDataSaving> dataSavingObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaving>();

        return new List<IDataSaving>(dataSavingObjects);
    }
}
