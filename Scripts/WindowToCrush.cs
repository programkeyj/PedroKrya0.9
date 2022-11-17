using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowToCrush : MonoBehaviour
{
    [SerializeField] private GameObject Particles;
    private void OnTriggerEnter2D(Collider2D other) {
        ParticleSystem _particleSystem = Instantiate(Particles, transform.position, transform.rotation).GetComponent<ParticleSystem>();
        _particleSystem.Play();
        Destroy(gameObject);
    }
}
