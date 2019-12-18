using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if (target)
        {
            transform.position = new Vector3(target.position.x, target.position.y + 3.25f, transform.position.z);
        }
    }
}
