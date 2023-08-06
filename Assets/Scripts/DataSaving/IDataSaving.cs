using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette interface permet de sauvegarder et charger les données du jeu.
/// </summary>
public interface IDataSaving
{
    void LoadGameData(GameData gameData);
    void SaveGameData(ref GameData gameData);
}
