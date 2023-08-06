using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
    /// <summary>
    /// Cette classe permet de gérer les différents panels du menu
    /// Elle permet de changer de panel en fonction de l'index
    /// Elle enclenche les animations de transition entre les panels
    /// Elle permet de savoir si une transition est en cours
    /// Elle permet de savoir quel panel est actif
    /// </summary>
public class PanelGroup : MonoBehaviour
    {
        public List<MenuPanel> panels;
        public int panelIndex;
        public bool isChanging = false;

        private void Start()
        {
            // On cache tous les panels
            SetPageIndex(-1);
        }

        /// <summary>
        ///  Cette fonction permet de changer de panel
        ///  Elle est appelée par les boutons du menu
        /// </summary>
        /// <param name="index"> L'index du panel à afficher </param>
        public void SetPageIndex(int index)
        {
            if (panelIndex == index) { return; }
            panelIndex = index;
            StartCoroutine(ChangeMenuPanel());
        }

        /// <summary>
        /// Cette Coroutine permet de changer de panel
        /// Elle est appelée par SetPageIndex
        /// Elle permet de lancer les animations de transition entre les panels
        /// </summary>
        private IEnumerator ChangeMenuPanel()
        {
            isChanging = true;

            // On cache tous les panels
            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].gameObject.activeSelf)
                {
                    panels[i].HideMenu();
                }
            }

            // On attend que les animations de transition soient terminées
            yield return new WaitForSeconds(0.5f);

            // On affiche le panel voulu
            for (int i = 0; i < panels.Count; i++)
            {
                if (i == panelIndex)
                {   
                    panels[i].gameObject.SetActive(true);
                    panels[i].ShowMenu();
                }
            }
            isChanging = false;
        }
    }
}
