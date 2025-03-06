using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotateSpeed = 19.0f;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;


    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // rotate on the z axis at a speed of 3 m per second
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    // check for laser collision (Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawing();
            Destroy(this.gameObject, 0.5f);
        }
    }

}
