using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationPanel : MonoBehaviour
{
    public StatsIndicatorsManager statsIndicatorsManager;
    public TextMeshProUGUI levelReference;
    
    public StatIndicator costLevelUpReference;
    public StatsIndicatorsManager statsLevelUpIndicatorsManager;
    public TextMeshProUGUI nextLevelTMP;

    public RectTransform effectPanel;

    void Start()
    {
        if (effectPanel != null) effectPanel.gameObject.SetActive(false);
    }

}
