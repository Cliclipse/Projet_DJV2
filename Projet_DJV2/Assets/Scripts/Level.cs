using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] pathPoints;
    
    private EnemyController[] _enemies;

    private void StartWave()
    {
        for (int i = 0; i < 2; i++)
        {
            var enemyPrefab = _enemies[Random.Range(0, _enemies.Length)];
            var enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
        }
    }

    private IEnumerator Start()
    {
        var loadHandle = Addressables.LoadAssetsAsync<GameObject>("Enemy", null);
        yield return loadHandle;
        _enemies = loadHandle.Result.Select(go => go.GetComponent<EnemyController>()).ToArray();
        StartWave();
    }
}
