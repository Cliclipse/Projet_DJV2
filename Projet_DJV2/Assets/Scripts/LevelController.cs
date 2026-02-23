using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

public class LevelController : MonoBehaviour
{
    //implémenter un singleton ensuite puis le fait que ce soit une machine à état entre l'animation de départ,
    //le menu pause, le jeu avec le shop ouvert ou le shop non ouvert
    [SerializeField] RectTransform shopPanel;
    [SerializeField] RectTransform PauseMenuPanel;
    
    [SerializeField] private TowerData[] towerData; // ce serait mieux de faire un dico mais on peut pas le serializeField et chiant à construire avec l'enum donc flemme
    //Dcp order : 0: Crossbow , 1: Mage , 2: Archer 
    
    [SerializeField] private LevelData levelData;

    
    protected Camera _mainCamera;


    public BuiltZone builtZoneSelected;
    public int score;
    public int gold;
    public int health;
    
    private bool _shopState;
    

    
    
    public void CloseShop()
    {
        shopPanel.gameObject.SetActive(false);
        builtZoneSelected = null;
        _shopState = false;
    }
    
    public void OpenShop(BuiltZone builtZone)
    {
        shopPanel.gameObject.SetActive(true);
        builtZoneSelected = builtZone;
        _shopState = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gold = levelData.initialGold;
        health = levelData.intialLife;
        
        _shopState = false;
        builtZoneSelected = null;
        shopPanel.gameObject.SetActive(false);
        
        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        _mainCamera = camObj.GetComponent<Camera>();
    }
    
    protected void ClickManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Transform clicked = hit.collider.gameObject.transform.parent;

                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (clicked == null) CloseShop();
                    else if (clicked.TryGetComponent<BuiltZone>(out BuiltZone builtZone))
                    {
                        OpenShop(builtZone);
                    }
                }
            }
        } 
    }
    
    
    
    public void TowerBought(int towerBoughtNumber) //Pas le choix de prendre un entier sinon le bouton le veut pas
    {
        int cost = towerData[towerBoughtNumber].cost;
        if (gold > cost)
        {
            Debug.Log("TowerBought");
            EnumTower.Tower towerBought = (EnumTower.Tower) towerBoughtNumber;
            builtZoneSelected.Construct(towerBought);
            gold -= cost;
        }
        else
        {
            Debug.Log("T'es trop pauvre");
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        ClickManager();
        
    }

}
