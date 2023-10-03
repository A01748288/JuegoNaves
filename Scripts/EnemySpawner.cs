using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que quieres generar.
    public float spawnInterval = 3.0f; // Intervalo de generación de enemigos en segundos.
    public float enemyLifetime = 5.0f; // Tiempo de vida del enemigo en segundos.

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) // Esto generará enemigos indefinidamente.
        {
            // Obtiene la posición del jugador y ajusta la posición de generación arriba de él.
            Vector3 spawnPosition = player.position + Vector3.up * 6.0f; 

            SpawnEnemy(spawnPosition); // Genera un nuevo enemigo arriba del jugador.
            yield return new WaitForSeconds(spawnInterval); // Espera el tiempo especificado antes de generar el siguiente.
        }
    }

    private void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Destruye el enemigo después de un tiempo especificado.
        Destroy(enemyInstance, enemyLifetime);
    }
}
