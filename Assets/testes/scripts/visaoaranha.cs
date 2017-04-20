using UnityEngine;
using System.Collections;

public class visaoaranha : MonoBehaviour {
    private aranha script;
	// Use this for initialization
	void Start () {

	    script = (aranha)GetComponentInParent(typeof (aranha));
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
           
            case "Player":
                script.vendo = true;
                if (script.desce == true)
                {
                    script.descendo = true;
                    script.parado = false;
                }
                break;

            default:
                break;
        }

     

    }
    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {

            case "Player":
                script.vendo = false;
               
                break;

            default:
                break;
        }
     
    }
}
