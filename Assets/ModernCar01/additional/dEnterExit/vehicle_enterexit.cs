using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class vehicle_enterexit : MonoBehaviour
{

public GameObject car;
private GameObject player;

public Transform exitPoint;
public Transform seatPoint;

public bool isPlayerVisible;
public int controlling = 0;
public AudioClip[] soundsystem;

public bool enterState;
public bool exitState;

public Text guiEnter;


void Start () {

isPlayerVisible = false;
if(guiEnter) guiEnter.enabled = false;
}

void OnTriggerEnter(Collider myTrigger) {
    
if(myTrigger.gameObject.tag == "Player"){
//print("playercar");
player = myTrigger.gameObject;
isPlayerVisible = true;
if(guiEnter) guiEnter.enabled = true;
//print("enter vehicle");
    
}
}

void OnTriggerExit(Collider myTrigger) {

if(myTrigger.gameObject.tag == "Player"){
isPlayerVisible  = false;
if(guiEnter) guiEnter.enabled = false;

player = null;
}
}


void FixedUpdate () {

if(Input.GetKeyDown(KeyCode.E) && isPlayerVisible){

player.gameObject.SetActive(false);

player.transform.position = seatPoint.position;
player.transform.rotation = seatPoint.rotation;
// parent player to Exit Point
player.transform.parent = seatPoint;

controlling = 1;
if(guiEnter) guiEnter.enabled = false;
car.SendMessage("SwitchOnDrive");

PlaySounds (); 

}

if (Input.GetKeyDown(KeyCode.R) && controlling == 1){ 
//print("exit");

player.gameObject.transform.position = exitPoint.position;
player.gameObject.transform.rotation = exitPoint.rotation;
player.gameObject.SetActive(true);
player.transform.parent = null;

controlling = 0;
car.SendMessage("SwitchOffDrive");

}
}


void PlaySounds () {
	
if(soundsystem.Length > 0){
GetComponent<AudioSource>().clip = soundsystem[Random.Range(0, soundsystem.Length)];
GetComponent<AudioSource>().Play();
}

}
	

}