using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;
    public float speedY = 2f;
    [Header("Limites")]
    public float limiteX = 4f;

    [Header("Disparo")]
    public GameObject prefabDisparo;
    public float disparoSpeed = 2f;
    public float timeDisparoDestroy = 2f;

    public Transform weapon1;
    public Transform weapon2;

    public int playerHealth = 3; // Vida inicial del jugador.

    private bool isFire = false;
    private Rigidbody2D rb;
    private bool isGameOver = false;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isGameOver)
        {
            MovePlayer();
            StartFire();

            if (playerHealth <= 0)
            {
                GameOver();
            }
        }
    }

    public void MovePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, speedY);
        if (transform.position.x > limiteX)
        {
            transform.position = new Vector2(limiteX, transform.position.y);
        }
        else if (transform.position.x < -limiteX)
        {
            transform.position = new Vector2(-limiteX, transform.position.y);
        }
    }

    public void StartFire()
    {
        if (Input.GetAxis("Fire1") == 1f)
        {
            if (!isFire)
            {
                isFire = true;
                GameObject disparoInstance = Instantiate(prefabDisparo);
                disparoInstance.transform.SetParent(transform.parent);

                disparoInstance.transform.position = weapon1.position;
                disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance, timeDisparoDestroy);

                GameObject disparoInstance2 = Instantiate(prefabDisparo);
                disparoInstance2.transform.SetParent(transform.parent);
                disparoInstance2.transform.position = weapon2.position;
                disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance2, timeDisparoDestroy);
            }
            else
            {
                isFire = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver && collision.gameObject.CompareTag("disparoEnemigo"))
        {
            playerHealth--; // Resta vida al jugador.
            Destroy(collision.gameObject);

            if (playerHealth <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0; // Detiene el tiempo del juego.

    }
}
