using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Level
{
    /// <summary>
    /// Classe représentant le controller des arènes. C'est un singleton qui invoque arènes choisies.
    /// Il gère les ennemies présents, les tours et les états du jeu
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        //implémenter un singleton ensuite puis le fait que ce soit une machine à état entre l'animation de départ,
        //le menu pause, le jeu avec le shop ouvert ou le shop non ouvert
        [SerializeField] RectTransform shopPanel;
        [SerializeField] private TowerData[] towerData; // ce serait mieux de faire un dico mais on peut pas le serializeField et chiant à construire avec l'enum donc flemme
        //Dcp order : 0: Crossbow , 1: Mage , 2: Archer 
    
        /// <summary>
        /// Arène actuelle présente dans la scène
        /// </summary>
        [SerializeField] private Level level;
        
        private Camera _mainCamera;

        public BuiltZone builtZoneSelected;
        public int score;
        public int gold;
        public int health;
    
        private bool _shopState;
        private EnemyController[] _enemies;
    
    
        [Header("GameStateMachine")]
        [SerializeField] private Canvas winScreen;
        [SerializeField] private Canvas loseScreen;
        [SerializeField] private Canvas pauseScreen;

        public GameStateMachine GameStateMachine { get; private set; }
        public Canvas WinScreen => winScreen; //Raccourci Getter
        public Canvas LoseScreen => loseScreen;
        public Canvas PauseScreen => pauseScreen;

    
    
        private UnityEvent _onPause = new();

    
        /// <summary>
        /// Ferme le shop et réinitialise la zone sélectionnée.
        /// </summary>
        public void CloseShop()
        {
            shopPanel.gameObject.SetActive(false);
            builtZoneSelected = null;
            _shopState = false;
        }
    
        /// <summary>
        /// Ouvre le shop et associe la zone de construction sélectionnée.
        /// </summary>
        /// <param name="builtZone">La zone de construction sur laquelle le joueur a cliqué.</param>
        public void OpenShop(BuiltZone builtZone)
        {
            shopPanel.gameObject.SetActive(true);
            builtZoneSelected = builtZone;
            _shopState = true;
        }
    
        /// <summary>
        /// Initialise le singleton, la state machine, les ressources du niveau et la caméra.
        /// </summary>
        void Awake()
        {
            // Singleton
            if (FindObjectOfType<LevelController>() != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        
            GameStateMachine = new GameStateMachine(this);
            GameStateMachine.Initialize(GameStateMachine.PlayState);
        
            gold = level.LevelData.initialGold;
            health = level.LevelData.intialLife;
        
            _shopState = false;
            builtZoneSelected = null;
            shopPanel.gameObject.SetActive(false);
        
            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
            _mainCamera = camObj.GetComponent<Camera>();
            AudioSource backgroundMuisc = camObj.GetComponent<AudioSource>();
            backgroundMuisc.clip = level.LevelData.levelMusic;
            backgroundMuisc.Play();
        }
    
        /// <summary>
        /// Vérifie si le pointeur de la souris survole un élément UI.
        /// </summary>
        /// <returns>True si le curseur est au-dessus d'un élément UI, false sinon.</returns>
        private bool IsPointerOverUI()
        {
            // TODO : Réimplémenter la méthode 
            //En gros le raycaster sur la cam que j'ai mis pour détecter le passage de la souris sur les tours, fait planter
            //la fonction qui détectait si la souris était au dessus l'ui, donc faut en refaire une.
            PointerEventData pointerData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            return results.Exists(r => r.gameObject.layer == LayerMask.NameToLayer("UI"));
        }
    
        /// <summary>
        /// Gère les clics souris : ouvre le shop si une <see cref="BuiltZone"/> est cliquée,
        /// le ferme sinon. Ignoré si le curseur est sur l'UI.
        /// </summary>
        protected void ClickManager()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Transform clicked = hit.collider.gameObject.transform.parent;

                    if (!IsPointerOverUI())
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
        
        /// <summary>
        /// Tente d'acheter et de construire une tour sur la zone sélectionnée.
        /// Déduit le coût en or si le joueur en a suffisamment.
        /// </summary>
        /// <param name="towerBoughtNumber">
        /// Index de la tour dans <c>towerData</c>, casté en <see cref="EnumTower.Tower"/>.
        /// </param>
        public void TowerBought(int towerBoughtNumber)
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
        
        void Update()
        {
            ClickManager();
            GameStateMachine.Update();
        }
        
        /// <summary>
        /// Démarre une vague d'ennemos
        /// </summary>
        /// <returns>Délai entre chaque spawn d'ennemis</returns>
        private IEnumerator StartWave()
        {
            for (int i = 0; i < 6; i++)
            {
                var enemyPrefab = _enemies[Random.Range(0, _enemies.Length)];
                var enemy = Instantiate(enemyPrefab, level.SpawnPoint.position, Quaternion.identity, level.SpawnPoint);
                enemy.Target = level.GetNextPathPoint(null);
                yield return new WaitForSeconds(0.5f);
            }
        }

        /// <summary>
        /// Récupérer le prochain point de la route
        /// </summary>
        /// <param name="pathPoint">Le point dont on cherche le suivant</param>
        /// <returns></returns>
        public Transform GetNextPathPoint([CanBeNull] Transform pathPoint)
        {
            return level.GetNextPathPoint(pathPoint);
        }

        private IEnumerator Start()
        {
            var loadHandle = Addressables.LoadAssetsAsync<GameObject>("Enemy", null);
            yield return loadHandle;
            _enemies = loadHandle.Result.Select(go => go.GetComponent<EnemyController>()).ToArray();
            StartCoroutine(StartWave());
        }

    
        //=========GESTION ETAT PAUSE=========
        //Crée en enlève le listener qui détecte le bouton qui met la fin de la pause
        public void AddPauseListener(UnityAction listener) => _onPause.AddListener(listener);
        public void RemovePauseListener(UnityAction listener) => _onPause.RemoveListener(listener);

        public void TogglePause()
        {
            _onPause.Invoke();
        }
    
    }
}
