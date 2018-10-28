using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class cDriver0 : MonoBehaviour
{
    public float maxTorque = 450f;

    public Transform centerOfMass;

    public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;

    public Transform tireMeshFL;
	public Transform tireMeshFR;
	public Transform tireMeshRL;
	public Transform tireMeshRR;

    private Rigidbody m_rigidBody;

	public float speed;
	public float maxSpeed;
	public bool canDrive;

	// CONTROLFREAK
	/*
	public	TouchController	ctrl;
	
	public const int STICK_WALK	= 0;
	public const int STICK_FIRE	= 1;
	*/
	private float h;
	private float v;
	private float h2;
	private float v2;

	// tyres n wheelcoll stiffness
	public WheelHit hit;
	public WheelFrictionCurve wfc;
	public WheelFrictionCurve wfc2;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = centerOfMass.localPosition;
    }


    void Update()
    {
        UpdateMeshesPositions();

    }


    void FixedUpdate()
    {
		if(canDrive){

			//CONTROL FREAK
		/*
		if (this.ctrl != null)
		{
			TouchStick	walkStick	= this.ctrl.GetStick(STICK_WALK);
			TouchStick	fireStick	= this.ctrl.GetStick(STICK_FIRE);

			//if (walkStick.Pressed())
			//{
				Vector2	walkVec	= walkStick.GetVec();
				h = walkVec.x;
				v = walkVec.y;		
			//}
			//if (fireStick.Pressed())
			//{
				Vector2	fireVec	= fireStick.GetVec();
				h2 = fireVec.x;
				v2 = fireVec.y;
			//}

		}
		*/
			//PC CONTROL
        h2 = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");


		//speed = GetComponent<Rigidbody>().velocity.magnitude; 
		speed = m_rigidBody.velocity.magnitude; 
		speed = speed*3.6f;




				if(speed > maxSpeed) v = v/100;


// VEHICLE MOTOR
		// ----------------------
		float finalAngle = h2 * 45f;
		finalAngle = Mathf.Clamp(finalAngle,-60,60);

		wheelFL.steerAngle = finalAngle;
		wheelFR.steerAngle = finalAngle;

		wheelFL.motorTorque = v * maxTorque;
		wheelFR.motorTorque = v * maxTorque;
		wheelRL.motorTorque = v * maxTorque;
		wheelRR.motorTorque = v * maxTorque;

		}
    }

    void UpdateMeshesPositions()
    {

            Quaternion quat;
            Vector3 pos;

			wheelFL.GetWorldPose(out pos, out quat);
		tireMeshFL.position = pos;
		tireMeshFL.rotation = quat;
			wheelFR.GetWorldPose(out pos, out quat);
		tireMeshFR.position = pos;
		tireMeshFR.rotation = quat;
			wheelRL.GetWorldPose(out pos, out quat);
		tireMeshRL.position = pos;
		tireMeshRL.rotation = quat;
			wheelRR.GetWorldPose(out pos, out quat);
		tireMeshRR.position = pos;
		tireMeshRR.rotation = quat;



    }

}
