using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {

    public float maxSpeed;
    public float maxAccel;
    public float rotation;
    public Vector3 velocity;
    protected Steering steering;
    protected Rigidbody m_rigidBody;
    
    void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    //uses the new velocity and rotation to change the displacement of the agent and to rotate it
    public void Update()
    {
        Vector3 displacement = velocity * Time.deltaTime;

        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
    }

    //updates the velocity and the rotation with the agents steering behaviour
    public void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
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
