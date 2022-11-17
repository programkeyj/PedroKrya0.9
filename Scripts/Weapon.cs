using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private GameObject FireEffect;


    [SerializeField] private GameObject magazine;

    [SerializeField] private float DELAY = 0.5f;
    [SerializeField] private Transform firePoint;

    [SerializeField] private Player _player;


    [SerializeField] private AudioSource aud;

    private int BulletCount;
    private bool NowShooting;
    private bool BLOCK;
    [SerializeField] private bool firstShoot = true;
    [SerializeField] private float FirstShootTimer = 0f;
    [SerializeField] private float FirstShootDelay = 0.3f;

    private float timer = 0f;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        _player.SetShooting(NowShooting);
        if (NowShooting){
            if (firstShoot){
                Shoot();
                firstShoot = false;
                FirstShootTimer = 0f;
            }/*
            timer += Time.deltaTime;
            if (timer >= DELAY){
                Shoot();
                timer = 0f;
            }*/
        }else{
            FirstShootTimer += Time.deltaTime;
            if (FirstShootTimer >= FirstShootDelay){
                firstShoot = true;
                FirstShootTimer = 0f;
            }
        }
        if (Input.GetButtonDown("Fire1")){
            FireEffect.SetActive(true);
            NowShooting = true;
            //timer = 0f;

        }
        if (Input.GetButtonUp("Fire1")){
            FireEffect.SetActive(false);
            //timer = 0f;
            NowShooting = false;
            //firstShoot = true;

        }
    }
    void Shoot(){
        
        ShakeCamera.Instance.ShakeCameraWithTimer(1.4f, 0.9f, 0.1f);


        aud.Play();
        
        Instantiate(bullet, firePoint.position, firePoint.rotation);      
          
        aud.Play();
        BulletCount += 1; 

        if (BulletCount == 18 ){           
            Instantiate(magazine, firePoint.position, new Quaternion(0f, 0f, 0f, -100.145f));
            BulletCount = 0;
        }
            
                  
    }
}
