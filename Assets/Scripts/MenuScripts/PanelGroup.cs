using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
public class PanelGroup : MonoBehaviour
    {
        public List<MenuPanel> panels;
        public int panelIndex;
        public bool isChanging = false;

        private void Start()
        {
            SetPageIndex(-1);
        }

        public void SetPageIndex(int index)
        {
            if (panelIndex == index) { return; }
            panelIndex = index;
            StartCoroutine(ChangeMenuPanel());
        }

        private IEnumerator ChangeMenuPanel()
        {
            isChanging = true;
            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].gameObject.activeSelf)
                {
                    panels[i].HideMenu();
                }
            }

            yield return new WaitForSeconds(0.5f);

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
