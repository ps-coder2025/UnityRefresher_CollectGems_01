using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 axis = new Vector3(15f, 45f, 30f);
    
    void Update()
    {
        transform.Rotate(axis * Time.deltaTime);
    }
}
