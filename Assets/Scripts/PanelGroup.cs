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

        private void Start()
        {
            SetPageIndex(0);
        }

        public void SetPageIndex(int index)
        {
            panelIndex = index;
            StartCoroutine(ChangeMenuPanel());
        }

        private IEnumerator ChangeMenuPanel()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].gameObject.activeSelf)
                {
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
                    panels[i].ShowMenu();
                }
            }
            
        }
    }
}
