using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;

    public Animator playerAnim;
    public GameObject ammoPrefab;
    private GameObject player;

    private GameManager gameManager;
    public SpawnManager spawnManager;

    public AudioSource playerAudio;
    public AudioClip fireSound;
    public AudioClip collisionSound;

    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        startDelay = Random.Range(1.0f,3.0f);
        InvokeRepeating("FireAmmo", startDelay, gameManager.fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        playerAnim.SetFloat("Speed_f", gameManager.enemySpeed);
        transform.Translate(Vector3.forward * gameManager.enemySpeed * Time.deltaTime);
    }

    private void FireAmmo()
    {
        Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);
        Instantiate(ammoPrefab,pos,transform.rotation);
        playerAudio.PlayOneShot(fireSound, 1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Good Ammo"))
        {
            gameManager.updateKills();
            playerAudio.PlayOneShot(collisionSound, 1.0f);
            Destroy(gameObject);
            Destroy(other.gameObject);
            int randomIndex = Random.Range(0, 2);
            if(randomIndex==0) spawnManager.SpawnHealth(transform.position);
            else spawnManager.SpawnAmmo(transform.position);
        }
    }
}
