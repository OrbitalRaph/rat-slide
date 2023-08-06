using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe contient les données du jeu.
/// </summary>
[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, int> playerCurrency;
    public SerializableDictionary<string, bool> unlockedApparels;
    public SerializableDictionary<string, string> equippedApparels;

    public GameData()
    {
        playerCurrency = new SerializableDictionary<string, int>();
        unlockedApparels = new SerializableDictionary<string, bool>();
        equippedApparels = new SerializableDictionary<string, string>
        {
            { "outfit", "" },
            { "board", "" },
        };
    }
}
