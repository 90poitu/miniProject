using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Click : MonoBehaviour
{
    [SerializeField] private float _targetDistance = 100;
    [SerializeField] private UImanager _UImanager;
    [SerializeField] private float _Health = 10;
    [SerializeField] private float _Damage = 2.5f;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private int _kills;
    [SerializeField] private int _targetKillGoal = 20;
    [SerializeField] private float _DamagePowerupExpireTime = 0;

    [SerializeField] private float _coinAmount;
    [SerializeField] private float _upgradeDamageAmount;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _UImanager.updateHealth(_Health);
        _UImanager.updateAttackText(_Damage);
        _upgradeDamageAmount += _Damage;
        targetGoal();
    }
    void Update()
    {
        _UImanager.updateTime(Time.timeSinceLevelLoad);
        click();
    }
    void click()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Time.timeScale == 1)
        {
            if (Physics.Raycast(ray, out hit, _targetDistance))
            {
                switch (hit.transform.tag)
                {
                    case "Zombie":
                    Zombie zombie = hit.transform.GetComponent<Zombie>();
                    zombie.damageZombie(_Damage);
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
            _spawnManager.stopSpawning();
            StartCoroutine(_UImanager.gameOverFlickeringTextRoutine());
            _gameManager.gameOver();
            _spawnManager.killAllZombies();
        }
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
            _spawnManager.stopSpawning();
            StartCoroutine(_UImanager.GameWinTextFlickeringRoutine());
            _gameManager.gameOver();
            _spawnManager.killAllZombies();
        }
    }
    public void doubleDamage()
    {
        _Damage *= 2;
        _UImanager.updateAttackText(_Damage);
        StartCoroutine(doubleDamagePowerDownRoutine(_DamagePowerupExpireTime));
    }
    public void update2xDamageExpireTime()
    {
        _UImanager.update2xDamageTextEnable(_DamagePowerupExpireTime);
    }
    public IEnumerator doubleDamagePowerDownRoutine(float expireTime)
    {
        yield return new WaitForSeconds(expireTime);
        _Damage /= 2;
        _UImanager.update2xDamageTextDisable();
        _UImanager.updateAttackText(_Damage);
    }
    public void upgradeNormalDamage(int amount)
    {
        if (_coinAmount >= amount)
        {
            _Damage += 3;
            _upgradeDamageAmount += 3;
            _UImanager.updateAttackText(_Damage);
            _coinAmount -= amount;
            _UImanager.updateCoinsText(_coinAmount);
            _UImanager.updateSuccessfullyUpgradedDamageIndicator(3);
            _gameManager.increaseUpgradePrice(5);
        }
        else
        {
            _UImanager.updateNotEnoughCoinIndicator(amount);
        }
    }
    public void addCoins(float coins)
    {
        float coinRandomAmount = Random.Range(2, 25);
        _coinAmount += coinRandomAmount;
        _UImanager.updateCoinIndicator(coinRandomAmount);
    }
    public void updateCoinsText()
    {
        float coinRandomAmount = Random.Range(2, 25);
        addCoins(coinRandomAmount);
        _UImanager.updateCoinsText(_coinAmount);
    }
    public void updateDamageIndicator()
    {
        _UImanager.updateDamageIndicator(_Damage);
    }
    public void updateUpgradeDamageText()
    {
        _UImanager.updateUpgradeDamageText(_Damage, _upgradeDamageAmount);
    }
}
