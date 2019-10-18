using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	private Camera camera;
	private LevelManager theLevelManager;

	private float newY;

	void Start() {
		theLevelManager = FindObjectOfType<LevelManager>();
		camera = GetComponent<Camera> ();
		newY = target.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if (target) {
			if (target.transform.position.y > newY)
				newY = target.transform.position.y;
			Vector3 point = camera.WorldToViewportPoint(new Vector2 (0, newY + 2));
			Vector3 delta = new Vector3(0, newY + 2, 0f) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			if(target.transform.position.y < transform.position.y - 8)
				theLevelManager.Respawn();
		}
	}
}
