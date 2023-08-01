using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IDataSaving
{
    public List<CurrencyType> currencyTypes;
    public Dictionary<string, int> playerCurrency;
    public static CurrencyManager Instance { get; private set; }
    private List<CostItemManager> costItemManagers;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found multiple instances of CurrencyManager");
        }
        Instance = this;
    }

    private void Start()
    {
        this.costItemManagers = GetCostItemManagers();
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

    public void AddCurrency(string currencyName, int amount)
    {
        this.playerCurrency[currencyName] += amount;

        UpdateCostItemManagers();
    }

    public void DeductCurrency(string currencyName, int amount)
    {
        this.playerCurrency[currencyName] -= amount;

        UpdateCostItemManagers();
    }

    public void DeductCurrency(Dictionary<string, int> itemCosts)
    {
        foreach (KeyValuePair<string, int> itemCost in itemCosts)
        {
            this.playerCurrency[itemCost.Key] -= itemCost.Value;
        }

        UpdateCostItemManagers();
    }

    public int GetCurrency(string currencyName)
    {
        return this.playerCurrency[currencyName];
    }

    public void SetCurrency(string currencyName, int amount)
    {
        this.playerCurrency[currencyName] = amount;

        UpdateCostItemManagers();
    }

    private void SetCurrency(Dictionary<string, int> currency)
    {
        foreach (KeyValuePair<string, int> currencyAmount in currency)
        {
            this.playerCurrency[currencyAmount.Key] = currencyAmount.Value;
        }
        UpdateCostItemManagers();
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

    private void UpdateCostItemManagers()
    {
        foreach (CostItemManager costItemManager in this.costItemManagers)
        {
            costItemManager.UpdatePlayerCurrency();
        }
    }

    private List<CostItemManager> GetCostItemManagers()
    {
        IEnumerable<CostItemManager> costItemManagers = FindObjectsOfType<CostItemManager>();

        return new List<CostItemManager>(costItemManagers);
    }
}
