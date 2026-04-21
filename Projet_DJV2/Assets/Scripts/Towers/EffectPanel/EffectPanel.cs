using ScriptableObjects;
using UnityEngine;

namespace Towers.EffectPanel
{
    public class EffectPanel : MonoBehaviour
    {
        [SerializeField] protected EffectUI[] effectUI;

        public virtual void UpdadeEffects(TowerData towerData)
        {
            Debug.Log("méthode update effect parent appelé par" + towerData.name );
        }
        void Start()
        {
            foreach (EffectUI effect in effectUI)
            {
                effect.gameObject.SetActive(false);
            }
        }
    }
}
