using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadRotacion;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform transformPersonaje;
    [SerializeField] private Camera camaraPersonaje;
    private Vector3 movimiento;
    private float rotacionX;
    private InputAction accionMovimiento;
    private InputAction accionMirada;

    void Awake()
    {
        InputActionAsset acciones = InputSystem.actions;
        if (acciones == null)
        {
            Debug.LogError("No hay un Input Action Asset configurado para el proyecto.", this);
            enabled = false;
            return;
        }

        accionMovimiento = acciones.FindAction("Player/Move");
        accionMirada = acciones.FindAction("Player/Look");
        if (accionMovimiento == null || accionMirada == null)
        {
            Debug.LogError("Faltan las acciones Player/Move o Player/Look en el Input Action Asset.", this);
            enabled = false;
        }
    }

    void Update()
    {
        MovimientoDelPersonaje();
        MovimientoDeLaCamara();
    }

    void MovimientoDelPersonaje()
    {
        Vector2 entrada = accionMovimiento.ReadValue<Vector2>();
        movimiento = transform.right * entrada.x + transform.forward * entrada.y;
        characterController.SimpleMove(movimiento * velocidadMovimiento);
    }

    void MovimientoDeLaCamara()
    {
        Vector2 entrada = accionMirada.ReadValue<Vector2>();
        float escala = accionMirada.activeControl != null &&
            accionMirada.activeControl.device is Mouse
            ? 0.1f
            : Time.deltaTime;
        float mouseX = entrada.x * velocidadRotacion * escala;
        float mouseY = entrada.y * velocidadRotacion * escala;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        camaraPersonaje.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transformPersonaje.Rotate(Vector3.up * mouseX);
    }
}
