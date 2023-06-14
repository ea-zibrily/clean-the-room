using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HoldController : MonoBehaviour
{
    #region Variable

    [Header("UI Component")]
    [SerializeField] private Image fillBarUI;
    [SerializeField] private float fillBarSpeed;
    private float maxFillBar;
    private float minFillBar;
    
    private KeyCode holdKey;
    
    [Header("Reference")]
    [SerializeField] private Animator holdPanelAnimation;
    private HoldDetector holdDetector;
    private PlayerController playerController;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        holdDetector = GetComponent<HoldDetector>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        maxFillBar = 1f;
        minFillBar = 0f;
        
        fillBarUI.fillAmount = minFillBar;
    }

    private void Update()
    {
        PlayerKeyChecker();
        HoldObject();
    }

    #endregion

    #region Tsukuyomi Methods

    private void HoldObject()
    {
        if (!holdDetector.isPlayerInside)
        {
           return;
        }
        
        if (Input.GetKey(holdKey))
        {
            StartCoroutine(IncreaseBar());
        }
        else
        {
            StartCoroutine(ResetBar());
        }
    }

    private IEnumerator IncreaseBar()
    {
        holdPanelAnimation.SetBool("isHolding", true);
        yield return new WaitForSeconds(0.20f);
        
        if (fillBarUI.fillAmount < maxFillBar)
        {
            fillBarUI.fillAmount += fillBarSpeed * Time.deltaTime;
        }
        else
        {
            fillBarUI.fillAmount = maxFillBar;
            holdPanelAnimation.SetBool("isHolding", false);
        }
    }

    private IEnumerator ResetBar()
    {
        holdPanelAnimation.SetBool("isHolding", false);
        yield return new WaitForSeconds(0.25f);
        
        fillBarUI.fillAmount = minFillBar;
    }
    
    private void PlayerKeyChecker()
    {
        if (playerController.IsPlayerOne)
        {
            holdKey = KeyCode.T;
        }
        else
        {
            holdKey = KeyCode.M;
        }
    }
    
    #endregion
}