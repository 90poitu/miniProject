using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private float _targetDistance = 100;
    [SerializeField] private int _score;
    [SerializeField] private UImanager _UImanager;
    [SerializeField] private float _Health = 10;
    [SerializeField] private float _Damage = 2.5f;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _kills;
    [SerializeField] private int _targetKillGoal = 20;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _UImanager.updateHealth(_Health);
        targetGoal();
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
                    _UImanager.updateIndicatorMessage(_Damage);
                     break;
                }
            }
        }
    }

    public void damagePlayer(float _damage)
    {
        _Health -= _damage;

        _UImanager.updateHealth(_Health);

        if (_Health < 1)
        {
            _spawnManager.stopSpawningZombies();
            StartCoroutine(_UImanager.gameOverFlickeringTextRoutine());
            _gameManager.gameOver();
            _spawnManager.killAllZombies();
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
    public void targetGoal()
    {
        _UImanager.updateKillTargetGoal(_kills, _targetKillGoal);

        if (_kills >= _targetKillGoal)
        {
            _spawnManager.stopSpawningZombies();
            StartCoroutine(_UImanager.GameWinTextFlickeringRoutine());
            _gameManager.gameOver();
            _spawnManager.killAllZombies();
        }
    }
}
