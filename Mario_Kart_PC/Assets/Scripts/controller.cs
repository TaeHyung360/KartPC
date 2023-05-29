using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class controller : MonoBehaviour
{
    private PhotonView photonView;

    internal enum driveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }
    [SerializeField]private driveType drive;
    [Header("Variables")]
    private inputManager IM;
    private GameObject wheelMeshes, wheelColliders;
    private WheelCollider[] wheels = new WheelCollider[4];
    private GameObject[] wheelMesh = new GameObject[4];
    private GameObject centeOfMass;
    private Rigidbody rigidbody;
    public float KPH;
    public float breakPower;
    public float radius = 6;
    public float downForceValue = 50;
    public float torque = 200;
    public float steeringMax = 4;
    [Header("Debug")]
    public float[] slip = new float[4];
    void Start()
    {
        getObjects();
        photonView = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        addDownForce();
        animateWheels();
        if(photonView.IsMine)
        {
            moveVehicle();
            streerVehicle();
        }
        else
        {
            photonView.gameObject.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
        getFriction();
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

        KPH = rigidbody.velocity.magnitude * 3.6f;

        if (IM.handbrake)
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = breakPower;
        }
        else
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = 0;
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
       rigidbody = GetComponent<Rigidbody>();
        
       wheelColliders = GameObject.Find("WheelsColliders");

       wheelMeshes = GameObject.Find("WheelsMeshes");

       wheels[0] = wheelColliders.transform.Find("0").gameObject.GetComponent<WheelCollider>();
       wheels[1] = wheelColliders.transform.Find("1").gameObject.GetComponent<WheelCollider>();
       wheels[2] = wheelColliders.transform.Find("2").gameObject.GetComponent<WheelCollider>();
       wheels[3] = wheelColliders.transform.Find("3").gameObject.GetComponent<WheelCollider>();

       
       wheelMesh[0] = wheelMeshes.transform.Find("0").gameObject;
       wheelMesh[1] = wheelMeshes.transform.Find("1").gameObject;
       wheelMesh[2] = wheelMeshes.transform.Find("2").gameObject;
       wheelMesh[3] = wheelMeshes.transform.Find("3").gameObject;


       centeOfMass = GameObject.Find("Masa");
       rigidbody.centerOfMass = centeOfMass.transform.localPosition;
    }

    private void addDownForce()
    {
        rigidbody.AddForce(-transform.up * downForceValue * rigidbody.velocity.magnitude);
    }

    private void getFriction()
    {
        for(int i = 0; i < wheels.Length; i++)
        {
            WheelHit whellHit;

            wheels[i].GetGroundHit(out whellHit);

            slip[i] = whellHit.forwardSlip;
        }
    }
}
