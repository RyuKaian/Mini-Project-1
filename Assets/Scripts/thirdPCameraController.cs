using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPCameraController : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0, 2, -6), Time.deltaTime * 100);
    }
}
