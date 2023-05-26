using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject Player ;
    public GameObject chlild;
    private GameObject cameralookAt;
    public float speed;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        chlild = Player.transform.Find("Camera constaint").gameObject;
        cameralookAt = Player.transform.Find("Camera lookAt").gameObject;
    }
    private void FixedUpdate()
    {
        follow();
    }

    private void follow()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position,chlild.transform.position,Time.deltaTime * speed);
        gameObject.transform.LookAt(cameralookAt.gameObject.transform.position);
    }

}
