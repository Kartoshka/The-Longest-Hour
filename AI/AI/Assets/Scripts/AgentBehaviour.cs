using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour {

    public GameObject target;
    protected Agent agent;
    
    public virtual void Awake()
    {
        agent = gameObject.GetComponent<Agent>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	public virtual void Update ()
    {
        agent.SetSteering(GetSteering());
    }

    public virtual Steering GetSteering()
    {
        return new Steering();
    }
}
