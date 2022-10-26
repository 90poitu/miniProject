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
    public void updateScore(float _score)
    {
        _scoreText.text = "Score: " + _score.ToString("f2");
    }
    public void updateHealth(float _hp)
    {
        _hpText.text = "Health: " + _hp;
    }
    public void updateIndicatorMessage(float amount, float damage)
    {
        Text text = Instantiate(_scoreAddedText);
        text.transform.SetParent(_scoreContainer);
        RectTransform rect = text.transform.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        text.text = "+ " + amount.ToString("f2") + " <color=blue>DMG " + damage.ToString("f2") + "</color>";
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
}
