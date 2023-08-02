using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CurrencyManager : MonoBehaviour, IDataSaving
{
    public List<CurrencyType> currencyTypes;
    public Dictionary<string, int> playerCurrency;
    public static CurrencyManager Instance { get; private set; }
    private List<ICurrencyDisplay> currencyDisplayers = new List<ICurrencyDisplay>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found multiple instances of CurrencyManager");
        }
        Instance = this;
    }

    public void LoadGameData(GameData gameData)
    {
        this.playerCurrency = new Dictionary<string, int>();
        foreach (CurrencyType currencyType in this.currencyTypes)
        {
            this.playerCurrency.Add(currencyType.uniqueName, 0);
        }
        this.SetCurrency(gameData.playerCurrency);
    }

    public void SaveGameData(ref GameData gameData)
    {
        gameData.playerCurrency = this.playerCurrency;
    }

    public CurrencyType GetCurrencyType(string currencyName)
    {
        foreach (CurrencyType currencyType in this.currencyTypes)
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
        this.playerCurrency[currencyName] += amount;

        updateCurrencyDisplay();
    }

    public void DeductCurrency(string currencyName, int amount)
    {
        this.playerCurrency[currencyName] -= amount;

        updateCurrencyDisplay();
    }

    public void DeductCurrency(Dictionary<string, int> itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            this.playerCurrency[itemCost.Key] -= itemCost.Value;
        }

        updateCurrencyDisplay();
    }

    public void DeductCurrency(SerializableStringIntDictionary itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            this.playerCurrency[itemCost.Key] -= itemCost.Value;
        }

        updateCurrencyDisplay();
    }

    public int GetCurrency(string currencyName)
    {
        return this.playerCurrency[currencyName];
    }

    public void SetCurrency(string currencyName, int amount)
    {
        this.playerCurrency[currencyName] = amount;

        updateCurrencyDisplay();
    }

    private void SetCurrency(Dictionary<string, int> currency)
    {
        foreach (KeyValuePair<string, int> currencyAmount in currency)
        {
            this.playerCurrency[currencyAmount.Key] = currencyAmount.Value;
        }
        updateCurrencyDisplay();
    }

    public Dictionary<string, int> GetCurrency()
    {
        return this.playerCurrency;
    }

    public bool canAfford(Dictionary<string, int> itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            if (this.playerCurrency[itemCost.Key] < itemCost.Value)
            {
                return false;
            }
        }
        return true;
    }

    public bool canAfford(string currencyName, int amount)
    {
        return this.playerCurrency[currencyName] >= amount;
    }

    public bool canAfford(SerializableStringIntDictionary itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            if (this.playerCurrency[itemCost.Key] < itemCost.Value)
            {
                return false;
            }
        }
        return true;
    }

    public void updateCurrencyDisplay()
    {
        foreach (ICurrencyDisplay currencyDisplay in this.currencyDisplayers)
        {
            currencyDisplay.UpdatePlayerCurrency();
        }
    }

    public void GetCurrencyDisplayers()
    {
        IEnumerable<ICurrencyDisplay> currencyDisplayers = FindObjectsOfType<MonoBehaviour>().OfType<ICurrencyDisplay>();

        this.currencyDisplayers.Clear();

        foreach (ICurrencyDisplay currencyDisplayer in currencyDisplayers)
        {
            this.currencyDisplayers.Add(currencyDisplayer);
        }
    }

    public void DebugAddCurrency()
    {
        foreach (CurrencyType currencyType in this.currencyTypes)
        {
            this.playerCurrency[currencyType.uniqueName] += 100;
        }
        updateCurrencyDisplay();
    }
}
