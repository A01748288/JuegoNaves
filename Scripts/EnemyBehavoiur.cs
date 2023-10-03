using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;
    public float moveSpeedX = 2f;

    [Header("Disparo")]
    public GameObject prefabDisparo;
    public float disparoSpeed = 2f;
    public float shootingInterval = 6f;
    public float timeDisparoDestroy = 2f;

    public int enemyHealth = 2; // Vida inicial del enemigo.

    public Transform weapon1;
    public Transform weapon2;

    public float limiteDerecho = 5f; // Definir el límite derecho.
    public float limiteIzquierdo = -5f; // Definir el límite izquierdo.

    private float _shootingTimer;
    private bool isMovingRight = true; // Variable para controlar la dirección del movimiento.
    private bool isShootingPaused = false; // Variable para controlar si el enemigo está en pausa.
    private float pauseDuration = 3.0f; // Duración de la pausa en segundos.
    private float currentPauseTime = 0.0f; // Tiempo transcurrido en la pausa.

    void Start()
    {
        _shootingTimer = Random.Range(0f, shootingInterval);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    void Update()
    {
        if (!isShootingPaused)
        {
            StartFire();
        }
        else
        {
            // Incrementa el tiempo transcurrido en la pausa.
            currentPauseTime += Time.deltaTime;

            // Verifica si la pausa ha terminado.
            if (currentPauseTime >= pauseDuration)
            {
                isShootingPaused = false;
                currentPauseTime = 0.0f; // Reinicia el tiempo de pausa.
            }
        }

        MoveHorizontally();
    }

    public void StartFire()
    {
        _shootingTimer -= Time.deltaTime;

        // Solo dispara si no está en pausa.
        if (!isShootingPaused && _shootingTimer <= 0f)
        {
            _shootingTimer = shootingInterval;

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

            // Pausa el modo de disparo después de recibir un impacto.
            if (enemyHealth <= 0)
            {
                isShootingPaused = true;
                currentPauseTime = 0.0f; // Reinicia el tiempo de pausa.
            }
        }
    }

    private void MoveHorizontally()
    {
        float horizontalMovement = isMovingRight ? moveSpeedX : -moveSpeedX;
        transform.Translate(new Vector3(horizontalMovement * Time.deltaTime, 0, 0));

        if (transform.position.x >= limiteDerecho)
        {
            isMovingRight = false;
        }
        else if (transform.position.x <= limiteIzquierdo)
        {
            isMovingRight = true;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("disparoPlayer"))
        {
            enemyHealth--; // Resta vida al enemigo.
            Destroy(otherCollider.gameObject);

            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
