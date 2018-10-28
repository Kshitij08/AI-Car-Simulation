using UnityEngine;
using System.Collections;

public class CarEngine : MonoBehaviour {

    public float force = 1;

    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(transform.forward * force);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddTorque(transform.up * -force);
        }
	}
}
