using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, int> playerCurrency;
    public string equippedApparel;
    public SerializableDictionary<string, bool> unlockedApparels;
    public string equippedBoard;
    public SerializableDictionary<string, bool> unlockedBoards;

    public GameData()
    {
        this.playerCurrency = new SerializableDictionary<string, int>();
        this.equippedApparel = null;
        this.unlockedApparels = new SerializableDictionary<string, bool>();
        this.equippedBoard = null;
        this.unlockedBoards = new SerializableDictionary<string, bool>();
    }
}
