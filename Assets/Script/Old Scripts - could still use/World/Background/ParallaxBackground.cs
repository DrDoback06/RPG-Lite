using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier; // Controls the speed of parallax effect
    [SerializeField] private Transform cameraTransform; // Reference to the main camera's transform
    private Vector3 lastCameraPosition; // Stores the last position of the camera

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition; // Calculate the camera's movement
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, 0);
        lastCameraPosition = cameraTransform.position;
    }
}
