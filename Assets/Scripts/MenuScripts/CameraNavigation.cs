using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNavigation : MonoBehaviour
{
    public Transform trésorerieMenu;
    public Transform magasinApparenceMenu;
    public Transform magasinPlancheSurfMenu;
    public Transform zoneLancementMenu;

    private Transform currentTarget;

    public void MoveToTrésorerie()
    {
        currentTarget = trésorerieMenu;
    }

    public void MoveToMagasinApparence()
    {
        currentTarget = magasinApparenceMenu;
    }

    public void MoveToMagasinPlancheSurf()
    {
        currentTarget = magasinPlancheSurfMenu;
    }

    public void MoveToZoneLancement()
    {
        currentTarget = zoneLancementMenu;
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            float speed = 5f; // Adjuste la vitesse de la caméra
            transform.position = Vector3.Lerp(transform.position, currentTarget.position, Time.deltaTime * speed);
        }
    }
}
