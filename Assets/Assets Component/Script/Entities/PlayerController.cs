using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region RequireComponent

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GrabController))]

#endregion
public class PlayerController : ObserverSubjects
{
    #region Variable
    
    [Header("Main Component")]
    [SerializeField] private PlayerData playerDataSO;
    [SerializeField] private Vector2 playerDirection;
    [field: SerializeField] public bool IsPlayerOne {get; private set;}
    public bool IsGrabObject {get; set;}
    
    [Header("Reference")]
    private Rigidbody2D myRb;
    private Animator myAnim;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.name = playerDataSO.playerName;
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void Update()
    {
        PlayerAnimation();
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void PlayerMove()
    {
        float moveX, moveY;
        if (IsPlayerOne)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal2");
            moveY = Input.GetAxisRaw("Vertical2");
        }

        playerDirection = new Vector2(moveX, moveY);
        playerDirection.Normalize();
        
        myRb.velocity = playerDirection * playerDataSO.PlayerSpeed;
    }

    public void PlayerGrabDirection() => playerDirection = Vector2.right;
    
    private void PlayerAnimation()
    {
        if (playerDirection != Vector2.zero)
        {
            if (IsGrabObject)
            {
                myAnim.SetFloat("Horizontal", 1);
                myAnim.SetFloat("Vertical", 0);
            }
            else
            {
                myAnim.SetFloat("Horizontal", playerDirection.x);
                myAnim.SetFloat("Vertical", playerDirection.y);
            }
           
            myAnim.SetBool("isMoving", true);
        }
        else
        {
            myAnim.SetBool("isMoving", false);
        }
    }

    #endregion
}
