using UnityEngine;
using UnityEngine.EventSystems;

public class BotonInteractivo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Configuración de Escala (Hover)")]
    [SerializeField] private Vector3 escalaHover = new Vector3(1.1f, 1.1f, 1f);
    private Vector3 escalaOriginal;

    [Header("Configuración de Cursores")]
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorPointer;
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    private bool mouseEncima = false;

    private void Awake()
    {
        escalaOriginal = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEncima = true;
        transform.localScale = escalaHover;

        if (cursorPointer != null)
        {
            Cursor.SetCursor(cursorPointer, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RestaurarEstado();
    }

    private void OnDisable()
    {
        RestaurarEstado();
    }

    private void RestaurarEstado()
    {
        mouseEncima = false;
        transform.localScale = escalaOriginal;

        // Si tienes asignado un cursor normal personalizado lo usa, de lo contrario usa el del sistema (null)
        Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
    }
}
