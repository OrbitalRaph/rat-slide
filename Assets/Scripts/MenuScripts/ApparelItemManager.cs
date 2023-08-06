using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ApparelShop
{
    /// <summary>
    /// Cette classe gère un item du magasin d'habillement.
    /// </summary>
    public class ApparelItemManager : MonoBehaviour
    {
        public Button actionButton;
        [SerializeField] private TextMeshProUGUI actionButtonText;
        [SerializeField] private Image backdropImage;
        [SerializeField] private RawImage itemImage;
        public GameObject costPrefab;
        public Sprite greyButtonSprite;
        public Sprite yellowButtonSprite;
        public Sprite greenButtonSprite;
        public Sprite blueButtonSprite;
        public Sprite greyBackdropSprite;
        public Sprite blueBackdropSprite;
        public Sprite greenBackdropSprite;
        public Transform costsListParent;
        private ApparelItem item;
        private readonly List<GameObject> costsObjects = new();

        /// <summary>
        /// Cette méthode est appelée lorsque le magasin d'habillement instancie un item manager.
        /// Elle permet d'initialiser l'item manager.
        /// </summary>
        public void Initialize(ApparelItem item, bool isPurchased, bool isEquipped)
        {
            this.item = item;
            itemImage.texture = item.itemTexture;

            if (!isPurchased)
                PopulateCostsList();

            UpdateItemAppearance(isPurchased, isEquipped);
        }

        /// <summary>
        /// Cette méthode est appelée à l'initialisation de l'item manager.
        /// Elle permet d'instancier les éléments UI pour les coûts de l'item.
        /// </summary>
        private void PopulateCostsList()
        {
            foreach (KeyValuePair<string, int> cost in item.itemCosts)
            {
                // Instancie un cost manager pour chaque coût
                GameObject costObject = Instantiate(costPrefab, costsListParent);
                CostItemManager costManager = costObject.GetComponent<CostItemManager>();
                CurrencyType currencyType = CurrencyManager.Instance.GetCurrencyType(cost.Key);
                costManager.SetCostItem(currencyType, cost.Value);
                costsObjects.Add(costObject);
            }
        }

        /// <summary>
        /// Cette méthode met à jour l'apparence de l'item.
        /// </summary>
        public void UpdateItemAppearance(bool isPurchased, bool isEquipped)
        {
            // Remet le texte du bouton à sa position initiale
            actionButtonText.rectTransform.offsetMin = new Vector2(0, 5);
            actionButtonText.rectTransform.offsetMax = new Vector2(0, 0);

            if (isPurchased)
            {
                // Met l'arrère plan en blanc si acheté
                backdropImage.color = Color.white;
                if (isEquipped)
                {
                    // descends le texte du bouton
                    actionButtonText.rectTransform.offsetMin = new Vector2(0, 0);
                    actionButtonText.rectTransform.offsetMax = new Vector2(0, -8);

                    // Met le bouton en vert si équipé 
                    actionButton.image.sprite = greenButtonSprite;
                    backdropImage.sprite = greenBackdropSprite;
                    actionButtonText.text = "Enlever";
                }
                else
                {
                    // Met le bouton en bleu si acheté mais pas équipé
                    actionButton.image.sprite = blueButtonSprite;
                    backdropImage.sprite = blueBackdropSprite;
                    actionButtonText.text = "Enfiler";
                }
            }
            else
            {
                // Met l'arrère plan en gris si non acheté
                backdropImage.sprite = greyBackdropSprite;
                backdropImage.color = new Color(0.3294118f, 0.3607843f, 0.4078431f);
                if (CurrencyManager.Instance.CanAfford(item.itemCosts))
                {
                    // Met le bouton en jaune si non acheté et achetable
                    actionButton.image.sprite = yellowButtonSprite;
                    actionButtonText.text = "Acheter";
                }
                else
                {
                    // Met le bouton en gris si non acheté et non achetable
                    actionButton.image.sprite = greyButtonSprite;
                    backdropImage.color = Color.gray;
                    actionButtonText.text = "Acheter";
                }
            }
        }

        public void RemoveCostsList()
        {
            foreach (GameObject costObject in costsObjects)
            {
                Destroy(costObject);
            }
        }
    }
}