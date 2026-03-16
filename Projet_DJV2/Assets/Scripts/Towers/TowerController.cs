using UnityEngine;
using UnityEngine.EventSystems;

//Les imports d'interface permettent d'utiliser un evenement qui regarde si l'objet est pointé par la souris.
//Sinon je devais soit regarder pour cheque tour si elle était visé (bien gourmand) soit le faire avec un objet
//en plus sur la scène spécialement pour ça, clairement pas ouf en terme de pratique
public class TowerController : MonoBehaviour ,  IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TowerData towerData;
    [SerializeField] private GameObject rangeIndicator;

    private int _cost;
    
    private ShootManager _shootManager;
    private AudioSource _audioSource;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _shootManager = GetComponent<ShootManager>();
        _audioSource = GetComponent<AudioSource>();
        
        _shootManager.SetProjectilsShot(towerData.projectilsShot);
        _shootManager.SetProjectileSpeed(towerData.shotCooldown);
        _shootManager.SetProjectileDamages(towerData.projectileDamages);
        _shootManager.SetShotCooldown(towerData.shotCooldown);
        _shootManager.SetRange(towerData.range);
        
        _audioSource.clip = towerData.castSound;
        
        _cost = towerData.cost;
        rangeIndicator.SetActive(false);

    }

    
    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("OnMouseEnter");
        rangeIndicator.transform.localScale = Vector3.one * towerData.range * 2;
        rangeIndicator.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        Debug.Log("OnMouseExit");
        rangeIndicator.SetActive(false);
    }

}
