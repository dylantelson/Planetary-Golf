using UnityEngine;
using System.Collections;

public class ShootSaws : MonoBehaviour {

    public GameObject sawToShoot;

    public Vector3 direction;

	// Use this for initialization
	void Start () {
        InvokeRepeating("LaunchProjectile", 2.0f, 2.0f);
	}
	
	// Update is called once per frame
	void LaunchProjectile () {
        GameObject saw = (GameObject) Instantiate(sawToShoot, new Vector2 (transform.position.x, transform.position.y), transform.rotation);
        saw.GetComponent<Rigidbody2D>().AddForce(saw.transform.right * 10, ForceMode2D.Impulse);
        //saw.GetComponent<Rigidbody2D>().velocity = transform.forward * 10f;
        //direction = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        //direction.z = transform.rotation.z;
        //saw.GetComponent<Rigidbody2D>().velocity = new Vector3(-10, 0);
        //saw.GetComponent<Rigidbody2D>().velocity = direction * 100;
    }
}