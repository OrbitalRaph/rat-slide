using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
    /// <summary>
    /// Gère les différents panels du menu
    /// change de panel en fonction de l'index
    /// enclenche les animations de transition entre les panels
    /// Permet de savoir si une transition est en cours
    /// Permet de savoir quel panel est actif
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
        /// Change de panel
        /// appelé par les boutons du menu
        /// </summary>
        /// <param name="index"> L'index du panel à afficher </param>
        public void SetPageIndex(int index)
        {
            if (panelIndex == index) { return; }
            panelIndex = index;
            StartCoroutine(ChangeMenuPanel());
        }

        /// <summary>
        /// Coroutine qui permet de changer de panel
        /// appelé par SetPageIndex
        /// lance les animations de transition entre les panels
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
