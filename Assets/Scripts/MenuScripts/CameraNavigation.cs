using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe gère la navigation de la caméra dans le menu.
/// </summary>
public class CameraNavigation : MonoBehaviour
{
    public Transform defaultTarget;
    public Transform treasuryMenu;
    public Transform outfitShopMenu;
    public Transform BoardShopMenu;
    public Transform LaunchZoneMenu;

    private Transform currentTarget;

    private void Start()
    {
        currentTarget = defaultTarget;
        LeanTween.moveX(defaultTarget.gameObject, 20, 40f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
    }

    public void MoveToDefaultTarget()
    {
        currentTarget = defaultTarget;
    }

    public void MoveToTreasury()
    {
        currentTarget = treasuryMenu;
    }

    public void MoveToOutfitShop()
    {
        currentTarget = outfitShopMenu;
    }

    public void MoveToBoardShop()
    {
        currentTarget = BoardShopMenu;
    }

    public void MoveToLaunchZone()
    {
        currentTarget = LaunchZoneMenu;
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            float speed = 5f; // Adjuste la vitesse de la caméra
            transform.position = Vector3.Lerp(transform.position, currentTarget.position, Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, currentTarget.rotation, Time.deltaTime * speed);
        }
    }
}
