using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLeftController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed = 10f;
    private Vector3 _spawnPoint;
    private void Awake() => _spawnPoint = transform.position;
    private void Start() => _spawnPoint = transform.position;

    void Update() => transform.Translate(Vector2.left * _speed * Time.deltaTime);

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "LimitLeft")
        {
            transform.position = _spawnPoint;
        }
    }
}
