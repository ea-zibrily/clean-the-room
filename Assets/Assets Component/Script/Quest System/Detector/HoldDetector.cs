using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDetector : MonoBehaviour
{
    [field: SerializeField] public bool isPlayerInside {get; private set;}
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player inside is {isPlayerInside}");
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player inside is {isPlayerInside}");
            isPlayerInside = false;
        }
    }
}
