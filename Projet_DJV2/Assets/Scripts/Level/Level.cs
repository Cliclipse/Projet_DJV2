using System.Collections;
using System.Linq;
using Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        
        public Transform SpawnPoint => spawnPoint;
        public LevelData LevelData => levelData;

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
    }
}
