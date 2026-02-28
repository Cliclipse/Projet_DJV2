using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationController : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    
    [SerializeField] private TextMeshProUGUI goldTMP;
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI waveTMP;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldTMP.text = levelController.gold.ToString();
        healthTMP.text = levelController.health.ToString();
        scoreTMP.text = levelController.score.ToString();
        //waveTMP.text = levelController.waveController.currentWave.ToString();
    }
}
