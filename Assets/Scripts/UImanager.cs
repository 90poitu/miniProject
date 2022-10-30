using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _scoreAddedText;
    [SerializeField] private Transform _scoreContainer;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _pressRtoRestartText;
    [SerializeField] private Text _killAmountText;
    [SerializeField] private Text _targetKillText;
    [SerializeField] private Text _winText;
    [SerializeField] private Text _pressAnyKeyToStartText;
    [SerializeField] private Text _attackText;
    [SerializeField] private Text _CoinsText;
    [SerializeField] private Text _CoinsAddedText;
    [SerializeField] private Text _notEnoughCoinText;
    [SerializeField] private Text _successfullyUpgradedDamageText;
    [SerializeField] private Image _powerupElementsBackground;
    [SerializeField] private GameManager _gameManager;
    void Start()
    {
        if (_scoreText.gameObject.activeInHierarchy && _hpText.gameObject.activeInHierarchy
            && _scoreAddedText.gameObject.activeInHierarchy &&
            _gameOverText.gameObject.activeInHierarchy &&
            _pressRtoRestartText.gameObject.activeInHierarchy
            && _killAmountText.gameObject.activeInHierarchy && 
            _targetKillText.gameObject.activeInHierarchy &&
            _winText.gameObject.activeInHierarchy && 
            _powerupElementsBackground.gameObject.activeInHierarchy &&
            _CoinsText.gameObject.activeInHierarchy == false)
        {
            enableAllText();
        }
    }
    public void updateTime(float _score)
    {
        _scoreText.text = "Time: " + _score.ToString("f2");
    }
    public void updateHealth(float _hp)
    {
        _hpText.text = "Health: " + _hp;
    }
    public void updateKillAmount(int killAmount)
    {
        _killAmountText.text = "Kill: " + killAmount;
    }
    public void updateKillTargetGoal(int currentKill, int targetGoal)
    {
        _targetKillText.text = "Kill: " + currentKill + " / "+ targetGoal;
    }
    public void updateDamageIndicator(float damage)
    {
        Text text = Instantiate(_scoreAddedText);
        text.transform.SetParent(_scoreContainer);
        text.text = "+ " + damage.ToString("f2");
    }
    public void updateCoinIndicator(float coins)
    {
        Text text = Instantiate(_CoinsAddedText);
        text.transform.SetParent(_scoreContainer);
        text.text = "<color=blue>+ " + coins.ToString("f2") + "</color>";
    }
    public void updateNotEnoughCoinIndicator(float coin)
    {
        Text text = Instantiate(_notEnoughCoinText);
        text.transform.SetParent(_scoreContainer);
        text.text = "<color=green>You need " + coin + " coin(s)</color>";
    }
    public void updateSlider(Slider slider, float SliderAmount)
    {
        slider.value = SliderAmount;
    }
    public void updateZombieText(Text text, float zombieHp, Slider slider)
    {
        text.text = zombieHp + " / " + slider.maxValue;
    }

    public void updateAttackText(float damage)
    {
        _attackText.text = "Damage: " + damage;
    }
    public void updateCoinsText(float coinAmount)
    {
        _CoinsText.text = "Coins: " + coinAmount;
    }
    public void updateSuccessfullyUpgradedDamageIndicator(float damageGoal)
    {
        Text text = Instantiate(_successfullyUpgradedDamageText);
        text.transform.SetParent(_scoreContainer);
        text.text = "<color=green>Successfully upgraded dmg +" + damageGoal + "</color>";
    }
    public void update2xDamageTextEnable(float expireTime)
    {
        _powerupElementsBackground.gameObject.SetActive(true);
        Text text = _powerupElementsBackground.transform.GetChild(0).GetComponent<Text>();

        text.text = "2x damage " + "("+expireTime+" s)";
    }
    public void update2xDamageTextDisable()
    {
        _powerupElementsBackground.gameObject.SetActive(false);
    }
    public void GameStart()
    {
        _pressAnyKeyToStartText.gameObject.SetActive(false);
        _gameManager.isUnPause();
        _gameManager.hasStarted();
    }
    public IEnumerator gameOverFlickeringTextRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            _pressRtoRestartText.gameObject.SetActive(true);
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
    public IEnumerator GameWinTextFlickeringRoutine()
    {
        while (true)
        {
            _pressRtoRestartText.gameObject.SetActive(true);
            _winText.gameObject.SetActive(true);
            _winText.text = "YOU WON";
            yield return new WaitForSeconds(.5f);
            _winText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    public void disableAllText()
    {
        _scoreText.gameObject.SetActive(false);
        _hpText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _pressRtoRestartText.gameObject.SetActive(false);
        _killAmountText.gameObject.SetActive(false);
        _targetKillText.gameObject.SetActive(false);
        _winText.gameObject.SetActive(false);
        _powerupElementsBackground.gameObject.SetActive(false);
        _CoinsText.gameObject.SetActive(false);
    }
    public void enableAllText()
    {
        _scoreText.gameObject.SetActive(true);
        _hpText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        _pressRtoRestartText.gameObject.SetActive(true);
        _killAmountText.gameObject.SetActive(true);
        _targetKillText.gameObject.SetActive(true);
        _winText.gameObject.SetActive(true);
        _powerupElementsBackground.gameObject.SetActive(true);
        _CoinsText.gameObject.SetActive(true);
    }
}
