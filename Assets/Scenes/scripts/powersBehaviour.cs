using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersBehaviour : MonoBehaviour
{
	//public string powerType = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void onCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Player"){
            print("picked up");
            Destroy(gameObject);
        }else{
			print(":O");
		}
	}
}
