using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    public Player playerScript;
    [SerializeField] private GameObject PlayerObject;


    public float speed = 5f;
    public float damage = 1f;

    private void Awake() {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = PlayerObject.GetComponent<Player>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (playerScript.m_FacingRight){
            rb.velocity = transform.right * speed; 
        }else{            
            rb.velocity = -transform.right * speed; 
        }
        Destroy(gameObject, 3);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
