using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEffectPanel : EffectPanel
{
    public override void UpdadeEffects(TowerData towerData)
    {
        if (towerData is BowTowerData bowTowerData)
        {
            if (bowTowerData.HasSlow)
            {
                effectUI[0].gameObject.SetActive(true);
                Debug.Log("mise à jour des stats d'effet slow");
                //effectUI[0].statIndicators = new []{bowTowerData.slowTime , bowTowerData.slowSpeed}
            }
            if (bowTowerData.hasPoison)
            {
                Debug.Log("mise à jour des stats d'effet poison");
                effectUI[1].gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Le tower Data affecté à la tour Bow nest aps un BowTowerData");
        }
    }
}
