using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using GameMachine;
using JetBrains.Annotations;
using ScriptableObjects;
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
        [SerializeField] private GameSession gameSession;
        [SerializeField] ShopManager shopManager;
    
        /// <summary>
        /// Arène actuelle présente dans la scène
        /// </summary>
        [SerializeField] private Level level;
        [SerializeField] private float timeBetweenWaves = 10f;
        
        private Camera _mainCamera;
        private int _ennemisCount;
        private int _waveNumber;
        
        // La liste des addressables des levels
        private Level[] _levels;
        private ScoreController _scoreController;
        public ScoreController ScoreController => _scoreController;

        public int gold;
        public int health;
        
        [Header("GameStateMachine")]
        [SerializeField] private Canvas winScreen;
        [SerializeField] private Canvas loseScreen;
        [SerializeField] private Canvas pauseScreen;

        public GameStateMachine GameStateMachine { get; private set; }
        public Canvas WinScreen => winScreen;
        public Canvas LoseScreen => loseScreen;
        public Canvas PauseScreen => pauseScreen;

        /// <summary>
        /// Numéro de la vague actuelle
        /// </summary>
        public int WaveNumber => _waveNumber;
        
        /// <summary>
        /// Nombre de vagues du niveau
        /// </summary>
        public int WaveCount => level.LevelData.waveCount;
    
        private UnityEvent _onPause = new();

        public bool levelLoaded;
        /// <summary>
        /// Initialise le singleton, la state machine, les ressources du niveau et la caméra.
        /// </summary>
        void Awake()
        {
            GameStateMachine = new GameStateMachine(this);
            GameStateMachine.Initialize(GameStateMachine.PlayState);
            _scoreController = GetComponent<ScoreController>();
        }

        /// <summary>
        /// Gère le spawn du level et l'initialisation variables du jeu
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start()
        {
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
            
            var loadHandle = Addressables.LoadAssetsAsync<GameObject>("Level", null);
            yield return loadHandle;
            _levels = loadHandle.Result.Select(go => go.GetComponent<Level>()).ToArray();
            var levelPrefab = _levels.First(l => l.LevelData.levelIndex == gameSession.levelIndex);
            level = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);
            levelLoaded = true;
            
            gold = level.LevelData.initialGold;
            health = level.LevelData.intialLife;
            _waveNumber = 0;
            _ennemisCount = 0;
            
            GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
            _mainCamera = camObj.GetComponent<Camera>();
            AudioSource backgroundMusic = camObj.GetComponent<AudioSource>();
            backgroundMusic.clip = level.LevelData.levelMusic;
            backgroundMusic.Play();
            
            // On attend que le level ai chargé les ennemis avant de commencer à les spawn
            if (level.IsReady)
            {
                BeginEnnemiesSpawn();
            }
            else
            {
                level.OnReady += BeginEnnemiesSpawn;
            }
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
                        if (clicked == null)shopManager.CloseShop();
                        else if (clicked.TryGetComponent(out BuiltZone builtZone))shopManager.OpenShop(builtZone);
                        else shopManager.CloseShop();
                    }
                }
            } 
        }
        

        
        void Update()
        {
            ClickManager();
            GameStateMachine.Update();
        }
        
        /// <summary>
        /// Démarre une vague d'ennemis
        /// </summary>
        /// <returns>Délai entre chaque spawn d'ennemis</returns>
        private IEnumerator StartWave()
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            _waveNumber++;
            EnemyController[] ennemies = level.GetEnnemiesOfWave(_waveNumber);
            foreach (var enemyPrefab in ennemies)
            {
                EnemyController enemy = Instantiate(enemyPrefab, level.SpawnPoint.position, Quaternion.identity, level.SpawnPoint);
                enemy.AddOnDeathListener(HandleEnemyDeath);
                enemy.AddOnReachCastleListener(HandleEnemyReachCastle);
                enemy.Target = level.GetNextPathPoint(null);
                _ennemisCount++;
                yield return new WaitForSeconds(1.5f);
            }
        }

        private void BeginEnnemiesSpawn()
        {
            StartCoroutine(StartWave());
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

        /// <summary>
        /// Le point de la route est-il le dernier ?
        /// </summary>
        /// <param name="pathPoint">Point actuel</param>
        /// <returns>Le point est le dernier</returns>
        public bool IsLastPathPoint([CanBeNull] Transform pathPoint)
        {
            return level.IsLastPathPoint(pathPoint);
        }
        
        private void HandleEnemyDeath(EnemyController enemy)
        {
            gold += enemy.EnemyData.reward;
            _scoreController?.KillEnemy(enemy.EnemyData);
            _ennemisCount--;
            if (_ennemisCount <= 0) EndOfWave();
        }

        private void HandleEnemyReachCastle(EnemyController enemy)
        {
            health -= enemy.EnemyData.damages;
            _scoreController?.EnemyReachCastle(enemy.EnemyData);
            _ennemisCount--;
            if (health <= 0) GameStateMachine.TransitionTo(GameStateMachine.LoseState);
            else if (_ennemisCount <= 0) EndOfWave();
        }

        /// <summary>
        /// Gère la fin d'une vague (quand plus aucun ennemi ne reste)
        /// </summary>
        private void EndOfWave()
        {
            if (_waveNumber == level.LevelData.waveCount)
            {
                GameStateMachine.TransitionTo(GameStateMachine.WinState);
            }
            else StartCoroutine(StartWave());
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
