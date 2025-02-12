using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.left * 20 *  Time.deltaTime);
    }
}
