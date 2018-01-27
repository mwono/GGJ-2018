using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public int currentHealth;
    public Vector2 startPos;
    public Rigidbody2D rb;
    private Vector3 aim;
    private const int MAX_HEALTH = 100;

	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        currentHealth = 100;
        startPos = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"),0);
        if (Input.GetAxis("Aim") != 0)
        {
            Vector2 dirToMouse = AimMode();
            if (Input.GetMouseButtonUp(0)) {
                Fire();
            }
        }
 	}

    public void Fire()
    {
        GameObject projectile = Instantiate(this.gameObject);
        projectile.gameObject.GetComponent<CharacterController>().enabled = false;
        Rigidbody2D projRB = projectile.gameObject.GetComponent<Rigidbody2D>();
        projRB.AddForce(new Vector2(3,3));//TEMP VALUES SUBJECT TO CHANGE
    }

    public Vector2 AimMode()
    {
        Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.gameObject.transform.position;
        aim.Normalize();
        return aim;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Spikes coll = collision.gameObject.GetComponent<Spikes>();
            int dam = coll.damage;
            int damageHop = coll.buoyancy;
            Vector3 front = this.gameObject.transform.forward;
            currentHealth -= dam;
            rb.AddForce(new Vector2(, damageHop));
        }
        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        this.gameObject.transform.position = startPos;
        currentHealth = MAX_HEALTH;
    }
}