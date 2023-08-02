using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Apparel Item", menuName = "Data/Apparel Item")]
public class ApparelItem : ScriptableObject
{
    public string uniqueName;
    public string itemName;
    public SerializableStringIntDictionary itemCosts;
    public GameObject apparelModel;
}
