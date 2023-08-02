using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public Sprite tabIdle;
        public Sprite tabActive;
        public TabButton selectedTab;
        public PanelGroup panelGroup;

        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }

            tabButtons.Add(button);
        }

        public void OnTabSelected(TabButton button)
        {
            if (panelGroup.isChanging) { return; }

            if (selectedTab == button) { 
                selectedTab.Deselect();
                selectedTab = null;
                ResetTabs();
                
                if (panelGroup != null)
                {
                    panelGroup.SetPageIndex(-1);
                }
                return;
            }
            
            selectedTab = button;
            selectedTab.Select();

            ResetTabs();
            button.image.sprite = tabActive;
            
            if (panelGroup != null)
            {
                panelGroup.SetPageIndex(button.transform.GetSiblingIndex());
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                if (button == selectedTab) { continue; }
                button.image.sprite = tabIdle;
            }
        }
    }
}
