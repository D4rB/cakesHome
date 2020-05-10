using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterOffset : MonoBehaviour
{
    // Start is called before the first frame update
   	public float  scrollSpeed  = 0.5f;
	private float offset;
	// float rotate ;
	
	void Update (){
		offset+= (Time.deltaTime*scrollSpeed)/10.0f;
		this.gameObject.GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", new Vector2(offset,0));
	
	}
}
