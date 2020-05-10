using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float visionRadius;
    public float speed;
    public bool follow;
    public bool secAttack;
	public float tiggerOffset;

    private GameObject player;
    private Vector3 initialPosition;
	private Vector3 triggerCenter;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
		triggerCenter = transform.position;
		triggerCenter.y = triggerCenter.y + tiggerOffset;
        //gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 target = initialPosition;
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < visionRadius)
        {
            if (follow)
            {
                target = player.transform.position;
            }
            Attack();
        }
        else {
            //gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        transform.position = Vector3.MoveTowards(transform.position,target,speed*Time.deltaTime);
        Debug.DrawLine(transform.position,target, Color.red);
	}

    private void Attack() {
       // gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        //Debug.Log("Attack");
        if (secAttack) {
            int childs = gameObject.transform.childCount;
            for (int x = 0; x < childs; x++) {
                Transform child = gameObject.transform.GetChild(x);
                //child.localScale.Scale();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
		triggerCenter = transform.position;
		triggerCenter.y = triggerCenter.y + tiggerOffset;
        Gizmos.DrawWireSphere(triggerCenter,visionRadius);
    }
}
