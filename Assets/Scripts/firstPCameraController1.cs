using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPCameraController1 : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 0.5f, 0.5f), Time.deltaTime * 100);
    }
}
