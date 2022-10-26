using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private float _targetDistance = 100;
    [SerializeField] private int _score;
    [SerializeField] private UImanager _UImanager;
    [SerializeField] private float _Health = 10;
    [SerializeField] private float _Damage =2.5f;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _kills;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _UImanager.updateHealth(_Health);
    }
    void Update()
    {
        click();
    }
    void click()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, _targetDistance))
            {
                switch (hit.transform.tag)
                {
                    case "Zombie":
                    Zombie zombie = hit.transform.GetComponent<Zombie>();
                    zombie.damageZombie(_Damage);
                    _UImanager.updateScore(_score);
                    _UImanager.updateIndicatorMessage(1, _Damage);
                     break;
                }
            }
        }
    }

    public void damagePlayer()
    {
        _Health -= 1;

        _UImanager.updateHealth(_Health);

        if (_Health < 1)
        {
            _spawnManager.stopSpawningZombies();
            StartCoroutine(_UImanager.gameOverFlickeringTextRoutine());
            _gameManager.gameOver();
        }
    }
    public void AddScore()
    {
        _score++;
    }

    public void UpdateKills()
    {
        _kills++;
        _UImanager.updateKillAmount(_kills);
    }
}
