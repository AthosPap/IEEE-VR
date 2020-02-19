using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Toy_Moving : MonoBehaviour
{
    private float speed = 0.45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.forward;
    }
}
