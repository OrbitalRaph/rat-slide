using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts
{
    /// <summary>
    /// Cette classe permet de gérer les boutons onglets du menu
    /// Elle permet de gérer les évènements lorsqu'un onglet est sélectionné ou désélectionné
    /// </summary>
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public Sprite tabIdle;
        public Sprite tabActive;
        public TabButton selectedTab;
        public PanelGroup panelGroup;

        /// <summary>
        /// Cette fonction permet d'ajouter un bouton onglet à la liste des boutons onglets
        /// </summary>
        /// <param name="button"> Le bouton onglet à ajouter </param>
        public void Subscribe(TabButton button)
        {
            tabButtons ??= new List<TabButton>();

            tabButtons.Add(button);
        }

        /// <summary>
        /// Cette fonction permet de sélectionner un onglet
        /// Elle est appelée par le bouton onglet
        /// </summary>
        public void OnTabSelected(TabButton button)
        {
            if (panelGroup.isChanging) { return; }

            if (selectedTab == button) { 
                selectedTab.Deselect();
                selectedTab = null;
                ResetTabs();
                
                panelGroup?.SetPageIndex(-1);
                return;
            }
            
            selectedTab = button;
            selectedTab.Select();

            ResetTabs();
            button.image.sprite = tabActive;
            button.iconPosition.transform.localPosition = new Vector3(0, -5, 0);
            
            panelGroup?.SetPageIndex(button.transform.GetSiblingIndex());
        }

        /// <summary>
        /// Cette fonction permet de désélectionner tous les onglets
        /// Elle est appelée par OnTabSelected
        /// </summary>
        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                if (button == selectedTab) { continue; }
                button.image.sprite = tabIdle;
                button.iconPosition.transform.localPosition = new Vector3(0, 8, 0);
            }
        }

        /// <summary>
        /// Cette fonction permet de cacher le groupe de boutons onglets
        /// </summary>
        public void HideTabGroup()
        {
            selectedTab.Deselect();
            selectedTab = null;
            ResetTabs();
                
            panelGroup?.SetPageIndex(-1);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Cette fonction permet d'afficher le groupe de boutons onglets
        /// </summary>
        public void ShowTabGroup()
        {
            gameObject.SetActive(true);
        }
    }
}
