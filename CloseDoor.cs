using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject Door;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            Door.GetComponent<HingeJoint2D>().useMotor = false;

        }    
    }
    private void OnTriggerExit2D(Collider2D other) {
        //Door.GetComponent<Rigidbody2D>().freezeRotation = true;
    }
}
