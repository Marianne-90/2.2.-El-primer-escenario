using UnityEngine;
using System.Collections.Generic;

public class ControladorNubes : MonoBehaviour
{
    [Header("Configuración de Nubes")]
    public GameObject prefabNube; // Un objeto base para clonar
    public Sprite[] spritesNubes; // Aquí arrastraremos tus 3 imágenes
    
    [Header("Ajustes de Movimiento")]
    public float velocidadMin = 1f;
    public float velocidadMax = 3f;
    
    [Header("Ajustes de Generación")]
    public float tiempoEntreNubesMin = 2f;
    public float tiempoEntreNubesMax = 6f;
    
    [Header("Límites de Pantalla (Escena)")]
    // Estos valores dependen de tu cámara. Ajustalos en el inspector.
    public float puntoAparicionX = -15f; // Fuera de pantalla a la izquierda
    public float puntoDesaparicionX = 15f; // Fuera de pantalla a la derecha
    public float limiteYMin = -4f;
    public float limiteYMax = 5f;

    private void Start()
    {
        // Empezamos a generar nubes
        Invoke("GenerarNuevaNube", Random.Range(tiempoEntreNubesMin, tiempoEntreNubesMax));
    }

void GenerarNuevaNube()
    {
        // 1. Crear el objeto
        GameObject nuevaNube = Instantiate(prefabNube);
        
        // --- DESACTIVAR RAYCAST (Para que no bloquee los clics/hover del ratón) ---
        if (nuevaNube.TryGetComponent<UnityEngine.UI.Graphic>(out var graphic))
        {
            graphic.raycastTarget = false;
        }
        // -----------------------------------------------------------------------

        // 2. Asignar un sprite aleatorio de tus 3 nubes
        if (spritesNubes.Length > 0)
        {
            Sprite spriteElegido = spritesNubes[Random.Range(0, spritesNubes.Length)];
            nuevaNube.GetComponent<SpriteRenderer>().sprite = spriteElegido;
        }

        // 3. Posicionar aleatoriamente en el borde izquierdo (X fijo, Y aleatorio)
        float spawnY = Random.Range(limiteYMin, limiteYMax);
        nuevaNube.transform.position = new Vector3(puntoAparicionX, spawnY, 0f);

        // 4. Asignar velocidad aleatoria y los límites al script de la nube
        float velocidad = Random.Range(velocidadMin, velocidadMax);
        MoverNube scriptMovimiento = nuevaNube.GetComponent<MoverNube>();
        scriptMovimiento.Configurar(velocidad, puntoDesaparicionX);

        // 5. Programar la siguiente nube
        Invoke("GenerarNuevaNube", Random.Range(tiempoEntreNubesMin, tiempoEntreNubesMax));
    }
}
