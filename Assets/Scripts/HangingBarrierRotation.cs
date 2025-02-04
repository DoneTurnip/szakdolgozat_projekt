using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingBarrierRotation : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        float z = Mathf.PingPong(Time.time * speed * 6, 1) * 100 - 50;

        transform.rotation = Quaternion.Euler(0, 3.69f,z);
    }
}
