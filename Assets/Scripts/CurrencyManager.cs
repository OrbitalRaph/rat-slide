using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class CurrencyManager : MonoBehaviour, IDataSaving
{
    public List<CurrencyType> currencyTypes;
    public SerializableDictionary<string, int> playerCurrency;
    public static CurrencyManager Instance { get; private set; }
    // private readonly List<ICurrencyDisplay> currencyDisplayers = new();
    public UnityEvent onCurrencyChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found multiple instances of CurrencyManager");
        }
        Instance = this;

        playerCurrency = new SerializableDictionary<string, int>();
        foreach (CurrencyType currencyType in currencyTypes)
        {
            playerCurrency.Add(currencyType.uniqueName, 0);
        }
    }

    public void LoadGameData(GameData gameData)
    {
        SetCurrency(gameData.playerCurrency);
    }

    public void SaveGameData(ref GameData gameData)
    {
        gameData.playerCurrency = playerCurrency;
    }

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

    public void AddCurrency(string currencyName, int amount)
    {
        playerCurrency[currencyName] += amount;

        UpdateCurrencyDisplay();
    }

    public void DeductCurrency(string currencyName, int amount)
    {
        playerCurrency[currencyName] -= amount;

        UpdateCurrencyDisplay();
    }

    public void DeductCurrency(SerializableDictionary<string, int> itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            playerCurrency[itemCost.Key] -= itemCost.Value;
        }

        UpdateCurrencyDisplay();
    }

    public int GetCurrency(string currencyName)
    {
        return playerCurrency[currencyName];
    }

    public void SetCurrency(string currencyName, int amount)
    {
        playerCurrency[currencyName] = amount;

        UpdateCurrencyDisplay();
    }

    private void SetCurrency(SerializableDictionary<string, int> currency)
    {
        foreach (KeyValuePair<string, int> currencyAmount in currency)
        {
            playerCurrency[currencyAmount.Key] = currencyAmount.Value;
        }
        UpdateCurrencyDisplay();
    }

    public SerializableDictionary<string, int> GetCurrency()
    {
        return playerCurrency;
    }

    public bool CanAfford(string currencyName, int amount)
    {
        return playerCurrency[currencyName] >= amount;
    }

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
        onCurrencyChanged.Invoke();
        // foreach (ICurrencyDisplay currencyDisplay in this.currencyDisplayers)
        // {
        //     currencyDisplay.UpdatePlayerCurrency();
        // }
    }

    // public void GetCurrencyDisplayers()
    // {
    //     IEnumerable<ICurrencyDisplay> currencyDisplayers = FindObjectsOfType<MonoBehaviour>().OfType<ICurrencyDisplay>();

    //     this.currencyDisplayers.Clear();

    //     foreach (ICurrencyDisplay currencyDisplayer in currencyDisplayers)
    //     {
    //         this.currencyDisplayers.Add(currencyDisplayer);
    //     }
    // }

    public void DebugAddCurrency()
    {
        foreach (CurrencyType currencyType in currencyTypes)
        {
            playerCurrency[currencyType.uniqueName] += 100;
        }
        UpdateCurrencyDisplay();
    }
}
