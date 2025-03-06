using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 4f;

    private Player _player;

    // handle to animator component
    private Animator _anim;

    [SerializeField]
    private AudioSource _audioSource;



    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player or UI is NULL");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        // if bottom of screen
        // respawn at top of screen with a new random x position
        // * Need to have a float here so it goes to -5 not stopping at -4.
        if (transform.position.y < -5f)
        {
            // * Need to have these as floats or it will only go to 7 not 8
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // * Best Practice
            // This is how you get the player component from the other object
            Player player = other.transform.GetComponent<Player>();
            // Now we can null check the variable we made holding the player component and see if it exists before we call the Player Damage method.
            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            // add 10 to score
            if (_player != null)
            {
                _player.AddScore(10);
            }
            // trigger the anim
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

    }


}
