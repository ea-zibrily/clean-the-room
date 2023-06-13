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
    [SerializeField] private float maxFillBar;
    [SerializeField] private float minFillBar;
    [SerializeField] private float fillBarSpeed;
    
    [Header("Reference")]
    [SerializeField] private Animator holdPanelAnimation;
    private HoldDetector holdDetector;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        holdDetector = GetComponent<HoldDetector>();
    }

    private void Update()
    {
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
        
        if (Input.GetKey(KeyCode.E))
        {
            IncreaseBar();
        }
        else
        {
            ResetBar();
        }
    }

    private void IncreaseBar()
    {
        holdPanelAnimation.SetBool("isHolding", true);
        
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

    private void ResetBar()
    {
        holdPanelAnimation.SetBool("isHolding", false);
        fillBarUI.fillAmount = minFillBar;
    }

    #endregion
}