using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyTextController : MonoBehaviour
{
    public Text m_EnergyText;

    void Start()
    {
        //Text sets your text to say this message
        m_EnergyText = GetComponent<Text>();
        m_EnergyText.text = "100%";
    }

    void Update()
    {

    }

    public void UpdatePercentage(int percentage)
    {
        m_EnergyText.text = percentage.ToString() + "%";
    }
}
