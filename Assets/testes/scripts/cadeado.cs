using UnityEngine;
using System.Collections;

public class cadeado : MonoBehaviour {
    public Animator anim;
    public float cadetime;
    public float timercade;
    private bool parado = true;
    private bool cade = false;

  
	// Use this for initialization
	void Start () {
       
	
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (cade == true) { 
        anim.SetBool("parado", parado);
        anim.SetBool("quebrando", cade);

        timercade += Time.deltaTime;
        if (timercade> cadetime)
        {
            Destroy(this.gameObject);
           
        }
        }
            
        
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            cade = true;
            parado = false;
            //Debug.Log("tag");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            cade = true;
            parado = false;
            //Debug.Log("tag");
        }
    }
}
