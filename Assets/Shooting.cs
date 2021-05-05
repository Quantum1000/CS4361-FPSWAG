using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    [SerializeField] Transform playerCamera = null;
    [SerializeField] bool continuous = true;
    // Use this for initialization
    bool seer;
    void Start () {
        seer = false;
	}
	
	// Update is called once per frame
	void Update () {
        bool triggerPressed = Input.GetAxis("Fire1") > 0;
        if (triggerPressed && !seer || !continuous)
        {
            seer = true;
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            GameObject hitObj = null;
            if (Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(playerCamera.position, playerCamera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                hitObj = hit.collider.gameObject;
                print(hitObj.name);
            }
            else
            {
                Debug.DrawRay(playerCamera.position, playerCamera.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
        else if(!triggerPressed && seer)
        {
            seer = false;
        }
	}
}
