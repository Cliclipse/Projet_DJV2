using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BuiltZone : MonoBehaviour
{
    //En gros le but c'est de générer un dico static des différentes tours pour pas les recharger à chaque fois, et en priant suffisament ça devrait marcher
    private static Dictionary< EnumTower.Tower, TowerController> _towersHashMap;
    
    void Start()
    {
        StartCoroutine(LoadTowersDictionaryCoroutine());
    }

    private IEnumerator LoadTowersDictionaryCoroutine()
    {
        if (_towersHashMap == null)
        {
            _towersHashMap = new Dictionary<EnumTower.Tower, TowerController>();
            var loadHandle = Addressables.LoadAssetsAsync<GameObject>("Towers", null);
            yield return loadHandle;
            _towersHashMap = loadHandle.Result.Select((go, index) => new { Index = (EnumTower.Tower)index, Controller = go.GetComponent<TowerController>() }).ToDictionary(x => x.Index, x => x.Controller);
            Debug.Log(_towersHashMap[0].ToString());
        }
    }
    
    
    public void Construct(EnumTower.Tower towerType)
    {
        Instantiate( _towersHashMap[towerType], transform.position, Quaternion.identity);
        Debug.Log("Instantiate tower");
        Destroy(gameObject);
    }
    
    
    // Start is called before the first frame update



    
    // Update is called once per frame
    void Update()
    {
    }
}
