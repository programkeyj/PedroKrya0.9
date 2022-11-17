using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsTrigger : MonoBehaviour
{
    public Player _player;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "EnemyBullet"){
            _player.BulletGotcha();
        }
    }
}
