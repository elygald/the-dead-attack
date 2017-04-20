using UnityEngine;
using System.Collections;

public class particuladestroy : MonoBehaviour {
    public AudioSource impacto;
    public AudioClip somimpactocadeado;
    private float tempo;

	// Use this for initialization
	void Start () {
        tempo = 0f;
        impacto.clip = somimpactocadeado;
        impacto.Play();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        tempo += Time.deltaTime;
        if (tempo >= 1) {
            Destroy(this.gameObject);
        }
	}
}
