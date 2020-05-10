using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomWander : MonoBehaviour
{
	public float speed;
	private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		float randomAngle = Random.Range(110, 250);
	   transform.Rotate(Vector3.forward, randomAngle); 
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 movement = new Vector3 (8,0,0);
        rb.velocity =  (transform.up * speed); 
		
    }

	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "krakenBoundary"){
			float randomAngle = Random.Range(110, 250);
			//transform.rotation.y = Random.rotation.y;
			transform.Rotate(Vector3.forward, randomAngle); 
		}
    }
}
