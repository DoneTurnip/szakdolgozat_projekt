using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.left * 30 *  Time.deltaTime);
    }
}
