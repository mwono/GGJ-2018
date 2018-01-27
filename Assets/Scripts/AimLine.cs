using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
	public float gravity = 10;
	public float divs = 20;
	public float throw_strength=1.0;
	public Vector2 getAim(){
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 player = this.gameobject.transform.position;
		Vector2 aim = Normalize(mouse - player);
		return aim;
	}
	
	public Vector2[] drawAim(Vector2 aim , this.divs){
		Vector2[] line; // line of the motion
		line.add(this.gameobject.transform.position); // first point is player position
		y0=line[0].y;
		x0=line[0].x;
		v0=aim*throw_strength;
		for(i=0.0;i<1.0;i=i+(1/divs)){
			yi= y0 + (v0.y + (1/2)*(-gravity)*(i^2));
			xi= x0 + v0.x;
			line.Add(xi,yi);
		}
		return line;
	}
}

