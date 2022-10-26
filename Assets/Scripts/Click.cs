using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private float _targetDistance = 100;
    [SerializeField] private int _score;
    [SerializeField] private UImanager _UImanager;
    [SerializeField] private float _Health = 10;
    [SerializeField] private float _MinDamage;
    [SerializeField] private float _MaxDamage;
    [SerializeField] private float _totalDamage;
    [SerializeField] private float _MinScoreAmount;
    [SerializeField] private float _MaxScoreAmount;
    [SerializeField] private float _totalScore;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameManager _gameManager;
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
                    float randomScoreEarned = Random.Range(_MinScoreAmount, _MaxScoreAmount);
                    float randomDamage = Random.Range(_MinDamage, _MaxDamage);
                    _totalScore += randomScoreEarned;
                    _totalDamage =+ randomDamage;
                    Zombie zombie = hit.transform.GetComponent<Zombie>();
                    zombie.damageZombie(_totalDamage);
                    _UImanager.updateScore(_totalScore);
                    _UImanager.updateIndicatorMessage(randomScoreEarned, _totalDamage);
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
}
