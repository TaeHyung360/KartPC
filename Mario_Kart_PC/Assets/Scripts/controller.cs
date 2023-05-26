using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField]private driveType drive;

    private inputManager IM;
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public float radius = 6;
    public float torque = 200;
    public float steeringMax = 4;
    // Start is called before the first frame update
    void Start()
    {
        getObjects();
    }

    private void FixedUpdate()
    {
        animateWheels();
        moveVehicle();
        streerVehicle();
    }

    private void moveVehicle()
    {
        float totalPower;

        if(drive == driveType.allWheelDrive)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (torque/4);
            }
        }
        else if(drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (torque / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length -2; i++)
            {
                wheels[i].motorTorque = IM.vertical * (torque / 2);
            }
        }

    }

    private void streerVehicle()
    {
        if(IM.horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius + (1.5 / 2)))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius - (1.5 / 2)))) * IM.horizontal;
        }else if(IM.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius - (1.5 / 2)))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius + (1.5 / 2)))) * IM.horizontal;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
        /*
        for (int i = 0; i < wheels.Length - 2; i++)
        {
            wheels[i].steerAngle = IM.horizontal * steeringMax;
        }
        */
    }


    void animateWheels()
        {
            Vector3 wheelPosition = Vector3.zero;
            Quaternion wheelRotation = Quaternion.identity;

            for (int i = 0; i < 4; i++)
            {
                wheels [i].GetWorldPose(out wheelPosition, out wheelRotation);
                wheelMesh [i].transform.position = wheelPosition;
                wheelMesh [i].transform.rotation = wheelRotation;
            }
        }
    private void getObjects()
    {
       IM = GetComponent<inputManager>();
    }
}
