#pragma strict

var capsule : GameObject;
var man : Transform;
private var caprigidb : Rigidbody;
private var capcoll : CapsuleCollider;

var walkspeed : float = 1.7f;
var runspeed : float = 4.2f;
private var movespeed : float;
private var movedir : int;
var dirVector : Vector3;

var moving : float;

private var animator : Animator;

//ControlFreak
//public	var ctrl	: TouchController;
//public static var STICK_WALK	: int = 0;
//public static var STICK_FIRE	: int = 1;

private var h : float;
private var v : float;
var hxd : float;
var vxd : float;

private var h2 : float;
private var v2 : float;
private var h2x : float;
//private var v2x : float;

public var canWalk : boolean = true;


// ----------------------

function Start(){

animator = man.GetComponent("Animator");
caprigidb = capsule.GetComponent(Rigidbody);
capcoll = capsule.GetComponent(CapsuleCollider);

}

function switchoffColl()
{
capcoll.enabled = false;
animator.enabled = false;
caprigidb.isKinematic = true;
	print("sendmessswitchoffcoll");
	
	}
// ----------------------

function FixedUpdate()
	{
	
if(canWalk){
	
h = 0;
v = 0;
dirVector = Vector3(0,0,0);
h = Input.GetAxis("Horizontal");
v = Input.GetAxis("Vertical");  

/*     
if (this.ctrl != null)
		{
var walkStick	: TouchStick	= this.ctrl.GetStick(STICK_WALK);
var fireStick	: TouchStick	= this.ctrl.GetStick(STICK_FIRE);

// ----------------
// Stick 'Walk'...
// ----------------
if (walkStick.Pressed())		
{	
var walkVec: Vector2	= walkStick.GetVec();
h = walkVec.x;			
v = walkVec.y;

}

// ----------------	
// Stick 'Fire'...	
// ----------------
	
if (fireStick.Pressed())
{
var fireVec: Vector2	= fireStick.GetVec();
h2 = fireVec.x;	
v2 = fireVec.y;

}

}	
*/

if(v>0) movedir = 1;
if(v<0) movedir = -1;
//WALKSTICK MOVE
       if(v<0) movespeed = walkspeed*0.75;
       if(v==0) movespeed = 0;
       if(v>0) movespeed = walkspeed;
       
       if(v>0) capsule.transform.Rotate(0, 1*h, 0);
       if(v<0) capsule.transform.Rotate(0, -1*h, 0);

//world.space
dirVector = Vector3(h,man.eulerAngles.y,v);
       //rotate vector
       dirVector = Quaternion.Euler(0, h, 0) * man.forward * movedir;
       
       hxd = dirVector.x;
       vxd = dirVector.z;
 
caprigidb.velocity.x = hxd * movespeed;
caprigidb.velocity.z = vxd * movespeed;

// ----------------------
// SEND ANIMATOR DATA
//----------------------

moving = caprigidb.velocity.magnitude;

if(animator.enabled==true) {
animator.SetFloat("speed", moving);
animator.SetFloat("v", v);
}

//print("m " + moving);
//print("v " + v);

}
}

