using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ApparelShop
{
    /// <summary>
    /// Cette classe gère le magasin d'habillement.
    /// </summary>
    public class ApparelShopManager : MonoBehaviour, IDataSaving
    {
        public ApparelItem[] ApparelItems;
        public GameObject itemManagerPrefab;
        public Transform itemListParent;
        public string apparelType;
        private SerializableDictionary<string, bool> unlockedApparels;
        private string equippedApparel;
        private ApparelItemManager equippedItemManager;

        /// <summary>
        /// Cette méthode permet d'initialiser le magasin d'habillement.
        /// Les items sont instanciés et les boutons sont configurés.
        /// </summary>
        private void PopulateShop()
        {
            foreach (ApparelItem item in ApparelItems)
            {
                bool isPurchased = IsItemPurchased(item);
                bool isEquipped = IsItemEquipped(item);

                // Instencie un item manager pour chaque item
                GameObject itemManagerObject = Instantiate(itemManagerPrefab, itemListParent);
                ApparelItemManager itemManager = itemManagerObject.GetComponent<ApparelItemManager>();
                itemManager.Initialize(item, isPurchased, isEquipped);

                if (isEquipped)
                {
                    // Garde une référence vers l'item manager de l'item équipé
                    equippedItemManager = itemManager;
                }

                // Configure le bouton d'action
                Button actionButton = itemManager.actionButton;
                actionButton.onClick.AddListener(() => OnButtonClick(item, itemManager));
            }

            // Met à jour l'affichage de la monnaie
            CurrencyManager.Instance.UpdateCurrencyDisplay();
        }

        /// <summary>
        /// Cette méthode permet de charger les données du jeu.
        /// Elle est appelée par le GameDataManager au démarrage du jeu.
        /// Les items déverrouillés et équipés sont chargés.
        /// </summary>
        /// <param name="gameData"> Les données du jeu. </param>
        public void LoadGameData(GameData gameData)
        {
            unlockedApparels = new SerializableDictionary<string, bool>();

            // Pour chaque item, vérifie si l'item est déverrouillé et l'ajoute au dictionnaire
            foreach (ApparelItem item in ApparelItems)
            {
                bool isUnlocked = gameData.unlockedApparels.ContainsKey(item.uniqueName) && gameData.unlockedApparels[item.uniqueName];
                unlockedApparels.Add(item.uniqueName, isUnlocked);
            }

            // Charge l'item équipé
            equippedApparel = gameData.equippedApparels[apparelType];

            // Initialise le magasin
            // wait for currency manager to be not null
            StartCoroutine(WaitForCurrencyManager());
        }

        private IEnumerator WaitForCurrencyManager()
        {
            yield return null;
            PopulateShop();
        }

        /// <summary>
        /// Cette méthode permet de sauvegarder les données du jeu.
        /// Elle est appelée par le GameDataManager à la fermeture du jeu.
        /// Les items déverrouillés et équipés sont sauvegardés.
        /// </summary>
        public void SaveGameData(ref GameData gameData)
        {
            // Ajoute le status des items au dictionnaire
            foreach (KeyValuePair<string, bool> item in unlockedApparels)
            {
                if (gameData.unlockedApparels.ContainsKey(item.Key))
                {
                    gameData.unlockedApparels[item.Key] = item.Value;
                    continue;
                }
                gameData.unlockedApparels.Add(item.Key, item.Value);
            }
 
            // Ajoute l'item équipé
            gameData.equippedApparels[apparelType] = equippedApparel;
        }

        /// <summary>
        /// Cette méthode est appelée lorsque le joueur clique sur un bouton d'action.
        /// Si l'item est déverrouillé, il est équipé, sinon il est acheté.
        /// </summary>
        private void OnButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            if (IsItemPurchased(item))
            {
                // Si l'item est déverrouillé, essaye de l'équiper
                OnEquipButtonClick(item, itemManager);
            }
            else
            {
                // Si l'item n'est pas déverrouillé, essaye de l'acheter
                OnPurchaseButtonClick(item, itemManager);
            }
        }

        /// <summary>
        /// Cette méthode est appelée lorsque le joueur clique sur le bouton d'achat.
        /// Si le joueur a assez d'argent, l'item est déverrouillé et acheté.
        /// </summary>
        /// <param name="item"> L'item à acheter. </param>
        /// <param name="itemManager"> L'item manager de l'item à acheter. </param>
        private void OnPurchaseButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            if (IsItemAffordable(item) && !IsItemPurchased(item))
            {
                // Déverrouille l'item
                unlockedApparels[item.uniqueName] = true;

                // Déduit le coût de l'item
                CurrencyManager.Instance.DeductCurrency(item.itemCosts);

                // Met à jour l'affichage de l'item
                itemManager.UpdateItemAppearance(true, false);
                itemManager.RemoveCostsList();

                // Met à jour l'affichage de la monnaie
                CurrencyManager.Instance.UpdateCurrencyDisplay();
            }
        }

        /// <summary>
        /// Cette méthode est appelée lorsque le joueur clique sur le bouton d'équipement.
        /// Si l'item est déjà équipé, il est déséquipé, sinon il est équipé.
        /// </summary>
        private void OnEquipButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            // Déséquipe l'item actuellement équipé
            equippedItemManager?.UpdateItemAppearance(true, false);

            if (itemManager == equippedItemManager)
            {
                // Si l'item est déjà équipé, déséquipe l'item
                equippedApparel = null;
                equippedItemManager = null;
                return;
            }
            // Sinon, équipe l'item
            equippedApparel = item.uniqueName;
            equippedItemManager = itemManager;

            //  Met à jour l'affichage de l'item
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
