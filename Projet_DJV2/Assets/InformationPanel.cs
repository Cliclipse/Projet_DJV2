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

    public EffectPanel effectPanel;

    void Start()
    {
        if (effectPanel != null) effectPanel.gameObject.SetActive(false);
    }


    public void UpdateData(TowerData towerData , TowerData nextTowerData ,  bool isMaxLevelUp)
    {
        statsIndicatorsManager.UpdateStats(towerData.projectileDamages  , towerData.shotCooldown , towerData.range);
        
        if (isMaxLevelUp)
        {
            statsLevelUpIndicatorsManager.gameObject.SetActive(false);
            nextLevelTMP.gameObject.SetActive(false);
            costLevelUpReference.gameObject.SetActive(false);
        }
        else
        {
            statsLevelUpIndicatorsManager.UpdateStats(nextTowerData.projectileDamages , nextTowerData.shotCooldown ,  nextTowerData.range);
            costLevelUpReference.stat = towerData.cost ;
        }
        
        levelReference.text = "Level : " + towerData.level; 
        costLevelUpReference.stat = towerData.cost;

        if (towerData.hasEffect = true)
        {
            effectPanel.gameObject.SetActive(true);
            effectPanel.UpdadeEffects(towerData); //Strategy ici
        }
    }
}
