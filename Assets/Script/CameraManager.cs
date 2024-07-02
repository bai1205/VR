using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
      //  offset = transform.position-target.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            transform.LookAt(target);
            transform.RotateAround(target.transform.position, Vector3.up, 90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.LookAt(target);
            transform.RotateAround(target.transform.position, Vector3.up, -90 * Time.deltaTime);
        }
    }
}
