using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Les sacs sont des objets qui peuvent être ramassés par le joueur.
/// Ils augmentent les monnaies du joueur de façon aléatoire.
/// </summary>
public class TrashBag : MonoBehaviour
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
            GameManager.Instance.GenerateCurrency();
            Destroy(gameObject);
        }
    }
}
