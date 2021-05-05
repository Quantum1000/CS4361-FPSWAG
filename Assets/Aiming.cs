using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {
    Vector3 eulers;
	// Use this for initialization
	void Start () {
        eulers = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        eulers = eulers + new Vector3(-y, x, 0);
        eulers.x = Mathf.Clamp(eulers.x, -90, 90);
        transform.localEulerAngles = eulers;
	}
}
