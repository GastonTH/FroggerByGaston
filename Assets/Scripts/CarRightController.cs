using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRightController : MonoBehaviour
{

    [SerializeField] private float _speed = 10f;
    private Vector3 _spawnPoint;

    private void Start() => _spawnPoint = transform.position;
    void Update() => transform.Translate(Vector2.right * _speed * Time.deltaTime);

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "LimitRight")
        {
            transform.position = _spawnPoint;
        }
    }
}


