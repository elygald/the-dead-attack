using UnityEngine;
using System.Collections;

public class cabeca : MonoBehaviour {
    public bool andando;
   // public Animator anim;
    private Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    private Vector3 object_pos;
    private float angle;
    public float velocidade;
    private andandopernas script;
    public bool morto;
	// Use this for initialization
	void Start () {
        script = (andandopernas)GetComponentInParent(typeof(andandopernas));
      
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (script.mortopersonagem == true)
        {
            morto = true;
        }
        else { 
      //  Movimentacao();
        //mousePos = Input.mousePosition;
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        mouse_pos.z = mouse_pos.z - object_pos.z;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

			Transform from = transform;
			Transform to = transform;
			// if (angle > -30f || angle < -150f)
			// {
			to.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.5f);
			if (angle >= 90 || angle <= -90)
			{
				to.rotation = Quaternion.Euler(new Vector3(180, 0, -angle));
				transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.5f);
			}
        
        if (Input.GetButton("Horizontal"))
        {
            andando = true;
        }
        else
        {
            andando = false;
        }
        }
        //anim.SetBool("morto", morto);
        andando = false;
       
	
	}
    void Movimentacao()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(-Vector2.right * velocidade * Time.deltaTime);
        }
    }
}
