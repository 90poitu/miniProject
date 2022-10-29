using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;
    [SerializeField] private UImanager _uiManager;
    [SerializeField] private Click _click;
    [SerializeField] private bool _isPause;
    [SerializeField] private bool _isStarted;
    [SerializeField] private float _priceToUpgrade;
    void Start()
    {
        isPause();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && Time.timeScale == 0)
        {
            _uiManager.GameStart();
        }
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            restartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !_isGameOver && _isStarted)
        {
            isPause();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && _isPause && _isStarted)
        {
            isUnPause();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _click.upgradeNormalDamage((int)_priceToUpgrade);
        }
    }

    public void gameOver()
    {
        _isGameOver = true;
        _uiManager.disableAllText();
    }

    public void isPause()
    {
        _isPause = true;
        Time.timeScale = 0;
    }
    public void isUnPause()
    {
        _isPause = false;
        Time.timeScale = 1;
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void hasStarted()
    {
        _isStarted = true;
    }
    public void increaseUpgradePrice(float amountToIncrease)
    {
        _priceToUpgrade += amountToIncrease;
    }
}
