using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;

    public GameObject Particles;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Instantiate(Particles, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
