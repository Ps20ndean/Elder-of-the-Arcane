using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if (target) // if the target exists then do the following
        {

            // follows the player around
            transform.position = new Vector3(target.position.x, target.position.y + 2f, transform.position.z);
        }
    }
}
