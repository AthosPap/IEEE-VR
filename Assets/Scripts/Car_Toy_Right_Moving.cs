using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Toy_Right_Moving : MonoBehaviour
{
    private float speed = 0.35f;
    private float rand_mult;
    // Start is called before the first frame update
    void Start()
    {
        //Random r = new Random();
        //int rand = r.Next(0, 5);
        rand_mult = Random.value / 4.0f + 1.0f;  //  [1, 1.25]
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += rand_mult * speed * transform.right;
    }
}
