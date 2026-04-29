using Enum;
using Level;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

//Les imports d'interface permettent d'utiliser un evenement qui regarde si l'objet est pointé par la souris.
//Sinon je devais soit regarder pour cheque tour si elle était visé (bien gourmand) soit le faire avec un objet
//en plus sur la scène spécialement pour ça, clairement pas ouf en terme de pratique
namespace Towers
{
    public class TowerController : MonoBehaviour ,  IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TowerDataLevel towerDataLevel;
        [SerializeField] private InformationPanel informationPanel;
        [SerializeField] private GameObject rangeIndicator;

        public EnumTower.Tower towerTypeEnum;

        


        private TowerData towerData;
        private int level;

        private LevelController levelController;
        private TowerAnimatorManager towerAnimatorManager;

    
        private ShootManager _shootManager;
        private AudioSource _audioSource;
    
    
        private bool _isSelected = false; //Mini machine à 2 états
        private bool _isMaxLevelUp = false;
    
        // Start is called before the first frame update
        void Start()
        {
            levelController = FindFirstObjectByType<LevelController>();
            _shootManager = GetComponent<ShootManager>();
            _audioSource = GetComponent<AudioSource>();

            towerAnimatorManager = GetComponent<TowerAnimatorManager>();
        
            level = 1;
            towerData = towerDataLevel.towerDatas[0];

            StatUpdate();
        
            _audioSource.clip = towerData.castSound;
        
        
            rangeIndicator.SetActive(false);
            informationPanel.gameObject.SetActive(false);
            
            towerAnimatorManager.SetUpdatedState(true);

            if (PoolManager.Instance != null)_shootManager.SetProjectilePool(PoolManager.Instance.GetPool(towerTypeEnum));
            else Debug.LogError("Faut rajouter un poolManager dans la scène");
        }

        public TowerData GetTowerData()
        {
            return towerData;
        }
        
        private void ShooterStatUpdate()
        {
            _shootManager.SetProjectilsShot(towerData.projectilsShot);
            _shootManager.SetProjectileSpeed(towerData.projectilSpeed);
            _shootManager.SetProjectileDamages(towerData.projectileDamages);
            _shootManager.SetShotCooldown(towerData.shotCooldown);
            _shootManager.SetRange(towerData.range);
        }

        //Si Je dois mettre à jour d'autre chose avec les données du scriptable
        private void StatUpdate()
        {
            ShooterStatUpdate();
            if (_isMaxLevelUp) informationPanel.UpdateData(towerData , null , _isMaxLevelUp);
            else informationPanel.UpdateData(towerData ,  towerDataLevel.towerDatas[level] , _isMaxLevelUp);
        }


        private void LevelUp()
        {
            towerData = towerDataLevel.towerDatas[level]; //On prend tjr à -1 du level donc prendre level permet de prendre le suivant
            level++;
            if (level == towerDataLevel.towerDatas.Length) _isMaxLevelUp = true;
            StatUpdate();
        }

        private void VerifLevelUp()
        {
            int cost = towerDataLevel.towerDatas[level].cost; //Meme sans le +1 ca va chercher le coup de la tour d'après
            if (levelController == null) //Si pas de levelController j'assume que c'est une scène de test qui vérif pas les gold
            {
                Debug.Log("Pas de level controller j'assume que c'est une scène de test. Amélioration sans coût");
                LevelUp();
            }
            else if (levelController.gold >= cost)
            {
                levelController.gold -= towerDataLevel.towerDatas[level + 1].cost;
                LevelUp();

                Debug.Log("LevelUp de la tour");
            }
            else
            {
                Debug.Log("Trop pauvre");
            }
        }

    
        public void OnPointerEnter(PointerEventData eventData){
            _isSelected = true;
            rangeIndicator.transform.localScale = Vector3.one * towerData.range * 2;
            informationPanel.gameObject.SetActive(true);
            rangeIndicator.SetActive(true);
            // ajouter un event d'update et quand je suis activé je mets un listener sur le fait que cette touche soit pressé dans le level controller, et j'upgrade si c'est le cas
        }

        public void OnPointerExit(PointerEventData eventData){
            _isSelected = false;
            rangeIndicator.SetActive(false);
            informationPanel.gameObject.SetActive(false);

        }

        void Update()
        {
            if (_isSelected && Input.GetKeyDown(KeyCode.Space) && !_isMaxLevelUp) VerifLevelUp();
        }

    }
}
