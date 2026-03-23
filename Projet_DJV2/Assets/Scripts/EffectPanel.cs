using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPanel : MonoBehaviour
{
    [SerializeField] protected EffectUI[] effectUI;

    public virtual void UpdadeEffects(TowerData towerData)
    {
        Debug.Log("Effect Panel Update Parent");
    }

    void Start()
    {
        foreach (EffectUI effect in effectUI)
        {
            effect.gameObject.SetActive(false);
        }
    }
}
