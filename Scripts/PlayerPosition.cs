using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public Vector3 CurrentPosition { get; private set; }

    private void Update()
    {
        // Obt�n la posici�n actual del jugador y almac�nala en la variable CurrentPosition.
        CurrentPosition = transform.position;
    }
}
