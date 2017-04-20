using UnityEngine;
using System.Collections;

public class andar_pernas : MonoBehaviour {

    private Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    private Vector3 object_pos;
    private float angle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        mouse_pos.z = mouse_pos.z - object_pos.z;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        Transform from = transform;
        Transform to = transform;
        to.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.5f);
        if (angle >= 90 || angle <= -90)
        {
            to.rotation = Quaternion.Euler(new Vector3(180, 0, -angle));
            transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.5f);
        }
	}
}
