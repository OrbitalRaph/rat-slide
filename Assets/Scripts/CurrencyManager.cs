using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Cette classe gère les monnaies du joueur.
/// </summary>
public class CurrencyManager : MonoBehaviour, IDataSaving
{
    public List<CurrencyType> currencyTypes;
    public SerializableDictionary<string, int> playerCurrency;
    public static CurrencyManager Instance { get; private set; }
    public UnityEvent onCurrencyChanged;

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

        
        // DataSavingManager.Instance.LoadGameData();
        UpdateCurrencyDisplay();
    }

    public void LoadGameData(GameData gameData)
    {
        SetCurrency(gameData.playerCurrency);
    }

    public void SaveGameData(ref GameData gameData)
    {
        gameData.playerCurrency = playerCurrency;
    }

    /// <summary>
    /// Cette méthode permet d'obtenir un CurrencyType à partir de son nom unique.
    /// </summary>
    /// <param name="currencyName"> Le nom unique de la monnaie. </param>
    /// <returns></returns>
    public CurrencyType GetCurrencyType(string currencyName)
    {
        foreach (CurrencyType currencyType in currencyTypes)
        {
            if (currencyType.uniqueName == currencyName)
            {
                return currencyType;
            }
        }
        return null;
    }

    /// <summary>
    /// Cette méthode permet d'ajouter de la monnaie au joueur.
    /// </summary>
    /// <param name="currencyName"> Le nom unique de la monnaie. </param>
    /// <param name="amount"> Le montant à ajouter. </param>
    public void AddCurrency(string currencyName, int amount)
    {
        playerCurrency[currencyName] += amount;

        UpdateCurrencyDisplay();
    }

    /// <summary>
    /// Cette méthode permet d'ajouter de la monnaie au joueur.
    /// </summary>
    /// <param name="currency"> Les monnaies à ajouter. </param>
    public void AddCurrency(SerializableDictionary<string, int> currency)
    {
        foreach (KeyValuePair<string, int> currencyToAdd in currency)
        {
            playerCurrency[currencyToAdd.Key] += currencyToAdd.Value;
        }

        UpdateCurrencyDisplay();
    }

    /// <summary>
    /// Cette méthode permet de retirer de la monnaie au joueur.
    /// </summary>   
    /// <param name="currencyName"> Le nom unique de la monnaie. </param>
    /// <param name="amount"> Le montant à ajouter. </param>
    public void DeductCurrency(string currencyName, int amount)
    {
        playerCurrency[currencyName] -= amount;

        UpdateCurrencyDisplay();
    }

    /// <summary>
    /// Cette méthode permet de retirer de la monnaie au joueur.
    /// </summary>
    /// <param name="itemCosts"> Les coûts de l'item. </param>
    public void DeductCurrency(SerializableDictionary<string, int> itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            playerCurrency[itemCost.Key] -= itemCost.Value;
        }

        UpdateCurrencyDisplay();
    }

    /// <summary>
    /// Cette méthode permet d'obtenir la quantité d'une monnaie.
    /// </summary>
    /// <param name="currencyName"> Le nom unique de la monnaie. </param>
    /// <returns></returns>
    public int GetCurrency(string currencyName)
    {
        return playerCurrency[currencyName];
    }

    /// <summary>
    /// Cette méthode permet de définir la quantité d'une monnaie.
    /// </summary>
    /// <param name="currencyName"> Le nom unique de la monnaie. </param>
    /// <param name="amount"> Le montant à définir. </param>
    public void SetCurrency(string currencyName, int amount)
    {
        playerCurrency[currencyName] = amount;

        UpdateCurrencyDisplay();
    }

    /// <summary>
    /// Cette méthode permet de définir la quantité de toutes les monnaies.
    /// </summary>
    /// <param name="currency"> Les monnaies à définir. </param>
    private void SetCurrency(SerializableDictionary<string, int> currency)
    {
        foreach (KeyValuePair<string, int> currencyAmount in currency)
        {
            playerCurrency[currencyAmount.Key] = currencyAmount.Value;
        }
        UpdateCurrencyDisplay();
    }

    /// <summary>
    /// Cette méthode permet d'obtenir toutes les monnaies.
    /// </summary>
    public SerializableDictionary<string, int> GetCurrency()
    {
        return playerCurrency;
    }

    /// <summary>
    /// Cette méthode permet de vérifier si le joueur peut se permettre d'acheter un item.
    /// </summary>
    /// <param name="currencyName"> Le nom unique de la monnaie. </param>
    /// <param name="amount"> Le montant à vérifier. </param>
    public bool CanAfford(string currencyName, int amount)
    {
        return playerCurrency[currencyName] >= amount;
    }

    /// <summary>
    /// Cette méthode permet de vérifier si le joueur peut se permettre d'acheter un item.
    /// </summary>
    /// <param name="itemCosts"> Les coûts de l'item. </param>
    public bool CanAfford(SerializableDictionary<string, int> itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            if (playerCurrency[itemCost.Key] < itemCost.Value)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateCurrencyDisplay()
    {
        onCurrencyChanged?.Invoke();
    }

    public void DebugAddCurrency()
    {
        foreach (CurrencyType currencyType in currencyTypes)
        {
            playerCurrency[currencyType.uniqueName] += 100;
        }
        UpdateCurrencyDisplay();
    }

    public void StartGame()
    {
        DataSavingManager.Instance.SaveGameData();

        SceneManager.LoadScene("GameScene");
    }
}
