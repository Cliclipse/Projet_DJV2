using System.Collections;
using System.Collections.Generic;
using Level;
using TMPro;
using UnityEngine;

public class InformationController : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    
    [SerializeField] private TextMeshProUGUI goldTMP;
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI waveTMP;

    // Update is called once per frame
    void Update()
    {
        goldTMP.text = levelController.gold.ToString();
        healthTMP.text = levelController.health.ToString();
        Debug.Log(levelController.WaveNumber);
        //Debug.Log(levelController.WaveCount);
        //waveTMP.text = $"Wave {levelController.WaveNumber} / {levelController.WaveCount}";
    }
}
