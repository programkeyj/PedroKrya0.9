using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
	private bool shooting = false;
	[Header("Bullet Time")]
	[SerializeField] private float SloweMoveDelay = 0.5f;
	[SerializeField] private float SloweMoveTimer = 0f;
	[SerializeField] private bool BulletTime = false;
	[SerializeField] private float StartCameraOrtho;
	[SerializeField] private float ForceOfScalingCamera = 2.5f;
	[SerializeField] private CinemachineVirtualCamera VirtualCam;
	[SerializeField] private Volume PostProcess;
	private Vignette Vignet;
	[SerializeField] private bool FirstSlide = true;
	[SerializeField] private float WaitSlide = 0.1f;
	[SerializeField] private float WaitSlideTimer = 0f;


	[Header("Movement Settings")]
	[SerializeField] private bool Walking = false;
	Animator animator;
	float inputHorizontal;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private float RunSpeed = 60f; 
	[SerializeField] private float WalkSpeed = 30f;

	[Header("Sliding Settings")]
	[SerializeField] private float SlideSpeed = 100f;
	[SerializeField] private bool RightSlideStarted = false;
	[SerializeField] bool AlredySliding = false;
	[SerializeField] private float SlideLength;
	private bool IsSliding;
	private bool CanRun = true;
	private bool CanSlide = true;
	private float SlideTimer = 0f;

	[Header("Die/Ragdoll")]
	[SerializeField] private GameObject Ragdoll;
	[SerializeField] private GameObject DieScreen;

	[Header("Bindings")]
	[SerializeField] private KeyCode SlideButton;
	[SerializeField] private KeyCode JumpButton;
	[SerializeField] private KeyCode SloweMoveButton;
	[SerializeField] private KeyCode WalkButton;

	[Header("Jump Settings")]
	private bool Jump;
	[SerializeField] private int JumpForce = 10;
	[SerializeField] private bool Grounded;

    void Start()
    {
		StartCameraOrtho = VirtualCam.m_Lens.OrthographicSize;
		PostProcess.profile.TryGet<Vignette>(out Vignet);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
		if (Grounded){			
			animator.SetBool("Flying", false);
        	animator.SetFloat("run", Mathf.Abs(inputHorizontal));
			animator.SetBool("Slide", IsSliding);
			animator.SetBool("walking", Walking);
		}
		if (Input.GetKeyUp(WalkButton)){
			if (Walking){
				Walking = false;
			}else{
				Walking = true;
			}
		}
		if (AlredySliding && Input.GetKeyDown(SlideButton)){
			animator.SetBool("Slide", IsSliding);
		}
		if ((inputHorizontal >= 0.01 && Grounded) || (inputHorizontal <= -0.01 && Grounded)){
			ShakeCamera.Instance.ShakeCameraRun(0.1f, 0.1f);
		}
		else if (inputHorizontal == 0f && !shooting){
			ShakeCamera.Instance.StopShaking();
		}
		else{
			animator.SetBool("Flying", true);
		}
		if (SlideTimer >= SlideLength){
			AlredySliding = false;
			IsSliding = false;
			CanRun = true;
			SlideTimer = 0f;
			gameObject.GetComponent<Collider2D>().enabled = true;
			RightSlideStarted = false;
			FirstSlide = true;
			WaitSlideTimer = 0f;
		}

		if (Input.GetKeyDown(JumpButton)){
			Jump = true;
		}
		if (Input.GetKeyUp(JumpButton)){
			Jump = false;
		}


		// Sliding
		if (CanSlide && Grounded){
			if (Input.GetKeyDown(SlideButton)){
				FirstSlide = false;
				//AlredySliding = true;
				IsSliding = true;
				CanRun = false;
				gameObject.GetComponent<Collider2D>().enabled = false;
			}	
		}
		if (Input.GetKeyUp(SlideButton)){	
			FirstSlide = true;		
			WaitSlideTimer = 0f;
    		Vignet.intensity.value = 0f;
			Time.timeScale = 1f;
			VirtualCam.m_Lens.OrthographicSize = StartCameraOrtho;
			AlredySliding = false;
			IsSliding = false;
			CanRun = true;
			SlideTimer = 0f;
			gameObject.GetComponent<Collider2D>().enabled = true;
			RightSlideStarted = false;
		}
		// END


		//BULLET TIME / SLOWEMOVE
		if (Input.GetKeyDown(SloweMoveButton)){
			if (SloweMoveTimer < SloweMoveDelay){
				BulletTime = true;
				SloweMoveTimer += Time.deltaTime;
			}else{
				BulletTime = false;
				SloweMoveTimer = 0f;
			}	
		}
		if (Input.GetKeyUp(SloweMoveButton)){
			BulletTime = false;
			SloweMoveTimer = 0f;
		}
		// END

    }
    void FixedUpdate() {
		if (BulletTime){
			if (SloweMoveTimer < SloweMoveDelay){
				SloweMoveTimer += Time.fixedDeltaTime;
				Time.timeScale = 0.3f;
				VirtualCam.m_Lens.OrthographicSize = StartCameraOrtho - ForceOfScalingCamera;	
    			Vignet.intensity.value = 0.315f;
			}else {
				BulletTime = false;
			}
		}else{
			Vignet.intensity.value = 0f;
			Time.timeScale = 1f;
			VirtualCam.m_Lens.OrthographicSize = StartCameraOrtho;
			SloweMoveTimer = 0f;
		}
		if (!IsSliding && Jump && Grounded){
			Jump = false;
			m_Rigidbody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
		}
		if (IsSliding && Grounded){
			if (WaitSlideTimer < WaitSlide){
				WaitSlideTimer += Time.fixedDeltaTime;
			}
			else if (WaitSlideTimer >= WaitSlide){
				FirstSlide = false;
				AlredySliding = true;
				Slide();
			}else if(!FirstSlide){
				//AlredySliding = true;
				Slide();
			}
		}
		else if (AlredySliding && Input.GetKey(SlideButton)){
			Slide();
		}
		if (CanRun){
			if (Walking){
				Move(WalkSpeed);
			}else{
				Move(RunSpeed);
			}
		}	
    }
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	private void Die(){
		Time.timeScale = 1f;
		DieScreen.SetActive(true);
		Instantiate(Ragdoll, transform.position += new Vector3(0f, -0.5f, 0f), transform.rotation);
		Destroy(gameObject);
	
	}
	public void BulletGotcha () {
		Die();
	}
	public void TryUp(){
		if (!IsSliding){
			Die();
		}
	}
	public void SetGroundnes(bool _grounded){
		Grounded = _grounded;
	}
	public void SetShooting(bool _shooting){
		shooting = _shooting;
	}
	private void Slide(){
		if (m_FacingRight){
			RightSlideStarted = true;
			float move = SlideSpeed*Time.fixedDeltaTime;
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			SlideTimer += Time.fixedDeltaTime;
		}
		else if (!m_FacingRight && !RightSlideStarted){
			float move = -SlideSpeed*Time.fixedDeltaTime;
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			SlideTimer += Time.fixedDeltaTime;
		}
	}
	void Move(float speed){
		float move = (inputHorizontal*speed)*Time.fixedDeltaTime;
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        if (move > 0 && !m_FacingRight && !IsSliding)
		{
			Flip();
		}
		else if (move < 0 && m_FacingRight && !IsSliding)
		{
			Flip();
		}
	}
}