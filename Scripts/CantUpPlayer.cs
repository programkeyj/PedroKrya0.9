using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantUpPlayer : MonoBehaviour
{
    public Player _player;

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "KillingObjs")
            _player.TryUp();
    }*/
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "KillingObjs")
            _player.TryUp();
    }/*
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "KillingObjs")
            _player.TryUp();
    }*/
}
