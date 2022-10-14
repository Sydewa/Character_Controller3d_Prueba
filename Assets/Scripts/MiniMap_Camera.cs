using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_Camera : MonoBehaviour
{
    public Transform player;

   
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);

        transform.rotation = Quaternion.Euler(90, 0, player.eulerAngles.y);
    }
}
