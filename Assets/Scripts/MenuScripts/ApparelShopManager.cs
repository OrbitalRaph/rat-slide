using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ApparelShop
{
    public class ApparelShopManager : MonoBehaviour, IDataSaving
    {
        public ApparelItem[] ApparelItems;
        public GameObject itemManagerPrefab;
        public Transform itemListParent;
        public string apparelType;
        private SerializableDictionary<string, bool> unlockedApparels;
        private string equippedApparel;
        private ApparelItemManager equippedItemManager;

        private void PopulateShop()
        {
            foreach (ApparelItem item in ApparelItems)
            {
                bool isPurchased = IsItemPurchased(item);
                bool isEquipped = IsItemEquipped(item);

                // Instantiate the UI element for the item
                GameObject itemManagerObject = Instantiate(itemManagerPrefab, itemListParent);
                ApparelItemManager itemManager = itemManagerObject.GetComponent<ApparelItemManager>();
                itemManager.Initialize(item, isPurchased, isEquipped);

                if (isEquipped)
                {
                    // Save a reference to the equipped item manager
                    equippedItemManager = itemManager;
                }

                // Attach button click event handlers
                Button actionButton = itemManager.actionButton;

                // If the item is purchased, add the equip functionality
                actionButton.onClick.AddListener(() => OnButtonClick(item, itemManager));
            }

            // Update the displayed currency value
            CurrencyManager.Instance.UpdateCurrencyDisplay();
        }

        public void LoadGameData(GameData gameData)
        {
            unlockedApparels = new SerializableDictionary<string, bool>();

            foreach (ApparelItem item in ApparelItems)
            {
                bool isUnlocked = gameData.unlockedApparels.ContainsKey(item.uniqueName) && gameData.unlockedApparels[item.uniqueName];
                unlockedApparels.Add(item.uniqueName, isUnlocked);
            }

            // Load the equipped item from the game data
            equippedApparel = gameData.equippedApparels[apparelType];

            PopulateShop();
        }

        public void SaveGameData(ref GameData gameData)
        {
            // Save the purchased items dictionary to the game data
            foreach (KeyValuePair<string, bool> item in unlockedApparels)
            {
                if (gameData.unlockedApparels.ContainsKey(item.Key))
                {
                    gameData.unlockedApparels[item.Key] = item.Value;
                    continue;
                }
                gameData.unlockedApparels.Add(item.Key, item.Value);
            }

            // Save the equipped item to the game data
            gameData.equippedApparels[apparelType] = equippedApparel;
        }

        private void OnButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            if (IsItemPurchased(item))
            {
                // If the item is purchased, equip it
                OnEquipButtonClick(item, itemManager);
            }
            else
            {
                // If the item is not purchased, try to purchase it
                OnPurchaseButtonClick(item, itemManager);
            }
        }

        private void OnPurchaseButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            if (IsItemAffordable(item) && !IsItemPurchased(item))
            {
                // Mark item as purchased
                unlockedApparels[item.uniqueName] = true;

                // Deduct the item price from player currency
                CurrencyManager.Instance.DeductCurrency(item.itemCosts);

                // Update the UI elements
                itemManager.UpdateItemAppearance(true, false);
                itemManager.RemoveCostsList();

                // Update the displayed currency value
                CurrencyManager.Instance.UpdateCurrencyDisplay();
            }
            else
            {
                // Play a sound or shake button
            }
        }

        private void OnEquipButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            // Unequip the currently equipped item
            equippedItemManager?.UpdateItemAppearance(true, false);

            if (itemManager == equippedItemManager)
            {
                // If the item is already equipped, unequip it`
                equippedApparel = null;
                equippedItemManager = null;
                return;
            }
            // Equip the item to the player character
            equippedApparel = item.uniqueName;
            equippedItemManager = itemManager;

            // Update the UI elements
            itemManager.UpdateItemAppearance(true, true);
        }


        private bool IsItemAffordable(ApparelItem item)
        {
            return CurrencyManager.Instance.CanAfford(item.itemCosts);
        }

        private bool IsItemPurchased(ApparelItem item)
        {
            return unlockedApparels[item.uniqueName];
        }

        private bool IsItemEquipped(ApparelItem item)
        {
            return equippedApparel == item.uniqueName;
        }
    }
}
