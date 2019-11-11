using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public Rigidbody playerRb;

    private Animator playerAnim;

    public GameObject ammoPrefab;
    private GameObject focalPoint;

    public GameManager gameManager;

    public AudioSource playerAudio;
    public AudioClip fireSound;
    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip collisionSound;
    public AudioClip pickupSound;

    public bool isOnGround = true;
    public float jumpForce;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        focalPoint = GameObject.Find("Focal Point");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            float forwardInput = Input.GetAxis("Vertical");
            playerAnim.SetFloat("Speed_f", speed);
            transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);
            focalPoint.transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.X) && gameManager.ammoLeft>0)
            {
                Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);
                Instantiate(ammoPrefab, pos, transform.rotation);
                playerAudio.PlayOneShot(fireSound,1.0f);
                gameManager.updateAmmo(-1);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                isOnGround = false;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
            if (transform.position.y <= -5)
            {
                playerAnim.SetBool("Death_b", true);
                gameManager.GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.isGameActive)
        {
            if (other.gameObject.CompareTag("Bad Ammo"))
            {
                Debug.Log("Hit by a bullet!");
                if (gameManager.isBossActive) gameManager.updateScore(-3);
                else gameManager.updateScore(-1);
                Destroy(other.gameObject);
                playerAudio.PlayOneShot(collisionSound,1.0f);
                if (gameManager.health <= 0)
                {
                    gameManager.GameOver();
                    playerAnim.SetBool("Death_b", true);
                }
            }
            if (other.gameObject.CompareTag("Health"))
            {
                Debug.Log("Health found!");
                if (true)
                {
                    playerAudio.PlayOneShot(pickupSound,1.0f);
                    gameManager.updateScore(1);
                    Destroy(other.gameObject);
                }
            }
            if(other.gameObject.CompareTag("AmmoRound"))
            {
                gameManager.updateAmmo(gameManager.level);
                playerAudio.PlayOneShot(pickupSound, 1.0f);
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Island1"))
            {
                isOnGround = true;
            }
        }
        
    }
}
