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


    private void FixedUpdate()
    {
        switch (driveController)
        {
            case driver.AI:
                break;
            case driver.keyboard: keyboardDrive();
                break;
            case driver.mobile:
                break;
        }
        
    }

    private void keyboardDrive()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handbrake = (Input.GetAxis("Jump") != 0) ? true : false;
    }
}
