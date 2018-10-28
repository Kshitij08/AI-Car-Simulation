using UnityEngine;
using System.Collections;

public class damageTrigger : MonoBehaviour
{
	public GameObject damageControl;
	public float bulletdamage = 2.5f;
	public float concretedamage = 3.5f;

	public float detonationDelay = 0.0f;

	public bool isthisMotor;

	

	void OnCollisionEnter(Collision collision)
	{
		//print ("concrete damage");

		//SENDING DAMAGE
		if(collision.collider.tag.Contains("concrete")){
			float realdamage = concretedamage * collision.relativeVelocity.magnitude;
			damageControl.SendMessage("addDamage", realdamage);
		}

		if(collision.collider.tag.Contains("bullet")){
			float realdamage2 = bulletdamage;
			if(!isthisMotor) damageControl.SendMessage("addDamage", realdamage2);
			if(isthisMotor) damageControl.SendMessage("addDamage", 500);
		}

	}


}

