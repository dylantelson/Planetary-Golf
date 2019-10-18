using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public bool hit;
    public bool star;
    public GameObject particleExplosion;
    public LevelManager theLevelManager;

    private Vector3 upPosition;
    private Vector3 downPosition;
    //private Vector3 currentTarget;

	// Use this for initialization
	void Start () {
        theLevelManager = FindObjectOfType<LevelManager>();
        upPosition = new Vector3(transform.position.x, transform.position.y + 0.3f);
        downPosition = new Vector3(transform.position.x, transform.position.y - 0.3f);
        //currentTarget = upPosition;
    }

    //void Update()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, currentTarget, 0.5f * Time.smoothDeltaTime);

    //    if (Vector3.Distance(transform.position, upPosition) < 0.15f && currentTarget == upPosition)
    //    {
    //        currentTarget = downPosition;
    //    }
    //    else if (Vector3.Distance(transform.position, downPosition) < 0.15f && currentTarget == downPosition)
    //    {
    //        currentTarget = upPosition;
    //    }
    //}

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Player" && !hit)
        {
            Instantiate(particleExplosion, transform.position, Quaternion.identity);
            hit = true;
            if(star)
            {
                theLevelManager.starsAcquired++;
                hit = false;
                gameObject.SetActive(false);
            } else
            {
                theLevelManager.LevelComplete();
            }
        }
    }
}
