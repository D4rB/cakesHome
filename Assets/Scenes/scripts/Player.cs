using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float vSpeed;
	public float hSpeed;
	 public bool enter = true;
    public bool stay = true;
    public bool exit = true;
	public float healthRecover = 25;
	public float powerDuration = 3;
	public Slider HealthBar;
	public Image speedIcon;
	public Image shieldIcon;
	public Material speedDisabled;
	public Material speedEnabled;
	public Material shieldDisabled;
	public Material shieldEnabled;
	public CanvasGroup endGameGUI;
	public CanvasGroup levelGUI;
	public Image resultIcon;
	public Material deadMaterial;
	public Material winMaterial;
	public Text timerText;
	public Image buttonIcon;
	public Material buttonMaterial;
	public float startTimer = 5;
	public CanvasGroup controllerGUI;
	GameObject enemies;
    Vector3 initialp;

    private Vector3 target;
    private Animator animator;
    private bool isSwimming=false;
	private Rigidbody rb;
	private float hp = 100;
	private float originalSpeed;
	private float originalVSpeed;
	private bool powerSpeed = false;
	private bool powerShield = false;
	private bool powerActive = false;
	private float powerLeft = 0.0f;
	private bool speedMaterialEnbl = false;
	private bool shieldMaterialEnbl = false;
	private float flashIcon = 0.0f;
	private bool dead = false;
	private bool deadAKW = false;//aknowledge
	private float timer = 0;
	private float nextLelevPush = 3;
	private bool gameStarted = false;
	
	// Use this for initialization
	void Start () {
        target = transform.position;
        animator = gameObject.GetComponent<Animator>();
		//animator.SetInteger("Speed",2);
		rb = GetComponent<Rigidbody>();
       	initialp = transform.position;
		originalSpeed = hSpeed;
		originalVSpeed = vSpeed;
		HealthBar.value = hp;
		//GetComponent<Camera>().cullingMask &=  ~(1 << LayerMask.NameToLayer("endGameGUI"));
		//endGameGUI.scaleFactor = 0;
		endGameGUI.alpha = 0;
		levelGUI.alpha = 1;
		timer = 0;
    }
	
	void Update(){
		if(!gameStarted){
			if (Input.anyKey){
					//Debug.Log("A key or mouse click has been detected");
				gameStarted = true;
				startGame();
				startTimer = -1;
			}
			/*
			startTimer -= Time.deltaTime;
			if(startTimer <= 0){
				gameStarted = true;
				startGame();
			}*/
		}else{
			if(powerLeft > 0)
				powerLeft -= Time.deltaTime;
			if(powerActive && powerLeft <= (powerDuration / 3) && powerLeft > 0){
				if(flashIcon <= 0){
					flashIcon = 0.3f;
					if(powerShield)
						if(shieldMaterialEnbl)
							shieldIcon.material = shieldDisabled;
						else
							shieldIcon.material = shieldEnabled;
						shieldMaterialEnbl = !shieldMaterialEnbl;
					if(powerSpeed)
						if(speedMaterialEnbl)
							speedIcon.material = speedDisabled;
						else
							speedIcon.material = speedEnabled;
						speedMaterialEnbl = !speedMaterialEnbl;
				}else{
					flashIcon -= Time.deltaTime;
				}
			}else if(powerActive && powerLeft <= 0){
				print("time's up");
				powerActive = false;
				powerShield = false;
				powerSpeed = false;
				hSpeed = originalSpeed;
				vSpeed = originalVSpeed;
				shieldIcon.material = shieldDisabled;
				speedIcon.material = speedDisabled;
				speedMaterialEnbl = false;
				shieldMaterialEnbl = false;
			}
			timer += Time.deltaTime;
	
			string minutes = Mathf.Floor(timer / 60).ToString("00");
			string seconds = (timer % 60).ToString("00");
			
			print(string.Format("{0}:{1}", minutes, seconds));

			if(hp < 0)
				die();

			if(dead){
				nextLelevPush -= Time.deltaTime;
				if(nextLelevPush <= 0)
					buttonIcon.material = buttonMaterial;
				if (Input.anyKey){
					//Debug.Log("A key or mouse click has been detected");
					restart();
				}
				
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(!dead && startTimer <= 0){
			float horizontalAxis = Input.GetAxis("Horizontal");
			float verticallAxis = Input.GetAxis("Vertical");
			if(horizontalAxis < -0.05)
				horizontalAxis = -0.05f;

			if (Input.GetMouseButtonDown(0)) {
				target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				target.z = 0f;
			}

			if (Input.GetAxis("Horizontal") < -0.001f || Input.GetAxis("Horizontal") > 0.001f) {
				target = new Vector3(transform.position.x + Input.GetAxis("Horizontal"),transform.position.y,0f);
				isSwimming = true;
			}

			if (Input.GetAxis("Vertical") < -0.001f || Input.GetAxis("Vertical") > 0.001f)
			{
				target = new Vector3(transform.position.x, transform.position.y + Input.GetAxis("Vertical"), 0f);
				isSwimming = true;
			}

			if (isSwimming) 
				animator.Play("swiming");
			else{
				animator.StopPlayback();
				animator.Play("Idle");
			}
			//transform.position = Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);//this cause issues with colliders
			//transform.Translate(new Vector3(horizontalAxis * speed * Time.deltaTime,0,0));
			//transform.Translate(new Vector3(0, verticallAxis * speed * Time.deltaTime,0));
			
			Vector3 movement = new Vector3 (horizontalAxis * hSpeed, verticallAxis * vSpeed,0);

			rb.velocity =  (movement); 
		}
	}
	void startGame(){
		controllerGUI.alpha = 0;
		levelGUI.alpha = 1;
	}
	private void die(){
		if(!deadAKW){
			deadAKW = true;
			dead = true;
			resultIcon.material = deadMaterial;
			float deadRotation = 180.0f;
			transform.eulerAngles = new Vector3(deadRotation, transform.eulerAngles.y, transform.eulerAngles.z);
			Vector3 movement = new Vector3 (0, 2,0);
			rb.velocity =  (movement); 
     		//GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("endGameGUI");
			//endGameGUI.scaleFactor = 1;
			endGameGUI.alpha = 1;
			levelGUI.alpha = 0;
			string minutes = Mathf.Floor(timer / 60).ToString("00");
			string seconds = (timer % 60).ToString("00");
			timerText.text = string.Format("{0}:{1}", minutes, seconds);
		}
	}

	private void win(){
			resultIcon.material = winMaterial;
			rb.velocity =  (new Vector3(0,0,0));
			levelGUI.alpha = 0;
			endGameGUI.alpha = 1;
			string minutes = Mathf.Floor(timer / 60).ToString("00");
			string seconds = (timer % 60).ToString("00");
			timerText.text = string.Format("{0}:{1}", minutes, seconds);
	}

	private void restart(){
		if(nextLelevPush <= 0)
			Application.LoadLevel(Application.loadedLevel);
	}

    private void OnCollisionEnter(Collision collision)
    {
		if(collision.gameObject.layer == 9){	
			if(collision.gameObject.tag == "Algae"){
				Debug.Log("Alga");
			}else if(collision.gameObject.tag == "crab"){
				Debug.Log("kani");
			}else if(collision.gameObject.tag == "kraken"){
				Debug.Log("The migthy Kraken");
			}else
				print("	ouch ");
		}else if(collision.gameObject.layer == 10){
			print("Feel the Power!!!");
			if(collision.gameObject.tag == "speed"){
				Debug.Log("speedy");
				hSpeed = originalSpeed *2;
				vSpeed = originalVSpeed * 3;
				powerSpeed = true;
				powerActive = true;
				powerLeft = powerDuration;
				speedIcon.material = speedEnabled;
				speedMaterialEnbl = true;
			}else if(collision.gameObject.tag == "health"){
				hp += 25;
				if(hp > 100)
					hp = 100;
				HealthBar.value = hp;
				Debug.Log("aspirine: " + hp);
			}else if(collision.gameObject.tag == "shield"){
				Debug.Log("invincible");
				powerActive = true;
				powerShield = true;
				powerLeft = powerDuration;
				shieldIcon.material = shieldEnabled;
				shieldMaterialEnbl = true;
			}
			Destroy(collision.gameObject);
		}
    }

	private void OnTriggerEnter(Collider other)
    {
		if(!powerShield){
			if (other.gameObject.tag == "fisura"){
				print("fisura Collision");
				//Physics.IgnoreCollision(collision.transform.GetComponent<Collider>(), GetComponent<Collider>());
			}else if (other.gameObject.tag == "fisuraD"){
				print("fisuraD Collision");
				Destroy(gameObject);
			}else if(other.gameObject.tag == "Algae"){
				Debug.Log("Alga");
			}else if(other.gameObject.tag == "crab"){
				Debug.Log("oh crab!");
			}else if(other.gameObject.tag == "kraken"){
				hp = 0;
				die();
				Debug.Log("The migthy Kraken");
			}else
				print("	ouch ");
			if (enter){
				Debug.Log("entered");
			}
		}
		if(other.gameObject.tag == "endLEvel"){
				Debug.Log("The end");
				win();
				dead = true;
			}
    }

    // stayCount allows the OnTriggerStay to be displayed less often
    // than it actually occurs.
    private float stayCount = 0.0f;
    private void OnTriggerStay(Collider other)
    {
        if (stay && !powerShield)
        {
            /*if (stayCount > 0.25f)
            {
                Debug.Log("staying");
                stayCount = stayCount - 0.25f;
            }
            else
            {
                stayCount = stayCount + Time.deltaTime;
            }*/
            /*foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
                // calculate direction from target to me
                Vector3 forceDirection = transform.position - collider.transform.position;
                // apply force on target towards me
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            }*/
			if(other.gameObject.tag == "fisura"){
				Vector3 target = initialp;
				enemies = GameObject.FindWithTag("fisura");
				float fixedSpeed = 0.5f * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, enemies.transform.position, fixedSpeed);
			}else if(other.gameObject.tag == "Algae" ){
				hSpeed = originalSpeed/4;
				if(hp < 0)
					print("Dead");
				else{
					print("dying : " + hp);
					hp -= 10 * Time.deltaTime;
				}
			}else if(other.gameObject.tag == "crab" ){
				if(hp < 0)
					print("Dead");
				else{
					print("dying : " + hp);
					hp -= 15 * Time.deltaTime;
				}
			}
			
			HealthBar.value = hp;
            // Apply this movement to the rigidbody's position.
            //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(exit && !powerShield){
			hSpeed = originalSpeed;
            Debug.Log("exit");
        }
    }
}
