using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private float value; // Current value of the bar
    public float maxValue; // Max value of the charge bar
    public Image fillImage; // The image of the fill
    public GameObject chargeBar; // The charge bar game object
    public Gradient colorGradient; // Color gradient of the charge bar

    // Start is called before the first frame update
    void Start()
    {

    }

    // Function to hide the charge bar
    public void HideChargeBar()
    {
        chargeBar.SetActive(false);
    }

    // Function to show the charge bar
    public void ShowChargeBar()
    {
        chargeBar.SetActive(true);
    }

    // Function to reset the value to 0
    public void ResetValue()
    {
        value = 0;
        UpdateChargeBar();
    }

    // Function to update the charge amount
    public void UpdateChargeValue(float amount)
    {
        value = amount;
        value = Mathf.Clamp(value, 0, maxValue);
        UpdateChargeBar();
    }

    // Function to update the fill of the charge bar
    private void UpdateChargeBar()
    {
        float fillPercentage = value / maxValue;
        fillImage.fillAmount = fillPercentage;
        fillImage.color = colorGradient.Evaluate(fillPercentage);
    }
}
