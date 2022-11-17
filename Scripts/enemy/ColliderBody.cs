using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBody : MonoBehaviour
{
    [SerializeField] private Transform bloodPlace;
    [SerializeField] private GameObject Blood;
    [SerializeField] private enemy _enemy;
    [SerializeField] private Balance[] DependedParts;
    [SerializeField] private bool IsHand = false;

    public int Type = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet"){
            if (IsHand){
                _enemy.SetCanShoot(false);
            }
            if (Type == 0){
                Instantiate(Blood, bloodPlace.position, bloodPlace.rotation);
                _enemy.TakeDamage(1.5f);
                for (int i = 0; i < DependedParts.Length; i++){
                    DependedParts[i].GetComponent<Balance>().force = 0f;
                }
            }else if (Type == 1){
                Instantiate(Blood, bloodPlace.position, bloodPlace.rotation);
                _enemy.HeadOut();
                Destroy(gameObject);
            }
            else if(Type == 2) {
                Instantiate(Blood, bloodPlace.position, bloodPlace.rotation);
                for (int i = 0; i < DependedParts.Length; i++){
                    DependedParts[i].GetComponent<Balance>().force = 0f;
                }
                _enemy.TakeDamage(1f);
                Destroy(gameObject);

            }
            Destroy(other.gameObject);
        }    
        /*if (other.tag == "PlayerFoot"){
            if (Type == 1){
                Instantiate(Blood, bloodPlace.position, bloodPlace.rotation);
                _enemy.HeadOut();
            }
        }*/
    }    
}
