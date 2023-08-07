using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script permet de ne pas détruire un objet lors d'un changement de scène
/// </summary>
public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
