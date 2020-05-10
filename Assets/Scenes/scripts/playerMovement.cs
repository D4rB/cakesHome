using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	/*
	
	
			DEPRECATED
	
	
	 */
	public float mov_speed = 15;
	private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        //anim["swiming"].layer = 123;
		anim.Play("swiming");
    }

    // Update is called once per frame
    void Update()
    {
		float horizontalAxis = Input.GetAxis("Horizontal");
		float verticallAxis = Input.GetAxis("Vertical");
        Vector3 hMovement = transform.right * horizontalAxis * mov_speed * Time.deltaTime;
        Vector3 vMovement = transform.up * verticallAxis * mov_speed * Time.deltaTime;
				transform.Translate(new Vector3(horizontalAxis * mov_speed * Time.deltaTime,0,0));
				transform.Translate(new Vector3(0, verticallAxis * mov_speed * Time.deltaTime,0));
    } 

	 /*void timePower()
    {
        float t = Time.time - startTime;

        if((t % 60).ToString("f2") != "5.00"){
            string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timeText.text =  minutes  + ":" + seconds;
        rb.velocity = new Vector3(0,0,-5,);

        float rotation = Input.GetAxis("Horizontal") * speed;

        //rb.Sleep();
        //rb.velocity = new Vector3(0,0,0);
        }
        else
        {
            startTime = Time.time;
            
            active = false;
        }
    }*/

 	/*IEnumerator speedTime () {
         yield return new WaitForSeconds(seconds);
	 }*/
/* 
	void onTriggerEnter(Collider other){
        ///Compare with the fish object
        if(other.gameObject.CompareTag("pickupObject")){
            other.gameObject.SetActive(false);
            //float moveHorizontal = Input.GetAxis("Horizontal") * speed;
            //StartCoroutine(speedTime());
        }
	}*/
}
