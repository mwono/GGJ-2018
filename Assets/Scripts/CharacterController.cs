using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float gravity = 10;
    public int divs = 20;
    public float throw_strength = 1000.0f;
    public int currentHealth;
    public Vector2 startPos;
    public Rigidbody2D rb;
    public Collider2D coll2D;
    public Vector3 aim;
    public GameObject proj;
    public int timeOut = 10;
    public GameObject corpse;

    private const int MAX_HEALTH = 100;
    private  int jumps = 1;
    private bool isGrounded;
    private LineRenderer lr;
    private float thresh = 0.01f;
    public Vector3 oldpos, newpos,oldmouse,newmouse;
    private GameObject Chld;
    private int frameRate = 48;
    private GameObject lastFired;
    private float distToGround;

    // Use this for initialization
    void Start () {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        lr = this.gameObject.GetComponent<LineRenderer>();
        //aimLine = this.gameObject.GetComponent<AimLine>();
        currentHealth = 100;
        startPos = this.gameObject.transform.position;
        //aim = aimLine.getAim();
        newpos = gameObject.transform.position;
        newmouse = Input.mousePosition;
        Chld = this.gameObject.transform.GetChild(0).gameObject;
        coll2D = Chld.GetComponent<Collider2D>();
        Time.captureFramerate = frameRate;
    }
	
	// Update is called once per frame
	void Update () {
        PlayerMove();
        PlayerMouse();
        PlayerTeleport();
 	}

    public void PlayerMove()
    {
        this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * .1f, 0);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Entering" + collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Spikes")
        {
            Spikes coll = collision.gameObject.GetComponent<Spikes>();
            int dam = coll.damage;
            int damageHop = coll.buoyancy;
            Vector3 front = this.gameObject.transform.forward;
            currentHealth -= dam;
            rb.AddForce(new Vector2(0, damageHop));
        }
        if (currentHealth <= 0)
        {
            Respawn();
        }
    }


    public void PlayerMouse()
    {
        oldpos = newpos + newmouse;
        newpos = gameObject.transform.position + Input.mousePosition;
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lr.enabled = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                lastFired=Fire(aim);
                lr.enabled = false;
            }
            if (newpos != oldpos)
            {
                aim = getAim();
                List<Vector3> linearr = drawAim(aim);
                lr.positionCount = linearr.Count;
                lr.SetPositions(linearr.ToArray());
            }
        }
    }

    public void PlayerTeleport()
    {
        if (lastFired && Input.GetMouseButtonDown(1))
        {
            projectile t = lastFired.GetComponent<projectile>();
            t.Teleport();
            die();
        }
    }

    public GameObject Fire(Vector3 aim)
    {
        GameObject projectile = Instantiate(proj);
        projectile.transform.position = Chld.transform.position;
        Rigidbody2D projRB = projectile.gameObject.GetComponent<Rigidbody2D>();
        projRB.AddForce(aim* 10, ForceMode2D.Impulse);
        StartCoroutine("NoProject");
        return projectile;
    }

    private IEnumerator NoProject()
    {
        yield return new WaitForSeconds(timeOut);
        lastFired = null;
    }

    

    void Respawn()
    {
        this.gameObject.transform.position = startPos;
        currentHealth = MAX_HEALTH;
    }

    public Vector3 getAim()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 AB = (mouse - Chld.transform.position);
        float theta = Mathf.Atan2(AB.y, AB.x);
        Vector3 aim = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta),0);
        return aim;
    }

    public List<Vector3> drawAim(Vector3 aim)
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float y0, x0, yi, xi;
        Vector3 v0;
        List<Vector3> line = new List<Vector3>();
        line.Add(Chld.transform.position); // first point is player position
        Vector3 AB = (mouse - Chld.transform.position);
        float theta = Mathf.Atan2(AB.y, AB.x);
        y0 = line[0].y;
        x0 = line[0].x;
        v0.x = Mathf.Cos(theta)*throw_strength;
        v0.y = Mathf.Sin(theta)*throw_strength;
        for (float i=0.0f; i<2.0f; i+=0.1f)
        {
            yi = y0 + v0.y * i - .5f * gravity*.7f * (i * i);
            xi = x0 + v0.x * i;
            line.Add(new Vector3(xi,yi,0.0f));
        }
        return line;
    }
    void die()
    {
        GameObject cor = Instantiate(corpse);
        cor.gameObject.transform.position = this.Chld.transform.position;
        Destroy(this.gameObject);
    }
}