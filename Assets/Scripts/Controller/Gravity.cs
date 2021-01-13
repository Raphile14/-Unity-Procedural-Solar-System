using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    const float G = 6.674f;
    public static List<Gravity> Bodies;
    public Rigidbody rb;

    private void FixedUpdate()
    {        
        foreach (Gravity body in Bodies)
        {
            if (body != this)
            {
                Attract(body);
            }            
        }
    }

    void OnEnable()
    {
        if (Bodies == null)
        {
            Bodies = new List<Gravity>();
        }
        Bodies.Add(this);
    }

    private void OnDisable()
    {
        Bodies.Remove(this);
    }

    void Attract(Gravity objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0f) return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2) / 10000000;
        Debug.Log(forceMagnitude);
        Vector3 force = direction.normalized * forceMagnitude;
        
        rbToAttract.AddForce(force);
    }
}
