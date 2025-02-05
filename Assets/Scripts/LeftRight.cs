using UnityEngine;

public class LeftRight : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        float x = Mathf.PingPong(Time.time * speed * 6, 1) * 3 - 1.5f;

        transform.localPosition = new Vector3(x, 1.5f, transform.localPosition.z);
    }
}
