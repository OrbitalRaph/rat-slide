using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Ce script s'occupe de gérer le jeu.
/// Il gère la fin de partie et la monnaie du joueur.
/// </summary>
public class GameManager : MonoBehaviour, IDataSaving
{
    public List<CurrencyType> currencyTypes;
    public SerializableDictionary<string, int> playerCurrency;
    public SerializableDictionary<string, int> obtainedCurrency;
    public static GameManager Instance;
    public GameObject gameOverPanel;
    private bool gameOver = false;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DataSavingManager.Instance.LoadGameData();
    }

    public void LoadGameData(GameData gameData)
    {   
        for (int i = 0; i < gameData.playerCurrency.Count; i++)
        {
            playerCurrency[currencyTypes[i].uniqueName] = gameData.playerCurrency[currencyTypes[i].uniqueName];
        }
    }

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

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    /// <summary>
    /// Generate random numbers of currency
    /// </summary>
    public void GenerateCurrency()
    {
        if (gameOver)
        {
            return;
        }
        foreach (CurrencyType currencyType in currencyTypes)
        {
            // odds of getting a currency number from this type (use currencytype.rarity)
            // rarity is a number between 0 and 10, 0 being the most common and 10 being the rarest
            int rarity = currencyType.rarity;
            int odds = 10 - rarity;
            int randomNumber = Random.Range(0, 10);
            if (randomNumber < odds)
            {
                // generate a random number of currency
                int amount = Random.Range(1, 10 - rarity);
                obtainedCurrency[currencyType.uniqueName] += amount;
            }
        }
    }

    /// <summary>
    /// Cette méthode permet d'ajouter de la monnaie au joueur.
    /// </summary>
    /// <param name="currency"> Les monnaies à ajouter. </param>
    public void AddCurrency(SerializableDictionary<string, int> currency)
    {
        foreach (KeyValuePair<string, int> currencyToAdd in currency)
        {
            print(currencyToAdd.Key + " " + currencyToAdd.Value);
            playerCurrency[currencyToAdd.Key] += currencyToAdd.Value;
        }
    }

    /// <summary>
    /// Cette fonction permet d'afficher le panel de fin de partie
    /// </summary>
    public void ShowGameOverMenu()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localPosition = new Vector3(0, 1000, 0);
        LeanTween.moveLocalY(gameOverPanel, 0, 0.5f).setEaseOutCubic();
        LeanTween.alphaCanvas(gameOverPanel.GetComponent<CanvasGroup>(), 1, 0.5f).setEaseOutCubic();
    }
}
