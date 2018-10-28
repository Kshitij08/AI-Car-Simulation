using UnityEngine;
using System.Collections;

public class damageControl : MonoBehaviour
{

public float hdamage = 500f;
public bool exploded;
public bool smoking;
//public Transform xposition;
public float dSmoke = 150f;
public float dExplo = 50f;

	public GameObject smoke;
	public GameObject fire;
	public GameObject explo;

	

void Update () {

		if (hdamage <= dSmoke){
		Smoke();
		}

		if (hdamage <= dExplo){
		Explosion();
		}


}

void addDamage (float damage) {

if (hdamage <= 0.0)
		return;
		
hdamage -= damage;

}

void Explosion () {

if(!exploded){
if(explo) {
//ex2 = Instantiate(explo, xposition.position, xposition.rotation);
//ex2.parent = xposition;
				explo.SetActive(true);
}
if(fire) {
//ex3 = Instantiate(fire, xposition.position, xposition.rotation);
//ex3.parent = xposition;
				fire.SetActive(true);
}

exploded = true;
}

}


void Smoke () {

if(!smoking){
if(smoke) {
//ex1 = Instantiate(smoke, xposition.position, xposition.rotation);
//ex1.parent = xposition;
				smoke.SetActive(true);
}

smoking = true;
}

}


	void OnTriggerEnter(Collider myTrigger) {
		if(myTrigger.transform.tag == "bullet"){
			addDamage(50);
		}

	}


}
