using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using JetBrains.Annotations;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;

namespace Level
{
    /// <summary>
    /// Classe repésentant une arène de jeu
    /// </summary>
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform[] pathPoints;
        [SerializeField] private LevelData levelData;

        // La liste des addressables des ennemis
        private EnemyController[] _enemies;
        
        /// <summary>
        /// Spawnpoint du level
        /// </summary>
        public Transform SpawnPoint => spawnPoint;
        
        /// <summary>
        /// Données du level
        /// </summary>
        public LevelData LevelData => levelData;
        
        /// <summary>
        /// Action pour prévenir que le level est prêt à spawn des ennemis
        /// </summary>
        public event Action OnReady;
        
        /// <summary>
        /// Le level est prêt à spawn des ennemis
        /// </summary>
        public bool IsReady { get; private set; }

        /// <summary>
        /// Récupérer le prochain point de la route
        /// </summary>
        /// <param name="pathPoint">Le point dont on cherche le suivant</param>
        /// <returns></returns>
        public Transform GetNextPathPoint([CanBeNull] Transform pathPoint)
        {
            if (pathPoint)
            {
                int index = System.Array.IndexOf(pathPoints, pathPoint);
                return pathPoints[index + 1];
            }

            return pathPoints[0];
        }
        
        /// <summary>
        /// Récupère la liste des addressables d'ennemies
        /// </summary>
        private IEnumerator Start()
        {
            var loadHandle = Addressables.LoadAssetsAsync<GameObject>("Enemy", null);
            yield return loadHandle;
            _enemies = loadHandle.Result.Select(go => go.GetComponent<EnemyController>()).ToArray();
            IsReady = true;
            OnReady?.Invoke();
        }

        /// <summary>
        /// Renvoie la liste des prefabs des ennemis d'une vague dans l'ordre
        /// </summary>
        /// <param name="waveNumber">Numéro de la vague</param>
        /// <returns>Liste des ennemis controllers dans l'ordre</returns>
        public EnemyController[] GetEnnemiesOfWave(int waveNumber)
        {
            Dictionary<int, List<WaveEntryJson>> waves = JsonConvert.DeserializeObject<Dictionary<int, List<WaveEntryJson>>>(levelData.waves.text);
            List<WaveEntryJson> waveEntry = waves[waveNumber];
            List<EnemyController> enemies = new List<EnemyController>();
            foreach (var enemyEntry in waveEntry)
            {
                // On cherche le prefab qui correspond à l'id renseigné dans le json
                var enemyPrefab = _enemies.First(e => e.EnemyData.id == enemyEntry.id);
                for (int i = 0; i < enemyEntry.count; i++)
                {
                    enemies.Add(enemyPrefab);
                }
            }
            return enemies.ToArray();
        }
    }
}
