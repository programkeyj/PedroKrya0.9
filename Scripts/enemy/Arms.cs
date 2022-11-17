using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [Range(0, 1)] public int isLeftOrRight;
    public float speed = 300f;

    public float Boost = -30f;

    [SerializeField] private bool Aiming;

    private Rigidbody2D rb;
    private GameObject _player;
    [SerializeField] private enemy _enemy;

    private void Start() 
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetAiming(bool _aiming){
        Aiming = _aiming;
    }
    private void FixedUpdate() {

        if (Aiming && _player != null && _enemy.GetCanShoot()){
            Vector3 difference = _player.transform.position - transform.position;
            var euler = transform.eulerAngles;
            euler.z = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;
            rb.MoveRotation(euler.z+Boost);
        }
    }
}