using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public Vector3 CurrentPosition { get; private set; }

    private void Update()
    {
        // Obtén la posición actual del jugador y almacénala en la variable CurrentPosition.
        CurrentPosition = transform.position;
    }
}
