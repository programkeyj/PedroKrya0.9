using UnityEngine;

public class enemy : MonoBehaviour
{
    [Header("Life Properties")]
    [SerializeField] private float Health = 3; 
    [SerializeField] private bool IsDead = false;
    public GameObject Head;
    Animator animator;
    public bool CanShoot = true;

    [Header("Fizik Settings")]
    private Rigidbody2D _Rigidbody;
    public GameObject[] BodyParts;

    [Header("AI Simulating")]
    [SerializeField] private bool angry = false;
    [SerializeField] private bool chill = true;
    [SerializeField] private float ViewingRange;
    [SerializeField] private GameObject _player;
    [SerializeField] private Arms[] arms;

    [Header("Gun Settings")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private float DELAY = 0.6f;
    private float timer = 0f;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_player != null && !IsDead){
            if (Vector2.Distance(transform.position, _player.transform.position) < ViewingRange){
                for(int i = 0; i < arms.Length; i++) {
                    arms[i].SetAiming(true);
                }
                angry = true;
                chill = false;
            }
            if (Vector2.Distance(transform.position, _player.transform.position) > ViewingRange){
                angry = false;
                for(int i = 0; i < arms.Length; i++) {
                    arms[i].SetAiming(false);
                }
            }
            if (angry){
                Angry();
            }
        }
        if (Health <= 0){
            Die();
        }
    }
    public void TakeDamage(float Damage){
        Health -= Damage;
    }
    public void SetCanShoot(bool _canShoot){
        CanShoot = _canShoot;
    }
    public bool GetCanShoot(){
        return CanShoot;
    }
    public void HeadOut(){
        if (IsDead){
            Head.GetComponent<HingeJoint2D>().enabled = false;
        }else{
            Die();
            Head.GetComponent<HingeJoint2D>().enabled = false;
        }
    }
    void Die(){
        IsDead = true;
        animator.enabled = false;
        for (int i = 0; i < BodyParts.Length; i++){
            BodyParts[i].GetComponent<Balance>().enabled = false;
        }
    }
    void Angry(){
        if (CanShoot){
            timer += Time.deltaTime;
            if (timer >= DELAY){
                Shoot();
                timer = 0f;
            }
        }
    }
    void Shoot(){
        audio.Play();
        Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
    }
}
