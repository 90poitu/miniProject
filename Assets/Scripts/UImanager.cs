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
    void Start()
    {
        if (_scoreText.gameObject.activeInHierarchy && _hpText.gameObject.activeInHierarchy
            && _scoreAddedText.gameObject.activeInHierarchy &&
            _gameOverText.gameObject.activeInHierarchy &&
            _pressRtoRestartText.gameObject.activeInHierarchy
            && _killAmountText.gameObject.activeInHierarchy && 
            _targetKillText.gameObject.activeInHierarchy &&
            _winText.gameObject.activeInHierarchy == false)
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
    public void updateIndicatorMessage(float damage)
    {
        Text text = Instantiate(_scoreAddedText);
        text.transform.SetParent(_scoreContainer);
        text.text = "+ " + damage.ToString("f2");
    }
    public void updateSlider(Slider slider, float SliderAmount)
    {
        slider.value = SliderAmount;
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
    }
}
