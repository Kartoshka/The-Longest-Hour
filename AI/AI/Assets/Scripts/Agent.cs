﻿using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {

    public float maxSpeed;
    public float maxAccel;
    public float orientation;
    public float rotation;
    public Vector3 velocity;
    protected Steering steering;
    
    void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
    }

    //uses the new velocity and rotation to change the displacement of the agent and to rotate it
    public virtual void Update()
    {
        Vector3 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;

        if (orientation < 0.0f)
        {
            orientation += 360.0f;
        }
        else if(orientation > 360.0f)
        {
            orientation -= 360.0f;
        }

        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, orientation);
    }

    //updates the velocity and the rotation with the agents steering behaviour
    public virtual void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }

        if(steering.angular == 0.0f)
        {
            rotation = 0.0f;
        }

        if(steering.linear.sqrMagnitude == 0.0f)
        {
            velocity = Vector3.zero;
        }

        steering = new Steering();
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }


}
