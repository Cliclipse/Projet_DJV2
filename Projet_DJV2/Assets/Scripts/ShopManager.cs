using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enum;
using Level;
using ScriptableObjects;
using Towers;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TowerData[] towerDatas; //Dico + opti mais par serializable, 0: Crossbow , 1: Mage , 2: Archer 
    
    [SerializeField] private LevelController levelController;
        
    [SerializeField] RectTransform shopPanel;
    private Dictionary< EnumTower.Tower, TowerController> _towersHashMap;


    private BuiltZone builtZoneSelected {get; set;}
    
    
    
    /// <summary>
    /// Tente d'acheter et de construire une tour sur la zone sélectionnée.
    /// Déduit le coût en or si le joueur en a suffisamment.
    /// </summary>
    /// <param name="towerBoughtNumber">
    /// Index de la tour dans <c>towerData</c>, casté en <see cref="EnumTower.Tower"/>.
    /// </param>
    public void TowerBought(int towerBoughtNumber)
    {
        int cost = towerDatas[towerBoughtNumber].cost;
        if (levelController.gold > cost)
        {
            Debug.Log("TowerBought");
            EnumTower.Tower towerBought = (EnumTower.Tower) towerBoughtNumber;
            builtZoneSelected.Construct(towerBought, _towersHashMap);
            levelController.gold -= cost;
            CloseShop();
        }
        else
        {
            Debug.Log("T'es trop pauvre");
        }
    }
    
    
    /// <summary>
    /// Ferme le shop et réinitialise la zone sélectionnée.
    /// </summary>
    public void CloseShop()
    {
        gameObject.SetActive(false);
        builtZoneSelected = null;
    }
    
    
    /// <summary>
    /// Ouvre le shop et associe la zone de construction sélectionnée.
    /// </summary>
    /// <param name="builtZone">La zone de construction sur laquelle le joueur a cliqué.</param>
    public void OpenShop(BuiltZone builtZone)
    {
        gameObject.SetActive(true);
        builtZoneSelected = builtZone;
    }

    
    //En gros le but c'est de générer un dico static des différentes tours pour pas les recharger à chaque fois, et en priant suffisament ça devrait marcher
    
    void Start()
    {
        builtZoneSelected = null;
        StartCoroutine(LoadTowersDictionaryCoroutine());
    }

    //Faudrait mettre ça dans le temps de chargement, car là le shop est activé jusqu'à ce que j'ai chargé le diso, je peux pas le charger quand le shop est desactivé
    private IEnumerator LoadTowersDictionaryCoroutine()
    {        
        Debug.Log("InitialisationHashMap :");
        _towersHashMap = new Dictionary<EnumTower.Tower, TowerController>();
        var loadHandle = Addressables.LoadAssetsAsync<GameObject>("Towers", null);
        yield return loadHandle;
        _towersHashMap = loadHandle.Result
            .Select(gameObject => gameObject.GetComponent<TowerController>())
            .ToDictionary(tc => tc.towerTypeEnum, tc => tc);
        Debug.Log("Tower HashMap :" +_towersHashMap[0].ToString());
        gameObject.SetActive(false);
    }
}
