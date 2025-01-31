using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * 30 *  Time.deltaTime);
    }
}
