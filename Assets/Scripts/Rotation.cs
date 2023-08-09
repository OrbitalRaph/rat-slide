using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe permet de faire tourner un objet autour de son axe Y.
/// </summary>
public class Rotation : MonoBehaviour
{
    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, 10f).setLoopClamp();
    }
}
