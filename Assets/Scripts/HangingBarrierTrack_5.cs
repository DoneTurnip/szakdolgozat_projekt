using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingBarrierRotationTrack_5 : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        float z = Mathf.PingPong(Time.time * speed * 2, 1) * 100 - 50;

        transform.localRotation = Quaternion.Euler(0, 90, z);
    }
}

