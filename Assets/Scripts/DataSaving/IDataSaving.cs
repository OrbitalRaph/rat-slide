using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSaving
{
    void LoadGameData(GameData gameData);
    void SaveGameData(ref GameData gameData);
}
