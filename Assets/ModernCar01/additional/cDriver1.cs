using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class cDriver1 : MonoBehaviour
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

	// windows
	public Transform winL;
	public Transform winR;

	public float ydown;//move -y
	public float xside;//move x
	public float zside;//move z
	public int movesq = 100;
	private int wlmove;
	private int wrmove;
	private bool lmovedown;
	private bool lmoveup;
	private bool lmovestop;
	private bool rmovedown;
	private bool rmoveup;
	private bool rmovestop;

	// doors
	public Transform doorL;
	public Transform doorR;
	private bool ldooropen;
	private bool ldoorclose;
	private bool rdooropen;
	private bool rdoorclose;
	public float rotamax = 45;
	public int rotq = 100;
	private float ldmove;
	private float rdmove;
	public bool LDnegative;//if door negative rotation
	private int ldnr;
	public bool RDnegative;//if door negative rotation
	private int rdnr;

	//steeringwheel 
	public Transform steeringjoint;
	//public SteeringWheel steeringwheel;//needs Mobile Touch Steering Wheel [UI/NGUI]
	public float steerInput;
	public float steer;
	public float steerq = 2.5f;

	//rearLights
	public GameObject rLight0;
	public GameObject rLight1;

	//skid smoke
	public GameObject slipprefab;

	public Text guiSteerAngle;


    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = centerOfMass.localPosition;

		if(LDnegative) ldnr = -1;
		if(!LDnegative) ldnr = 1;
		if(RDnegative) rdnr = -1;
		if(!RDnegative) rdnr = 1;
	}


    void Update()
    {
        UpdateMeshesPositions();

		if(guiSteerAngle) guiSteerAngle.text = ":: " + wheelFR.steerAngle;
		//if(steeringjoint) steeringjoint.Rotate(0, 0, -wheelFR.steerAngle*0.01f);
		if(steeringjoint) steeringjoint.localRotation =  Quaternion.Euler(15, 0, -wheelFR.steerAngle);
		//WINDOW
		if ( Input.GetKeyDown( KeyCode.X ) ) {
			lmovedown = true;
			lmoveup = false;
		}
		if ( Input.GetKeyDown( KeyCode.C ) ) {
			lmoveup = true;
			lmovedown = false;
		}
		if ( Input.GetKeyDown( KeyCode.V ) ) {
			if(lmovestop){
			   lmovestop = false;
			}else{
			   lmovestop = true;
			   Invoke("UndoLWinStop",1);
			}
		}
		if ( Input.GetKeyDown( KeyCode.Y ) ) {
			rmovedown = true;
			rmoveup = false;
		}
		if ( Input.GetKeyDown( KeyCode.U ) ) {
			rmoveup = true;
			rmovedown = false;
		}
		if ( Input.GetKeyDown( KeyCode.I ) ) {
			if(rmovestop){
				rmovestop = false;
			}else{
				rmovestop = true;
				Invoke("UndoRWinStop",1);
			}
		}

		if(winL){
			if (lmovedown && !lmovestop) {
				
				if(wlmove>-movesq){
					if(!LDnegative) winL.Translate(xside/movesq, -ydown/movesq, -zside/movesq);	
					if(LDnegative) winL.Translate(-xside/movesq, ydown/movesq, zside/movesq);	
					wlmove -= 1;
				}
			}
			
			if (lmoveup && !lmovestop) {
				
				if(wlmove<0){
					if(!LDnegative) winL.Translate(-xside/movesq, ydown/movesq, zside/movesq);
					if(LDnegative) winL.Translate(xside/movesq, -ydown/movesq, -zside/movesq);
					wlmove += 1;
				}
			}
			
		}
		
		if(winR){
			if (rmovedown && !rmovestop) {
				
				if(wrmove>-movesq){
					if(!RDnegative) winR.Translate(-xside/movesq, -ydown/movesq, -zside/movesq);
					if(RDnegative) winR.Translate(xside/movesq, ydown/movesq, zside/movesq);
					wrmove -= 1;
				}
			}
			
			if (rmoveup && !rmovestop) {
				if(wrmove<0){
					if(!RDnegative) winR.Translate(xside/movesq, ydown/movesq, zside/movesq);	
					if(RDnegative) winR.Translate(-xside/movesq, -ydown/movesq, -zside/movesq);
					wrmove += 1;
				}
			}
			
		}

		//DOOR
		if(doorL){
			if ( Input.GetKeyDown( KeyCode.N ) ) {
				ldooropen = true;
				ldoorclose = false;
			}
			if ( Input.GetKeyDown( KeyCode.M ) ) {
				ldooropen = false;
				ldoorclose = true;
			}
			
			if(ldooropen){
				if(!LDnegative){
					if(ldmove<rotamax){
						doorL.Rotate(0,ldnr*rotamax/rotq,0);
						ldmove += ldnr*rotamax/rotq;
					}
				}
				if(LDnegative){
					if(ldmove>-rotamax){
						doorL.Rotate(0,ldnr*rotamax/rotq,0);
						ldmove += ldnr*rotamax/rotq;
					}
				}
			}
			if(ldoorclose){
				if(!LDnegative){
					if(ldmove>0){
						doorL.Rotate(0,-ldnr*rotamax/rotq,0);
						ldmove -= ldnr*rotamax/rotq;
					}
				}
				
				if(LDnegative){
					if(ldmove<0){
						doorL.Rotate(0,-ldnr*rotamax/rotq,0);
						ldmove -= ldnr*rotamax/rotq;
					}
				}
				
			}
		}
		
		if(doorR){
			if ( Input.GetKeyDown( KeyCode.K ) ) {
				rdooropen = true;
				rdoorclose = false;
			}
			if ( Input.GetKeyDown( KeyCode.L ) ) {
				rdooropen = false;
				rdoorclose = true;
			}
			
			if(rdooropen){
				if(!RDnegative){
					if(rdmove<rotamax){
						doorR.Rotate(0,-rdnr*rotamax/rotq,0);
						rdmove += rdnr*rotamax/rotq;
					}
				}
				if(RDnegative){
					if(rdmove>-rotamax){
						doorR.Rotate(0,-rdnr*rotamax/rotq,0);
						rdmove += rdnr*rotamax/rotq;
					}
				}
			}
			if(rdoorclose){
				if(!RDnegative){
					if(rdmove>0){
						doorR.Rotate(0,rdnr*rotamax/rotq,0);
						rdmove -= rdnr*rotamax/rotq;
					}
				}
				
				if(RDnegative){
					if(rdmove<0){
						doorR.Rotate(0,rdnr*rotamax/rotq,0);
						rdmove -= rdnr*rotamax/rotq;
					}
				}
				
			}
		}
		
	}


    void FixedUpdate()
    {
		speed = m_rigidBody.velocity.magnitude; 
		speed = speed*3.6f;

		if(rLight0 && rLight1){
			if(speed>0 && rLight1.activeSelf == true) {
				rLight0.SetActive(true);
				rLight1.SetActive(false);
			}
		}

		//skidmarks
		WheelHit wheelRLHit;
		wheelRL.GetGroundHit(out wheelRLHit);
		//print(BackLeftWheelGroundHit.sidewaysSlip);
		if(speed>10 && (wheelRLHit.sidewaysSlip > 0.02 || wheelRLHit.sidewaysSlip < -0.02)){
			if (slipprefab) Instantiate( slipprefab, wheelRLHit.point, Quaternion.identity );
		}
		WheelHit wheelRRHit;
		wheelRR.GetGroundHit(out wheelRRHit);
		//print(BackRightWheelGroundHit.sidewaysSlip);
		if(speed>10 && (wheelRRHit.sidewaysSlip > 0.02 || wheelRRHit.sidewaysSlip < -0.02)){
			if (slipprefab) Instantiate( slipprefab, wheelRRHit.point, Quaternion.identity );
		}



		//forceapppointdistance
		if ( Input.GetKeyDown( KeyCode.O ) ) {
			wheelFL.forceAppPointDistance += 0.1f;
			wheelFR.forceAppPointDistance += 0.1f;
			wheelRL.forceAppPointDistance += 0.1f;
			wheelRR.forceAppPointDistance += 0.1f;
			print("wfapd: " + wheelFL.forceAppPointDistance);
		}
		if ( Input.GetKeyDown( KeyCode.P ) ) {
			wheelFL.forceAppPointDistance -= 0.1f;
			wheelFR.forceAppPointDistance -= 0.1f;
			wheelRL.forceAppPointDistance -= 0.1f;
			wheelRR.forceAppPointDistance -= 0.1f;
			print("wfapd: " + wheelFL.forceAppPointDistance);
		}		



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
		steer = h2;

			//STEERINGWHEEL
			/*
			if(steeringwheel){
			steerInput = steeringwheel.GetInput();
			steer = steerInput * steerq;
			h2 = steer;
			}
			*/

			//STEERINGWHEEL OBJECT
			//if(steeringjoint) steeringjoint.localRotation = Quaternion.AngleAxis(-steer*30, Vector3.forward);
			//if(steeringjoint) steeringjoint.Rotate(0, 0, h2*1f);
			//
				if(speed > maxSpeed) v = v/100;

			if(v<0) {
				if(rLight0 && rLight1){
					rLight1.SetActive(true);
					rLight0.SetActive(false);
				}
			}

			//THRUST FORCE
			//if(v>0.5f) m_rigidBody.AddForce(transform.forward.normalized *10500);


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

	void UndoLWinStop()
	{
		lmovestop = false;
		lmoveup = false;
		lmovedown = false;
	}

	void UndoRWinStop()
	{
		rmovestop = false;
		rmoveup = false;
		rmovedown = false;
	}

	void SwitchOnDrive()
	{
		canDrive = true;
		
	}
	
	void SwitchOffDrive()
	{
		canDrive = false;
		
	}

}
