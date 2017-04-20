using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class armakh : MonoBehaviour {
    public Text textmunicao;
    public LayerMask escada;
    public AudioClip reload;
    public AudioClip somarma;
    public GameObject line;
    public ParticleSystem flash;
    public SpriteRenderer arma;
    public Animator anim;
    public bool atirando;
    public bool recarregar;
    public bool andandobraço;
    public float recarregartime;
    public float timer;
    public Transform tiro;
    public GameObject projetio;
    public float forca;
    //public Transform centro;
    public AudioSource somtiro;
    public float timersomtiro;

    

    public float tirotime;
    public float timertiro;
    private float tiroprojetio;


  private Vector3 mouse_pos  ;
  public  Transform target ; //Assign to the object you want to rotate
  private Vector3 object_pos;
  private float angle ;

    //private LineRenderer line;
    public int municao;
    private int carregador;
   // private  Vector3 mousePos;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 2;
   // private LineRenderer lineRenderer;
    private Vector3 ultimaposition;
    public float velocidade;
    private int municaomaxima = 150;

   
	// Use this for initialization
	void Start () {
        c1.a = 0.5f;
        c2.a = 0.5f;
        carregador = municao;
        tiroprojetio = 0;
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.02F, 0.02F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        lineRenderer.gameObject.layer = 1;

        textmunicao.text = carregador + " / " + municaomaxima;
         // Collider is added as child object of line
       
        
     
        
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        //mousePos = Input.mousePosition;
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        mouse_pos.z = mouse_pos.z - object_pos.z;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x ) * Mathf.Rad2Deg;
       
        //line.SetPosition(2, new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.z));
        if (Input.GetButton("Fire1") && carregador > 0 && recarregar == false)
        {
            atirando = true;
           // flash.Play();
         
          
        }
        if (Input.GetButton("Horizontal"))
        {
            if (recarregar == false) {
                andandobraço = true;
            }
        }
        else
        {
            andandobraço = false;
        }

        if (Input.GetButtonDown("Recarregar") && carregador != municao && recarregar == false)
        {
            recarregar = true;
            //carregador = municao;
            timer = 0;
          //  timertiro = 0;
            somtiro.clip = reload;
            //somtiro.time = recarregartime;
            somtiro.Play();
               
            
        }
        else
        {
            if (recarregar)
            {
              if (municaomaxima > 0) { 
                timer += Time.deltaTime;
                if (timer >= recarregartime)
                {
                    int basemunicao;
                    recarregar = false;
                    basemunicao = carregador - municao;
                    carregador = municao;
                    if (basemunicao > municaomaxima) {
                        carregador += municaomaxima;
                    }
                    municaomaxima -= -(basemunicao);
                    if (municaomaxima < 0) { municaomaxima = 0; }
                    textmunicao.text = carregador + " / " + municaomaxima;
                }
              }
            }
        }
       if (atirando)
       {
          
            timertiro += Time.deltaTime;
            if (timertiro > tirotime)
            {
               
                atirando = false;
                carregador = carregador - 1;
                textmunicao.text = carregador + " / " + municaomaxima;
                timertiro = 0;
               // Debug.Log("Carregador =" + carregador);
            }
            
            if (tiroprojetio <= 0)
            {
                somtiro.clip = somarma;
                somtiro.time = timersomtiro;
                if(flash.isStopped==true){
                    flash.Play();
                    somtiro.Play();
                }
               

                GameObject tiros = (GameObject)Instantiate(projetio, new Vector3(tiro.position.x, tiro.position.y, tiro.position.z), transform.rotation);
                tiroprojetio = tirotime;
            }
            tiroprojetio -= Time.deltaTime;
        }
       
        anim.SetBool("tiro", atirando);
        anim.SetBool("recarregar", recarregar);
        anim.SetBool("andando", andandobraço);
       // Transform pivot = transform.Find("braco");
        // transform.RotateAround(pivot.position, Vector3.up, 20);
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
       // }
        
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[lengthOfLineRenderer];

       // float t = Time.time;
        //int i = 0;
       // while (i < lengthOfLineRenderer)
        //{
        points[0] = line.transform.position;
        points[1] = new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.z);
           // i++;
        //}
        //if (angle > -30f || angle < -150f)
        //{

        lineRenderer.SetPositions(points);
        RaycastHit2D raio = Physics2D.Raycast(line.transform.position, new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.z));


        if (raio.collider != null )
            {
                if (raio.collider.gameObject.CompareTag("parede") || raio.collider.gameObject.CompareTag("chao") || raio.collider.gameObject.CompareTag("Player") || raio.collider.gameObject.CompareTag("cadeado") || raio.collider.gameObject.CompareTag("inimigo") || raio.collider.gameObject.CompareTag("agua") || raio.collider.gameObject.CompareTag("aranha"))
                {
                    ultimaposition = new Vector3(raio.point.x, raio.point.y, mouse_pos.z);
                }
               
            }
            else
            {   
                ultimaposition = new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.z);
            }

            lineRenderer.SetPosition(1, ultimaposition);
       // }

       // mira2.transform.Translate(new Vector3(ultimaposition.x, ultimaposition.y, 0.0f));
        Movimentacao();

        
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
