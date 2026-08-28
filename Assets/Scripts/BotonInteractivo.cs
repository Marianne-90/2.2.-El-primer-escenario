using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BotonInteractivo : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Configuración de Cursores")]
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorPointer;
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    [Header("Animación")]
    [SerializeField] private Animator animator;

    [Header("Acción al terminar la animación")]
    [SerializeField] private UnityEvent accionAlTerminar;

    private bool procesandoClick = false;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (procesandoClick)
            return;

        if (cursorPointer != null)
        {
            Cursor.SetCursor(cursorPointer, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!procesandoClick)
        {
            RestaurarCursor();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (procesandoClick)
            return;

        procesandoClick = true;

        // Vuelve al cursor normal mientras se reproduce la animación
        RestaurarCursor();

        if (animator != null)
        {
            animator.SetTrigger("Pulsar");
        }
        else
        {
            // Si por algún motivo no hay Animator,
            // ejecuta igualmente la acción
            EjecutarAccion();
        }
    }

    // Esta función la llama el Animation Event
    // colocado al final de la animación
    public void EjecutarAccion()
    {
        accionAlTerminar?.Invoke();
        procesandoClick = false;
    }

    private void OnDisable()
    {
        procesandoClick = false;
        RestaurarCursor();
    }

    private void RestaurarCursor()
    {
        Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
    }
}
