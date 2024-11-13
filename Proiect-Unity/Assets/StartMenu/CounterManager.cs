using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Button increaseButton;
    public Button decreaseButton;

    private int currentValue = 2;

    private void Start()
    {
        
        UpdateText();

        increaseButton.onClick.AddListener(IncreaseValue);
        decreaseButton.onClick.AddListener(DecreaseValue);
    }

    private void IncreaseValue()
    {
        if (currentValue < 4)
        {
            currentValue++;
            UpdateText();
        }
    }

    private void DecreaseValue()
    {
        if (currentValue > 2)
        {
            currentValue--;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        textMesh.text = currentValue.ToString();
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }



}

