using UnityEngine;
using System.Collections;

public class GateButton : MonoBehaviour {

    public GameObject gateObject;
    public GateScript gateScript;

	// Use this for initialization
	void Start () {
        gateScript = gateObject.GetComponent<GateScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !gateObject.GetComponent<Animator>().GetBool("opened"))
        {
            gateScript.Open();
        }
    }
}
