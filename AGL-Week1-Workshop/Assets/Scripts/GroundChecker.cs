using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private int _groundedCount;

    public bool IsGrounded => _groundedCount > 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        _groundedCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _groundedCount--;
    }
}
   