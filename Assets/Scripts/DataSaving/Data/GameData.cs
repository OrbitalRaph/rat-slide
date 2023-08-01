using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Dictionary<string, int> playerCurrency;
    public string equippedApparel;
    public Dictionary<string, bool> unlockedApparels;
    public string equippedBoard;
    public Dictionary<string, bool> unlockedBoards;

    public GameData()
    {
        this.playerCurrency = new Dictionary<string, int>();
        this.equippedApparel = null;
        this.unlockedApparels = new Dictionary<string, bool>();
        this.equippedBoard = null;
        this.unlockedBoards = new Dictionary<string, bool>();
    }
}
