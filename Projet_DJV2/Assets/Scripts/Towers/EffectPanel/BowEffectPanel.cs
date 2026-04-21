using ScriptableObjects;
using UnityEngine;

namespace Towers.EffectPanel
{
    public class BowEffectPanel : EffectPanel
    {
        public override void UpdadeEffects(TowerData towerData)
        {
            if (towerData is BowTowerData bowTowerData)
            {
                if (bowTowerData.HasSlow)
                {
                    Debug.Log("mise à jour des stats d'effet slow");
                    effectUI[0].gameObject.SetActive(true);
                    effectUI[0].statIndicators[0].stat = bowTowerData.slowTime;
                    effectUI[0].statIndicators[1].stat = bowTowerData.slowSpeed;
                }
                if (bowTowerData.hasPoison)
                {
                    Debug.Log("mise à jour des stats d'effet poison");
                    effectUI[1].gameObject.SetActive(true);
                    effectUI[1].statIndicators[0].stat = bowTowerData.poisonTime;
                    effectUI[1].statIndicators[1].stat = bowTowerData.poisonDamages;
                }
            }
            else
            {
                Debug.Log("Le tower Data affecté à la tour Bow nest aps un BowTowerData");
            }
        }
    }
}
