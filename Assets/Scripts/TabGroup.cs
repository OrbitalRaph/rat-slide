using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public Color tabIdle;
        public Color tabHover;
        public Color tabActive;
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

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab) 
            {
                button.image.color = tabHover;    
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
            }
            selectedTab = button;
            selectedTab.Select();

            ResetTabs();
            button.image.color = tabActive;
            
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
                button.image.color = tabIdle;
            }
        }
    }
}
