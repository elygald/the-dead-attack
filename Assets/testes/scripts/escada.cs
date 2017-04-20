using UnityEngine;
using System.Collections;

public class escada : MonoBehaviour {
    public Animator anim;
    public float escadatime;
    public float timerescada;
    public GameObject cadeado;
    private bool descendo = false;
    public bool final = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {

        if (!cadeado)
        {
            descendo = true;
        }else{
            descendo = false;
        }

        if (descendo == true && final == false)
        {
            anim.SetBool("descendo", descendo);
          
            timerescada += Time.deltaTime;
            if (timerescada > escadatime)
            {
                final = true;

            }
            anim.SetBool("final", final);
        }
        
      
	}
}
