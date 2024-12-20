using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private float value; // Current value of the charge bar
    public float maxValue; // Max value for the charge bar
    public Image fillImage; // Image component that represents the fill
    public GameObject chargeBar; // The game object containing the charge bar UI
    public Gradient colorGradient; // Color gradient based on charge percentage

    // Hides the charge bar
    public void HideChargeBar()
    {
        chargeBar.SetActive(false);
    }

    // Shows the charge bar
    public void ShowChargeBar()
    {
        chargeBar.SetActive(true);
    }

    // Resets the charge value to 0
    public void ResetValue()
    {
        value = 0;
        UpdateChargeBar(); // Update UI after resetting the value
    }

    // Updates the charge value and clamps it between 0 and max value
    public void UpdateChargeValue(float amount)
    {
        value = Mathf.Clamp(amount, 0, maxValue);
        UpdateChargeBar(); // Update UI after changing the value
    }

    // Updates the UI elements (fill amount and color) based on current value
    private void UpdateChargeBar()
    {
        // Calculate the fill percentage and apply to the fill image
        float fillPercentage = value / maxValue;
        fillImage.fillAmount = fillPercentage;
        // Set the fill color based on the charge percentage
        fillImage.color = colorGradient.Evaluate(fillPercentage);
    }
}
