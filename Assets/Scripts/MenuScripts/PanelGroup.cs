using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
public class PanelGroup : MonoBehaviour
    {
        public List<MenuPanel> panels;
        public TabGroup tabGroup;
        public int panelIndex;
        public bool isChanging = false;

        private void Start()
        {
            SetPageIndex(0);
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
            // bool toRight = false;
            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].gameObject.activeSelf)
                {
                    // toRight = i > panelIndex;
                    panels[i].HideMenu();
                    print("Hiding panel " + i);
                }
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < panels.Count; i++)
            {
                if (i == panelIndex)
                {   
                    panels[i].gameObject.SetActive(true);
                    print("Showing panel " + i);
                    // bool fromRight = !toRight;
                    panels[i].ShowMenu();
                }
            }
            isChanging = false;
        }
    }
}
