using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody bossRb;

    private GameObject player;
    public GameObject ammoPrefab;

    private GameManager gameManager;
    public Animator playerAnim;

    public AudioSource playerAudio;
    public AudioClip fireSound;
    public AudioClip collisionSound;
    public AudioClip dieSound;

    public float startDelay = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bossRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        InvokeRepeating("FireAmmo", startDelay, gameManager.bossFireRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        playerAnim.SetFloat("Speed_f", gameManager.bossSpeed);
        transform.Translate(Vector3.forward * gameManager.bossSpeed * Time.deltaTime);
    }

    private void FireAmmo()
    {
        Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);
        playerAudio.PlayOneShot(fireSound, 1.0f);
        Instantiate(ammoPrefab, pos, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Good Ammo"))
        {
            gameManager.updateBossHealth();
            playerAudio.PlayOneShot(collisionSound, 1.0f);
            if (gameManager.bossHealth <= 0)
            {
                Destroy(gameObject);
                gameManager.YouWon();
            }
            Destroy(other.gameObject);
        }
    }
}
