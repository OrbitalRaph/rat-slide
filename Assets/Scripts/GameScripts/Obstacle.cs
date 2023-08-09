using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Les obstacles terminent la partie du joueur s'ils les touchent.
/// </summary>
public class Obstacle : MonoBehaviour
{
    public float speed = 5f;

    private void Start()
    {
        LeanTween.moveLocalZ(gameObject, transform.position.z + 100, speed).setEaseInQuad().setOnComplete(() => Destroy(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
