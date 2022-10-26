using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private float _hp = 10;
    [SerializeField] private UImanager _uiManager;
    [SerializeField] private Click _click;
    [SerializeField] private Slider _zombieSider;
    void OnEnable()
    {
        _uiManager = GameObject.Find("UImanager").GetComponent<UImanager>();
        _click = GameObject.Find("Main Camera").GetComponent<Click>();
    }
    void Start()
    {
        _zombieSider.maxValue = _hp;
        _zombieSider.value = _zombieSider.maxValue;
    }
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.back);

        if (transform.position.z < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    public void damageZombie(float damage)
    {
        _hp -= damage;
        _uiManager.updateSlider(_zombieSider, _hp);
        _click.AddScore();

        if (_hp < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Click click = other.GetComponent<Click>();

            if (click != null)
            {
                click.damagePlayer();
            }
        }
    }
}
