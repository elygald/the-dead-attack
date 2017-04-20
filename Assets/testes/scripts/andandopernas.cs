using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class andandopernas : MonoBehaviour {
    public Text hp;
    public bool andando;
    public bool parado;
    public bool agachado;
    public bool andandotraz;
    public bool pulando;
    public bool subir;
    public bool paradoescada;
    public bool subindo;
    public bool subiraposlimite;
    public bool tocandochão;
    public bool descendo;
    public Animator anim;
    private Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    private Vector3 object_pos;
    private float angle;
	// Use this for initialization
    public float velocidade;
    Rigidbody2D personagem;
    public AudioClip[] somfoot;
    public AudioSource foot;
    private int indicesom = 0;
    private Vector2 v;
    private SpriteRenderer rend;
    public Animator coracao;
    public float vida;
    public float vidamaxima;
    public float valordano;
    public bool sendoatacado;
    public float timerimpacto;
    public float impactotime;
    public bool mortopersonagem;
    
   float jumpHeight = 8;
	void Start () {
        personagem = GetComponent<Rigidbody2D>();
        subir = false;
        vidamaxima = vida;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (vida <= 0) {
            sendoatacado = false;
            mortopersonagem = true;
            agachado = false;
            andando = false;
            andandotraz = false;
            pulando = false;
            subindo = false;
            paradoescada = false;
            parado = false;
			SceneManager.LoadScene("demo") ;
        }
        hp.text = vida + "/" + vidamaxima;
        if (sendoatacado == true)
        {
             timerimpacto += Time.deltaTime;
             if (timerimpacto > impactotime)
             {
                 vida -= valordano;
                 timerimpacto = 0;
             }
            float perc;
            perc = ((vida - vidamaxima) / vidamaxima * 100)*-1;
            Debug.Log("perc ="+perc);
            bool morto;
            bool verde;
            bool amarelo;
            bool vermelho;
            if (perc >= 100) {
                morto = true;
                verde = false;
                amarelo = false;
                vermelho = false;
            }
            else if (perc >= 50 &&  perc <= 99) {
                morto = false;
                verde = false;
                amarelo = false;
                vermelho = true;
            }
            else if (perc >= 1 && perc <= 49)
            {
                morto = false;
                verde = false;
                amarelo = true;
                vermelho = false;  
            }
            else
            {
                morto = false;
                verde = true;
                amarelo = false;
                vermelho = false; 
            }
            coracao.SetBool("morto", morto);
            coracao.SetBool("verde", verde);
            coracao.SetBool("amarelo", amarelo);
            coracao.SetBool("vermelho", vermelho);
        }
        if (vida > 0) { 
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        mouse_pos.z = mouse_pos.z - object_pos.z;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        v = personagem.velocity;
        if (Input.GetButtonDown("Jump") && pulando == false)
        {
            v.y=9f;
            pulando = true;
            personagem.gravityScale = 1;
            tocandochão = false;
        }

        personagem.velocity = v;

        if (personagem.velocity.y == 0)
        {
            pulando = false;
        }

        Transform from = transform;
        Transform to = transform;
        //if (angle > -30f || angle < -150f)
        //{
            to.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.5f);
            if (angle >= 90 || angle <= -90)
            {
                to.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.5f);
            }
        //}
         
          
        if (Input.GetButton("Horizontal"))
        {
            if (angle >= 90 || angle <= -90)
            {
                if (paradoescada == false && tocandochão == true || parado == true)
                {
                    if (paradoescada == false)
                    {
                        andandotraz = true;
                    }
                  
                }
                else if (tocandochão == false && subir == false)
                    {
                        pulando = true;
                    }
            }
            else {
                if (paradoescada == false && tocandochão == true || parado == true)
                {
                    if (paradoescada == false)
                    {
                        andando = true;
                    }
                    
                   
                }
                else if (tocandochão == false && subir == false)
                {
                    pulando = true;
                }
            }
            if (foot.isPlaying == false && pulando == false && paradoescada == false && tocandochão == true)
            {
                if (indicesom == 0)
                {
                    indicesom = 1;
                }
                else
                {
                    indicesom = 0;
                }
                    foot.clip = somfoot[indicesom];
                    foot.Play();
            }
        }
        else
        {
            andando = false;
            andandotraz = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
           // agachado = true;

        }
        else {
           // agachado = false;
        }

        Movimentacao();
        }
        else
        {

        }
        anim.SetBool("agachado", agachado);
        anim.SetBool("andando", andando);
        anim.SetBool("andandotraz", andandotraz);
        anim.SetBool("pulando", pulando);
        anim.SetBool("subindo", subindo);
        anim.SetBool("paradoescada", paradoescada);
        anim.SetBool("parado", parado);
        anim.SetBool("morto", mortopersonagem);
        
        andando = false;

        
       // Debug.Log(angle); 
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("chao") || col.gameObject.CompareTag("parede") || col.gameObject.CompareTag("agua"))
        {
            if (personagem.velocity.y < 0)
            {
                pulando = false;
            }
            if (subir == true && pulando == false)
            {
                subir = false;
                subindo = false;
                paradoescada = true;
                subiraposlimite = false;
            }
           
            tocandochão = true;
           
            //Debug.Log("tag");
        }
       
    }
    void OnCollisionExit2D(Collision2D coll)
    {
       // Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "chao" || coll.gameObject.tag == "parede" || coll.gameObject.tag == "agua")
        {
            tocandochão = false;
            if (subir == false)
            {
                if(paradoescada == false)
                    pulando = true;
                parado = false;
            }
           
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GameObject col = other.gameObject;
        if (col.CompareTag("escada"))
        {
            subiraposlimite = false;
            subindo = false;
            paradoescada = false;
            subir = false;
            personagem.gravityScale = 1;
            

        }
        if (tocandochão == false)
        {
            pulando = true;
            parado = false;
        }
        if (col.CompareTag("fundo_falso"))
        {
            //bool oddeven = Mathf.FloorToInt(Time.time) % 2 == 0;
            rend = col.GetComponent<SpriteRenderer>();
            // Enable renderer accordingly
            rend.enabled = true;
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject col = other.gameObject;
        if (col.CompareTag("escada"))
        {
            escada esc = other.GetComponent<escada>();
            if (esc.final == true)
            {
                if (pulando == true)
                {
                    paradoescada = true;
                    parado = false;
                }
                else
                {
                    parado = true;
                    paradoescada = false;
                }
                subir = true;
                subindo = false;
               
                v = personagem.velocity;
                personagem.gravityScale = 0;
                v.y = 0f;
                personagem.velocity = v;
                
              
            }
        }
        if (col.CompareTag("fundo_falso"))
        {
            bool oddeven = Mathf.FloorToInt(Time.time) % 2 == 0;
            rend = col.GetComponent<SpriteRenderer>();
            // Enable renderer accordingly
            rend.enabled = false;
        }
      
    }

    void Movimentacao()
    {
        if (Input.GetButton("subindo") && (subir == true && pulando == false))
        {
          
            subindo = true;
            transform.Translate(Vector2.up * velocidade * Time.deltaTime);
            v = personagem.velocity;
            personagem.gravityScale = 0;
            v.y = 0f;
            personagem.velocity = v;
            parado = false;
            paradoescada = false;
            tocandochão = false;
        }
        if (Input.GetButtonUp("subindo") && subir == true)
        {
            if (subindo == true)
            {
                subindo = false;
                paradoescada = true;

            }
           
        }
        if (Input.GetButtonDown("subindo") && paradoescada == true)
        {   
            if(subiraposlimite==false)
                subir = true;
            paradoescada = true;
            if (pulando == true)
            {
                v = personagem.velocity;
                personagem.gravityScale = 0;
                v.y = 0f;
                personagem.velocity = v;
            }
        }

        if (Input.GetButton("descendo") && subir == true && pulando == false)
        {
            if (tocandochão == false) { 
                parado = false;
                transform.Translate(Vector2.down * velocidade * Time.deltaTime);
                personagem.gravityScale = 0;
                subindo = true;
                paradoescada = false;
            }
        }
        if (Input.GetButtonUp("descendo") && subir == true)
        {
            if (subindo == true)
            {
                subindo = false;
                paradoescada = true;

            }
           
        }
        if (Input.GetButtonDown("descendo") && paradoescada == true)
        {   if( tocandochão == false)
            subir = true;
        }
       
       
        if (subir == true && pulando == false)
        {
            personagem.gravityScale = 0;
        }
     
       
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //subir = false;
            if (angle >= 90 || angle <= -90)
            {
                transform.Translate(-Vector2.right * velocidade * Time.deltaTime);
            }
            else {
                transform.Translate(Vector2.right * velocidade * Time.deltaTime);
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
           // subir = false;
            if (angle >= 90 || angle <= -90)
            {
                transform.Translate(Vector2.right * velocidade * Time.deltaTime);
            }
            else
            {
                transform.Translate(-Vector2.right * velocidade * Time.deltaTime);
            }
        }
    
       
    }
    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(200, 75, 300, 100), coracao);
    }
}
