using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private Transform _ZombieContainer;
    [SerializeField] private float _spawnRate;
    [SerializeField] private bool _stopSpawning;
    void Start()
    {
        StartCoroutine(ZombieSpawnRoutine());
    }
    IEnumerator ZombieSpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject zombieSpawned = Instantiate(_zombiePrefab, new Vector3(0f, .67f, .40f), Quaternion.identity);
            zombieSpawned.transform.SetParent(_ZombieContainer);
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void stopSpawningZombies() => _stopSpawning = true;
}
