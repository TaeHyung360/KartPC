using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;
    public WheelCollider[] wheelColliders;

    private Rigidbody rb;

    public Transform[] frontWheelMeshes;

    void Start()
    {
        // Obtiene el componente Rigidbody
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical"); // W,S
        float moveHorizontal = Input.GetAxis("Horizontal"); // A,D

        // Calcula la velocidad y la rotación
        float moveSpeed = moveVertical * speed * Time.deltaTime;
        float turnSpeed = moveHorizontal * this.turnSpeed * Time.deltaTime;

        // Aplica la velocidad y la rotación al Rigidbody
        rb.AddRelativeForce(0, 0, moveSpeed);
        rb.AddRelativeTorque(0, turnSpeed, 0);

        // Control de las ruedas
        foreach (WheelCollider wheel in wheelColliders)
        {
            // Si la rueda es una rueda delantera, permite el giro
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = moveHorizontal * 30;

            // Aplica fuerza a todas las ruedas
            if (moveVertical > 0)
            {
                wheel.motorTorque = moveVertical * speed; // adelante
                wheel.brakeTorque = 0; // elimina la fuerza de frenado si estamos yendo hacia adelante
            }
            else
            {
                wheel.motorTorque = 0; // no aplica fuerza motriz si queremos ir hacia atrás
                wheel.brakeTorque = -moveVertical * speed; // hacia atrás
            }
        }
        // Control de las ruedas
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            WheelCollider wheel = wheelColliders[i];

            // Si la rueda es una rueda delantera, permite el giro
            if (wheel.transform.localPosition.z > 0)
            {
                wheel.steerAngle = Mathf.Clamp(moveHorizontal * 30, -30, 30);  // limita el ángulo de dirección a 30 grados hacia la izquierda o hacia la derecha

                // Encuentra la malla de la rueda correspondiente y rótala para que coincida con el ángulo de dirección
                Transform wheelMesh = frontWheelMeshes[i];

                // Calcula el ángulo objetivo basado en el ángulo de dirección del WheelCollider
                float targetAngle = wheel.steerAngle;

                // Limita el ángulo objetivo a un rango de -30 a 30 grados
                targetAngle = Mathf.Clamp(targetAngle, -30, 30);

                // Crea un Quaternion basado en el ángulo objetivo
                Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

                // Aplica la rotación objetivo a la malla de la rueda
                wheelMesh.localRotation = targetRotation;
            }

            // Aplica fuerza a todas las ruedas
            if (moveVertical > 0)
            {
                wheel.motorTorque = moveVertical * speed; // adelante
                wheel.brakeTorque = 0; // elimina la fuerza de frenado si estamos yendo hacia adelante
            }
            else
            {
                wheel.motorTorque = 0; // no aplica fuerza motriz si queremos ir hacia atrás
                wheel.brakeTorque = -moveVertical * speed; // hacia atrás
            }
        }
    }
}