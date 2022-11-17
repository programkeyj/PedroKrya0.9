using UnityEngine;

public class weaponLook : MonoBehaviour
{

    private float offset;
    [SerializeField] private Player _player;

    private void Start() {
        
    }
    void Update(){
        if (_player.m_FacingRight){
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }else{
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(-difference.y, -difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }
    }
}
