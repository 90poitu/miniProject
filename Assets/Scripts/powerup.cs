using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _PowerupID;
    [SerializeField] private Click _click;
    void Start()
    {
        _click = GameObject.Find("Main Camera").GetComponent<Click>();
    }
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.back);

        if (transform.position.z < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            switch (_PowerupID)
            {
                case 0:
                    _click.doubleDamage();
                    break;
                case 1:
                    Debug.Log("Powerup 1");
                    break;
            }
        }
    }
}
