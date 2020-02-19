using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColisions : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody m_Rigidbody;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();    
        Physics.IgnoreLayerCollision(11, 12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
