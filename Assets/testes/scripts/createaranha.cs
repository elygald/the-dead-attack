using UnityEngine;
using System.Collections;

public class createaranha : MonoBehaviour {
    public float tempo;
    public GameObject aranha;
    private float tempot;
    public int quantidademaxima;
    public int quantidade;
	// Use this for initialization
	void Start () {
        tempot = 0;
       // Instantiate(aranha, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (quantidade <= quantidademaxima)
        { 

            tempot += Time.deltaTime;
            if (tempot >= tempo)
            {
                Instantiate(aranha, transform.position, transform.rotation);
                tempot = 0;
                quantidade += 1;
                //aranha.GetComponent<aranha>().
            }
        }
	
	}
}
