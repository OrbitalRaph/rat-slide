using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, 10f).setLoopClamp();
    }
}
