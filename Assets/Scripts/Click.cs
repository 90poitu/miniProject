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
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _UImanager.updateHealth(_Health);
        _UImanager.updateAttackText(_Damage);
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
}
