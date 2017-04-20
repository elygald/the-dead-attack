using UnityEngine;
using System.Collections;

public class aranha : MonoBehaviour {
    public bool vendo;
    public bool descendo;
    public bool tocandochao;
    public bool desce;
    public GameObject basededescida;
    public GameObject line;
    public Animator animacao;
    public float velocidade;
    public float velocidadedescida;
    public GameObject destino;
    public bool impacto;
    public float sangue;
    public float valordano;
    private bool atacando;
    public bool andando;
    public bool parado;
    private bool morta;
    public float timerimpacto;
    public float impactotime;
    public float timermorte;
    public float mortetime;
    public float timerbarra;
    public float teiatime;
    public float timerteia;
    public float barratime;
    private Rigidbody2D inimigo;
    public float sangueatual;
    public GameObject barrafrente;
    public GameObject barrafundo;
    private float valorpuntsangue;
    private Vector2 v;
    GameObject respaw;
    public int lengthOfLineRenderer = 2;


    public AudioClip somandando;
    public AudioClip somatacando;
    public AudioClip sommorrendo;
    public AudioClip somimpacto;
    public AudioSource somcontroller;



    public Color c1 = Color.yellow;
    public Color c2 = Color.red;

    private int dest = 0;
    private float startTime;
    private float speedy;
    private float speedydescida;
    private string colisionInfo ="Collision info";
    
	// Use this for initialization
	void Awake() {
        barrafrente.GetComponent<SpriteRenderer>().enabled = false;
        barrafundo.GetComponent<SpriteRenderer>().enabled = false;
        vendo = false;
        morta = false;
        atacando = false;
        andando = true;
        startTime = Time.deltaTime;
        speedy = velocidade;
        speedydescida = velocidadedescida;
        velocidadedescida = velocidadedescida * -1;
        velocidade = velocidade * -1;
        sangueatual = sangue;
        valorpuntsangue = 100/sangue ;
        valorpuntsangue = valorpuntsangue / 100;
        barrafrente.transform.localScale = new Vector3((valorpuntsangue * sangueatual), barrafrente.transform.localScale.y, 0);
        destino = GameObject.FindWithTag("Player");
        respaw = GameObject.FindWithTag("respawaranha");
        if (desce == true) {
            morta = false;
            atacando = false;
            andando = false;
            parado = false;
            gameObject.transform.position = basededescida.transform.position;
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.SetColors(c1, c2);
            lineRenderer.SetWidth(0.02F, 0.02F);
            lineRenderer.SetVertexCount(lengthOfLineRenderer);
            lineRenderer.gameObject.layer = 1;
            teiatime = 2f;
        }
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        impacto = verificaimpacto();
        if (vendo == true) {
           
            descendo = Aranhadescendo();
            if (morta != true && descendo == false)
            {
                Movimentando();
            }
        
        }
        else
        {
            if (tocandochao == false && gameObject.transform.position == basededescida.transform.position)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
                parado = true;
                andando = false;
                descendo = false;
            }
            else {
                descendo = Aranhadescendo();
            }
            
        }

        animacao.SetBool("andandoaranha", andando);
        animacao.SetBool("aranha_atacando", atacando);
        animacao.SetBool("parada", parado);
        animacao.SetBool("impacto", impacto);
        animacao.SetBool("morta", morta);
        animacao.SetBool("descendo", descendo);
	
	}
    bool verificaimpacto()
    {
        if (barrafrente.GetComponent<SpriteRenderer>().enabled == true)
        {
            timerteia += Time.deltaTime;
            if (timerteia > barratime)
            {
                timerteia = 0;

                barrafrente.GetComponent<SpriteRenderer>().enabled = false;
                barrafundo.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (sangueatual <= 0)
        {
           
            barrafrente.GetComponent<SpriteRenderer>().enabled = true;
            barrafundo.GetComponent<SpriteRenderer>().enabled = true;

            if (somcontroller.isPlaying == false)
            {
                somcontroller.clip = sommorrendo;
                somcontroller.Play();
            }
            morta = true;
            atacando = false;
            andando = false;
            parado = false;
            impacto = false;
            timermorte += Time.deltaTime;
                     
           if (timermorte > mortetime)
            {

                respaw.GetComponent<createaranha>().quantidade -= 1;
                Destroy(gameObject);
                // Debug.Log("Carregador =" + carregador);
            }

        }
        if (impacto == true && morta != true)
        {
            atacando = false;
            andando = false;
            parado = false;

          
            timerimpacto += Time.deltaTime;
            if (timerimpacto > impactotime)
            {
                sangueatual = sangueatual - valordano;
                barrafrente.transform.localScale = new Vector3((valorpuntsangue * sangueatual), barrafrente.transform.localScale.y, 0);
                barrafrente.GetComponent<SpriteRenderer>().enabled = true;
                barrafundo.GetComponent<SpriteRenderer>().enabled = true;
                impacto = false;
                andando = true;
                timerimpacto = 0;
                if (sangueatual > 0)
                {
                    somcontroller.clip = somimpacto;
                    //somcontroller.time = 2f;
                    somcontroller.Play();
                }

            }


        }
        return impacto;

    }
    void Movimentando() {
        if (destino.transform.position.x > gameObject.transform.position.x)
        {
            velocidade = speedy;

            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            if (velocidade == speedy)
            {
                velocidade = velocidade * -1;
            }

            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (destino.transform.position.y < gameObject.transform.position.y)
        {
            descendo = true;
            //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
           // velocidade = velocidade * -1;
        }
        else {
            descendo = false;
        }
        // transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.5f);

        // Debug.Log("Personagem" + destino[1].transform.position.x + "aranha" + gameObject.transform.position.x + "distancia" + (destino.transform.position.x - gameObject.transform.position.x));
      
        if (atacando == false && impacto == false)
        {
            if ((destino.transform.position.x - gameObject.transform.position.x) > 0.7 || (destino.transform.position.x - gameObject.transform.position.x) < -0.7)
            {

                gameObject.transform.position += new Vector3((velocidade), 0, 0) * Time.deltaTime;
                andando = true;
                parado = false;
                if (somcontroller.isPlaying == false)
                {
                    somcontroller.clip = somandando;
                    somcontroller.Play();
                }
            }
            else
            {
                // if (dest == 0)
                //  {
                //dest = 1;
                // velocidade = speedy;
                //  }
                // else {

                //     dest = 0;
                // }
                if (atacando == false)
                    parado = true;
                    andando = false;
            }
           
        }
        if (atacando == true && somcontroller.isPlaying == false)
        {
            somcontroller.clip = somatacando;
            somcontroller.Play();
        }

        
    
    }
    bool Aranhadescendo() {
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        if (desce == true) { 
        if (descendo == true)
        {
            atacando = false;
            andando = false;
            parado = false;
            impacto = false;
            //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Vector3[] points = new Vector3[lengthOfLineRenderer];
            lineRenderer.enabled = true;
          
            points[0] = line.transform.position;
            points[1] = new Vector3(basededescida.transform.position.x, basededescida.transform.position.y, basededescida.transform.position.z);
            

            lineRenderer.SetPositions(points);
            // RaycastHit2D raio = Physics2D.Raycast(line.transform.position, new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.z));

            gameObject.transform.position += new Vector3(0, (velocidadedescida), 0) * Time.deltaTime;
        }
        else
        {
            if (descendo == false && lineRenderer.enabled == true)
            {
                timerteia += Time.deltaTime;
                if (timerteia > teiatime)
                {
                    lineRenderer.enabled = false;

                    // Debug.Log("Carregador =" + carregador);
                }
            }
        }
        }
        return descendo;
    
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "chao":
                descendo = false;
                parado = true;
                tocandochao = true;
                //andando = true;
                break;
		case "Player":
			if (morta == false) {
				atacando = true;
				parado = false;
				andando = false;
				descendo = false;
				other.gameObject.GetComponent<andandopernas> ().valordano = 25f;
				other.gameObject.GetComponent<andandopernas> ().sendoatacado = true;
			}

                break;

            default:
                break;
        }
       
        //colisionInfo = "houve colisão TriggerEnter" + other.gameObject.tag;
       
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject col = other.gameObject;
        colisionInfo = "houve colisão TriggerExit" + col.gameObject.tag;
        if (col.CompareTag("chao"))
        {
            Debug.Log("tag");
            descendo = false;
            andando = false;
            tocandochao = false;
            parado = true;
        }
      
        if (col.CompareTag("Player"))
        {
            atacando = false;
            andando = true;
            other.gameObject.GetComponent<andandopernas>().valordano = 0f;
            other.gameObject.GetComponent<andandopernas>().sendoatacado = false;
        }
    }
    void OnGUI()
    {
       // GUI.Label(new Rect(200, 75, 300, 100), colisionInfo);
    }
}
