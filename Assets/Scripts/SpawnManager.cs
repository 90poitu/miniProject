using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[]  _zombiePrefabs;
    [SerializeField] private GameObject[] _powerups;
    [SerializeField] private Transform _ZombieContainer;
    [SerializeField] private Transform _PowerupsContainer;
    [SerializeField] private Click _click;

    [SerializeField] private float _ZombieSpawnRate;
    [SerializeField] private float _PowerupSpawnRate;
    [SerializeField] private bool _stopSpawning;
    void Start()
    {
        StartCoroutine(ZombieSpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }
    IEnumerator ZombieSpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject zombieSpawned = Instantiate(_zombiePrefabs[Random.Range(0, _zombiePrefabs.Length)], new Vector3(0f, .67f, .40f), Quaternion.identity);
            zombieSpawned.transform.SetParent(_ZombieContainer);
            yield return new WaitForSeconds(_ZombieSpawnRate);
        }
    }
    IEnumerator PowerupSpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject powerupSpawned = Instantiate(_powerups[Random.Range(0, _powerups.Length)], new Vector3(0f, .67f, .40f), Quaternion.identity);
            powerupSpawned.transform.SetParent(_PowerupsContainer);
            yield return new WaitForSeconds(_PowerupSpawnRate);
        }
    }
    public void stopSpawning() => _stopSpawning = true;

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
