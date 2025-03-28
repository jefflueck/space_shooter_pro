using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    // public or private
    // data type (int, float, bool, string)
    // every variable has a name
    // optional value assigned

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _speedMultiplier = 10.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;


    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _score;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;


    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;



    [SerializeField]
    private GameObject _shieldVisualizer;

    private UIManager _uiManager;

    [SerializeField]
    private GameManager _gameManager;



    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;



    // Start is called before the first frame update
    void Start()
    {
        // Take current position = new position (0, 0, 0)



        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }

        if (_gameManager._isCoOpMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }


        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL");
        }
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
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

        // Move the player speed based on if speed boost is active
        if (_isSpeedBoostActive == true)
        {
            transform.Translate(direction * _speedMultiplier * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }



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
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        // play the audio laser clip
        _audioSource.Play();
    }



    public void Damage()
    {

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            // deactivate the shield visual
            _shieldVisualizer.SetActive(false);


            return;
        }
        // this subtracts 1 from the lives
        _lives--;


        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }


        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);

        }

    }


    // * Powerup methods
    public void TripleShotActive()
    {
        // Activate the powerup
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        // Activate the powerup
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }

    public void ShieldActive()
    {
        // Activate the powerup
        _isShieldActive = true;
        // Activate the shield visual
        _shieldVisualizer.SetActive(true);
    }

    // add method to add 10 to score
    // Communicate with the UI Manager to update the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }



}
