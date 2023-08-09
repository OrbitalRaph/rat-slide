using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script est attaché au joueur et permet de le déplacer de gauche à droite et de le faire sauter.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float laneDistance = 2.0f;
    public float jumpHeight = 2.0f;
    public float minJumpCooldownHeight = 0.2f;
    public float laneChangeRotationAngle = 15.0f;

    private int currentLane = 1;
    private bool isJumping = false;

    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private bool isRotating = false;

    private void Start()
    {
        originalRotation = transform.rotation;
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (!isRotating)
        {
            if (Input.GetKey(KeyCode.D) && currentLane > 0)
            {
                MoveLane(-1);
            }
            else if (Input.GetKey(KeyCode.A) && currentLane < 2)
            {
                MoveLane(1);
            }
        }

        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
    }

    /// <summary>
    /// Déplace le joueur d'une voie à une autre.
    /// </summary>
    /// <param name="direction"> -1 pour aller à gauche, 1 pour aller à droite.</param>
    private void MoveLane(int direction)
    {
        Vector3 targetPosition = transform.position + Vector3.right * direction * laneDistance;
        Quaternion targetRotation = Quaternion.Euler(0, -direction * laneChangeRotationAngle, 0) * originalRotation;

        isRotating = true;
        LeanTween.rotateY(gameObject, targetRotation.eulerAngles.y, 0.2f)
                 .setEase(LeanTweenType.easeOutQuad)
                 .setOnComplete(() =>
                 {
                     LeanTween.moveLocalX(gameObject, targetPosition.x, 0.2f)
                              .setEase(LeanTweenType.easeInQuad)
                              .setOnComplete(() =>
                              {
                                  LeanTween.rotateY(gameObject, originalRotation.eulerAngles.y, 0.2f)
                                           .setEase(LeanTweenType.easeInQuad)
                                           .setOnComplete(() => isRotating = false);
                              });
                     currentLane += direction;
                 });
    }

    /// <summary>
    /// Fait sauter le joueur.
    /// </summary>
    private void Jump()
    {
        if (transform.position.y == originalPosition.y)
        {
            isJumping = true;
            float jumpDuration = Mathf.Sqrt(2 * jumpHeight / Physics.gravity.magnitude);

            LeanTween.moveLocalY(gameObject, jumpHeight, jumpDuration / 2)
                     .setEase(LeanTweenType.easeOutCubic)
                     .setOnComplete(() =>
                     {
                         LeanTween.moveLocalY(gameObject, originalPosition.y, jumpDuration / 2)
                                  .setEase(LeanTweenType.easeInCubic)
                                  .setOnComplete(() => isJumping = false);
                     });
        }
    }
}