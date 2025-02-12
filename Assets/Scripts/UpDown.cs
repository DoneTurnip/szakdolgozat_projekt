using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed * 2, 1) * 1.5f;
        
        transform.localPosition=new Vector3(transform.localPosition.x,y,transform.localPosition.z);
    }
}
