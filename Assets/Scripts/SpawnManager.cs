using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[]  _zombiePrefabs;
    [SerializeField] private Transform _ZombieContainer;
    [SerializeField] private float _spawnRate;
    [SerializeField] private bool _stopSpawning;
    void Start()
    {
        StartCoroutine(ZombieSpawnRoutine());
    }
    void Update()
    {
    }
    IEnumerator ZombieSpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject zombieSpawned = Instantiate(_zombiePrefabs[Random.Range(0, _zombiePrefabs.Length)], new Vector3(0f, .67f, .40f), Quaternion.identity);
            zombieSpawned.transform.SetParent(_ZombieContainer);
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void stopSpawningZombies() => _stopSpawning = true;

    public void killAllZombies()
    {
        if (_stopSpawning)
        {
            for (int i = 0; i < _ZombieContainer.childCount; i++)
            {
                Destroy(_ZombieContainer.GetChild(i).gameObject);
            }
        }
    }
}
