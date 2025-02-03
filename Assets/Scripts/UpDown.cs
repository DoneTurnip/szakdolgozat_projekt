using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed * 6, 1) * 1.5f;
        
        transform.position=new Vector3(0,y,0);
    }
}
