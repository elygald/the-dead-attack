using UnityEngine;
using System.Collections;

public class movecamera : MonoBehaviour {

    public Transform personagem;
    public float suavização= 0.4f;
    public Vector2 velocidade;
    public bool telatrem;
    public bool radar;
	// Update is called once per frame
    void FixedUpdate()
    {
        if (radar == false) { 
            if (telatrem == true) {
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, personagem.position.x, ref velocidade.x, suavização), transform.position.y, transform.position.z);
     
            } else { 
                transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, personagem.position.x, ref velocidade.x, suavização), Mathf.SmoothDamp(transform.position.y, personagem.position.y, ref velocidade.y, suavização),transform.position.z);
            }
        }
        else
        {
            transform.position = new Vector3(personagem.position.x, personagem.position.y, transform.position.z);
        }

	}
}
