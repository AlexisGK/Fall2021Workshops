using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded{
        get {
            int playerLayer = 9;
            int layerMask = ~(1 << playerLayer); //Exclude layer 9
            Ray ray = new Ray();
            ray.origin = transform.position;
            ray.direction = Vector2.down;
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
            return hit.collider && hit.collider.sharedMaterial && hit.collider.sharedMaterial.name == "groundMaterial" && hit.distance <= 0.05;
        }
    }
}
   
