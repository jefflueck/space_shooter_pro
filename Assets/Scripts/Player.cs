using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // public or private
    // data type (int, float, bool, string)
    // every variable has a name
    // optional value assigned

    // The f suffix means that this is a float
    [SerializeField]  // This means that this variable will show up in the inspector
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        // Take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {

        // Calculate movement on the x and y axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a new vector3 and assign to the variable direction
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // Move the player
        transform.Translate(direction * _speed * Time.deltaTime);


        // Clamp the player to the screen on the y axis
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        // Wrap around the screen on the x axis
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }


    void FireLaser()
    {

        _canFire = Time.time + _fireRate;

        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

    }

    public void Damage()
    {
        // this subtracts 1 from the lives
        _lives--;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
        }

    }
}
