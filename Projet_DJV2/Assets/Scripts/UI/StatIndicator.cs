using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatIndicator : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    public float stat;
    
    [Header("References")]
    [SerializeField] private TextMeshProUGUI statTextReference;
    [SerializeField] private Image imageReference ;



    void Start()
    {
        statTextReference.text = stat.ToString();
        imageReference.sprite=sprite;
    }

    public void UpdateTotale(float newStat , Sprite newSprite)
    {
        stat = newStat;
        sprite = newSprite;
        
        statTextReference.text = stat.ToString();
        imageReference.sprite=sprite;
    }
    
    public void UpdateStat(float newStat)
    {
        stat = newStat;
        statTextReference.text = stat.ToString();
        
    }
}
