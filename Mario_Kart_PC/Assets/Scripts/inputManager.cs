using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    internal enum driver
    {
        AI,
        keyboard,
        mobile
    }
    [SerializeField] driver driveController;
    public float vertical;
    public float horizontal;
    public bool handbrake;


    private void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            driveController = driver.keyboard;
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            driveController = driver.mobile;
        }
        else
        {
            driveController = driver.AI;
        }
    }

    private void FixedUpdate()
    {

        switch (driveController)
        {
            case driver.AI:
                break;
            case driver.keyboard: keyboardDrive();
                break;
            case driver.mobile: mobile();
                break;
        }
        
    }
    //=========================================================================================================================================
    // Controles de PC
    //=========================================================================================================================================
    private void keyboardDrive()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handbrake = (Input.GetAxis("Jump") != 0) ? true : false;
    }
    //=========================================================================================================================================
    // Controles de mobil
    //=========================================================================================================================================
    private void mobile()
    {
        horizontal = Input.acceleration.x;
    }

    public void accelerationDown()
    {
        vertical = 1;
    }
    public void accelerationUp()
    {
        vertical = 0;
    }

    public void brakeDown()
    {
        vertical = -1;
    }
    public void brakeUp()
    {
        vertical = 0;
    }

}
