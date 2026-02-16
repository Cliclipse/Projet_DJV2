using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //implémenter un singleton ensuite puis le fait que ce soit une machine à état entre l'animation de départ,
    //le menu pause, le jeu avec le shop ouvert ou le shop non ouvert
    [SerializeField] RectTransform shopPanel;
    [SerializeField] RectTransform PauseMenuPanel;


    public BuiltZone builtZoneSelected;
    public int score;
    public int gold;
    public int health;
    
    private bool _shopState;
    
    
    public void CloseShop()
    {
        Debug.Log("Closing shop");
        shopPanel.gameObject.SetActive(false);
        builtZoneSelected = null;
        _shopState = false;
    }
    
    public void OpenShop(BuiltZone builtZone)
    {
        Debug.Log("OpenShop");
        shopPanel.gameObject.SetActive(true);
        builtZoneSelected = builtZone;
        _shopState = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
