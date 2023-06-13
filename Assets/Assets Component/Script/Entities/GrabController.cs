using System;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    [Header("Main Component")]
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastRadius;

    private KeyCode grabKey;
    private GameObject grabObject;
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
            if (Input.GetKeyDown(grabKey) && grabObject == null)
            {
                playerController.PlayerGrabDirection();
                playerController.IsGrabObject = true;
                
                grabObject = grabHitChecker.collider.gameObject;
                grabObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabObject.transform.position = grabPoint.position;
                grabObject.transform.SetParent(transform);
            }
            else if(Input.GetKeyDown(grabKey))
            {
                playerController.IsGrabObject = false;
                
                grabObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabObject.transform.SetParent(null);
                grabObject = null;
            }
        }
    }

    private void PlayerOneChecker()
    {
        if (playerController.IsPlayerOne)
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