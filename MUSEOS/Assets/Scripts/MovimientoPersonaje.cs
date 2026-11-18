using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadRotacion;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform transformPersonaje;
    [SerializeField] private Camera camaraPersonaje;
    private Vector3 movimiento;
    private float rotacionX;

    void Update()
    {
        MovimientoDelPersonaje();
        MovimientoDeLaCamara();
    }

    void MovimientoDelPersonaje()
    {
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");
        movimiento = transform.right * movX + transform.forward * movZ;
        characterController.SimpleMove(movimiento * velocidadMovimiento);
    }

    void MovimientoDeLaCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * velocidadRotacion;
        float mouseY = Input.GetAxis("Mouse Y") * velocidadRotacion;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        camaraPersonaje.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transformPersonaje.Rotate(Vector3.up * mouseX);
    }
}
