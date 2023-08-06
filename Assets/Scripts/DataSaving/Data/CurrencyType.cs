using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe représente un type de monnaie.
/// </summary>
[CreateAssetMenu(fileName = "New Currency Type", menuName = "Data/Currency Type")]
public class CurrencyType : ScriptableObject
{
    public string uniqueName;
    public string typeName;
    public Sprite icon;
    public int rarity;
}
