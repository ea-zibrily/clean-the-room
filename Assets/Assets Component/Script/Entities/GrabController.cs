using System;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    [Header("Main Component")]
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastRadius;

    private KeyCode grabKey;
    private int objectLayerIndex;
    
    [Header("Reference")]
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        objectLayerIndex = LayerMask.GetMask("Objects");
    }

    private void Update()
    {
        PlayerOneChecker();
        GrabObject();
    }
    
    private void GrabObject()
    {
        RaycastHit2D grabHitChecker = Physics2D.Raycast(raycastPoint.position, 
            Vector2.right, raycastRadius, objectLayerIndex);
        
        if (grabHitChecker.collider != null && grabHitChecker.collider.CompareTag("Grabable"))
        {
            if (Input.GetKeyDown(grabKey))
            {
                grabHitChecker.collider.gameObject.transform.parent = grabPoint;
                grabHitChecker.collider.gameObject.transform.position = grabPoint.position;
                grabHitChecker.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(grabKey))
            {
                grabHitChecker.collider.gameObject.transform.SetParent(null);
                grabHitChecker.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }

    private void PlayerOneChecker()
    {
        if (playerController.isPlayerOne)
        {
            grabKey = KeyCode.F;
        }
        else
        {
            grabKey = KeyCode.RightAlt;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastPoint.position, Vector2.right * raycastRadius);
    }
}