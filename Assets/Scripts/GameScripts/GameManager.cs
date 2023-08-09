using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Ce script s'occupe de gérer le jeu.
/// Il gère la fin de partie et la monnaie du joueur.
/// </summary>
public class GameManager : MonoBehaviour, IDataSaving
{
    public List<CurrencyType> currencyTypes;
    public SerializableDictionary<string, int> playerCurrency;
    public SerializableDictionary<string, int> obtainedCurrency;
    public GameObject ratModel;
    public GameObject BoardModel;
    public SerializableDictionary<string, GameObject> outfitPrefabs;
    public SerializableDictionary<string, GameObject> boardPrefabs;
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public UnityEvent onCurrencyChanged;
    private bool gameOver = false;

    private void Start()
    {
        // S'assure qu'il n'y a qu'une seule instance de GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Récupère les données de jeu nécessaires au GameManager.
    /// </summary>
    public void LoadGameData(GameData gameData)
    {   
        // récupérer les monnaies du joueur
        for (int i = 0; i < gameData.playerCurrency.Count; i++)
        {
            playerCurrency[currencyTypes[i].uniqueName] = gameData.playerCurrency[currencyTypes[i].uniqueName];
        }

        // récupérer l'outfit équipé
        if (gameData.equippedApparels.ContainsKey("outfit"))
        {
            string outfitName = gameData.equippedApparels["outfit"];
            if (outfitPrefabs.ContainsKey(outfitName)) 
            {
                EquipOutfit(outfitName);
            }
        }

        // récupérer le board équipée
        if (gameData.equippedApparels.ContainsKey("board"))
        {
            string boardName = gameData.equippedApparels["board"];
            if (boardPrefabs.ContainsKey(boardName))
            {
                EquipBoard(boardName);
            }
        }
    }

    /// <summary>
    /// Équipe l'outfit spécifié.
    /// </summary>
    /// <param name="outfitName"> Le nom unique de l'outfit. </param>
    private void EquipOutfit(string outfitName)
    {
        GameObject outfitPrefab = outfitPrefabs[outfitName];

        // remplacer le model du rat par le model de l'outfit
        GameObject outfitModel = Instantiate(outfitPrefab, ratModel.transform.position, ratModel.transform.rotation);
        outfitModel.transform.parent = ratModel.transform.parent;
        Destroy(ratModel);
    }

    /// <summary>
    /// Équipe le board spécifié.
    /// </summary>
    /// <param name="boardName"> Le nom unique du board. </param>
    private void EquipBoard(string boardName)
    {
        GameObject boardPrefab = boardPrefabs[boardName];

        // replace the board model with the board model
        GameObject boardModel = Instantiate(boardPrefab, BoardModel.transform.position, BoardModel.transform.rotation);
        boardModel.transform.parent = BoardModel.transform.parent;
        Destroy(BoardModel);
    }

    /// <summary>
    /// Sauvegarde les données de jeu nécessaires au GameManager.
    /// </summary>
    public void SaveGameData(ref GameData gameData)
    {
        gameData.playerCurrency = playerCurrency;
    }

    /// <summary>
    /// Cette méthode permet de terminer le jeu.
    /// </summary>
    public void GameOver()
    {
        if (gameOver)
        {
            return;
        }
        gameOver = true;

        AddCurrency(obtainedCurrency);
        
        DataSavingManager.Instance.SaveGameData();

        ShowGameOverMenu();
    }

    /// <summary>
    /// Retourne au menu principal.
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    /// <summary>
    /// Génère la monnaie obtenu par un sac de monnaie.
    /// </summary>
    public void GenerateCurrency()
    {
        if (gameOver)
        {
            return;
        }
        foreach (CurrencyType currencyType in currencyTypes)
        {
            // fait un random pour savoir si la monnaie est obtenue
            // les monnaies plus rares ont moins de chance d'être obtenues
            int rarity = currencyType.rarity;
            int odds = 10 - rarity;
            int randomNumber = Random.Range(0, 10);
            if (randomNumber < odds)
            {
                // fait un random pour savoir combien de monnaie est obtenue
                // les monnaies plus rares donnent moins de monnaie
                int amount = Random.Range(1, 10 - rarity);
                obtainedCurrency[currencyType.uniqueName] += amount;
            }
        }
    }

    /// <summary>
    /// Ajoute de la monnaie au joueur.
    /// </summary>
    /// <param name="currency"> Les monnaies à ajouter. </param>
    public void AddCurrency(SerializableDictionary<string, int> currency)
    {
        foreach (KeyValuePair<string, int> currencyToAdd in currency)
        {
            playerCurrency[currencyToAdd.Key] += currencyToAdd.Value;
        }

        onCurrencyChanged.Invoke();
    }

    /// <summary>
    /// Affiche le panel de fin de partie
    /// </summary>
    public void ShowGameOverMenu()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localPosition = new Vector3(0, 1000, 0);
        LeanTween.moveLocalY(gameOverPanel, 0, 0.5f).setEaseOutCubic();
        LeanTween.alphaCanvas(gameOverPanel.GetComponent<CanvasGroup>(), 1, 0.5f).setEaseOutCubic();
    } 
}
