using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        this.transform.position = target.position + offset;
    }
}
