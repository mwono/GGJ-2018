using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    private Rigidbody2D rb;
    bool isGrounded;
	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, 500f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entering" + collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exited" + collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
