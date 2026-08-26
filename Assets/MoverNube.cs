using UnityEngine;

public class MoverNube : MonoBehaviour
{
    private float velocidad;
    private float limiteXDestruccion;
    private bool configurada = false;

    // Esta es la función que daba el error. 
    // Debe ser 'public' para que ControladorNubes pueda verla.
    public void Configurar(float vel, float limiteX)
    {
        velocidad = vel;
        limiteXDestruccion = limiteX;
        configurada = true;
    }

    private void Update()
    {
        if (!configurada) return;

        // Mover hacia la derecha
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);

        // Destruir si cruza el límite derecho
        if (transform.position.x > limiteXDestruccion)
        {
            Destroy(gameObject);
        }
    }
}
