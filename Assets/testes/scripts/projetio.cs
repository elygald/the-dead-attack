using UnityEngine;
using System.Collections;

public class projetio : MonoBehaviour {
    public GameObject faisca;
    public GameObject terra;
    public GameObject paredep;
    public GameObject inimigo;
    public GameObject agua;
    public GameObject aranha;
    public float velocidade;
   
  	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        
       
        //transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));


	}
    void FixedUpdate()
    {
        transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       // if (col.gameObject.CompareTag("parede") || col.gameObject.CompareTag("chao") || col.gameObject.CompareTag("cadeado") || col.gameObject.CompareTag("inimigo") || col.gameObject.CompareTag("agua"))
       // {
            switch (col.gameObject.tag)
            {
                case "cadeado":
                    Instantiate(faisca, transform.position, transform.rotation);
                    break;
                case "chao":
                    Instantiate(terra, transform.position, transform.rotation);
                    break;
                case "parede":
                    Instantiate(paredep, transform.position, transform.rotation);
                    break;
                case "inimigo":
                    Instantiate(inimigo, transform.position, transform.rotation);
                    break;
                case "agua":
                    Instantiate(agua, transform.position, transform.rotation);
                    break;
                case "aranha":
                    col.gameObject.GetComponent<aranha>().impacto = true;
                    col.gameObject.GetComponent<aranha>().valordano = 25f;
                    Instantiate(aranha, transform.position, transform.rotation);
                    break;
                default:
                    break;

            }
            if (!col.gameObject.CompareTag("visao") && !col.gameObject.CompareTag("escada") && !col.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            
            //Debug.Log("tag");
        //}
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("parede") || other.gameObject.CompareTag("chao") || other.gameObject.CompareTag("cadeado") || other.gameObject.CompareTag("agua"))
       // {

            switch (other.gameObject.tag)
            {
                case "cadeado":
                    Instantiate(faisca, transform.position, transform.rotation);
                    break;
                case "chao":
                    Instantiate(terra, transform.position, transform.rotation);
                    break;
                case "parede":
                    Instantiate(paredep, transform.position, transform.rotation);
                    break;
                case "inimigo":
                    Instantiate(inimigo, transform.position, transform.rotation);
                    break;
                case "agua":
                    Instantiate(agua, transform.position, transform.rotation);
                    break;
                case "aranha":
                    other.gameObject.GetComponent<aranha>().impacto = true;
                    other.gameObject.GetComponent<aranha>().valordano = 25f;
                    Instantiate(aranha, transform.position, transform.rotation);
                    break;
                default:
                    break;

            }
            if (!other.gameObject.CompareTag("visao") && !other.gameObject.CompareTag("escada") && !other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            
            //Debug.Log("tag");
        //}
    }
}
