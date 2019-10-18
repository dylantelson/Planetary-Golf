using UnityEngine;
using System.Collections;

public class SawsMoving : MonoBehaviour {

    public Rigidbody2D myRigid;

	// Use this for initialization

   void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag != "Hat")
        {
            Destroy(gameObject);
        }
    }
}
