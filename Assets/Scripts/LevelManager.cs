using UnityEngine;
using System.Collections;
using Water2DTool;

public class LevelManager : MonoBehaviour {

    public BallController theBall;
    public GameObject deathExplosion;
    public Swipe theSwipe;
    private ResetOnRespawn[] objectsToReset;
    public Coin theCoin;
    public float starsAcquired;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject levelCompleteMenu;

    public Sprite starFull;

    public bool levelComplete;
    public bool levelCompleteDead;

    public Water2D_Simulation waters;

	// Use this for initialization
	void Start () {
        theSwipe = FindObjectOfType<Swipe>();
        theBall = FindObjectOfType<BallController>();
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();
        theCoin = FindObjectOfType<Coin>();
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Restart") && !levelComplete)
        {
            Respawn();
        }
	}

    public void Respawn()
    {
        if (waters != null)
        {
            waters.RemoveCollider2DFromList(theBall.GetComponent<CircleCollider2D>());
        }
        if (!levelCompleteDead) { 
        theBall.GetComponent<TrailRenderer>().enabled = false;
            theBall.gameObject.SetActive(false);
            Instantiate(deathExplosion, theBall.transform.position, Quaternion.identity);
            if (!levelComplete)
            {
                theBall.transform.position = theBall.respawnPosition;
                theBall.gameObject.SetActive(true);
                theCoin.hit = false;
                theBall.myRigidBody.gravityScale = 2;
                starsAcquired = 0;
                theSwipe.Reset();

                for (int i = 0; i < objectsToReset.Length; i++)
                {
                    objectsToReset[i].gameObject.SetActive(true);
                    objectsToReset[i].ResetObject();
                }
                StartCoroutine("RespawnCo");
            } else
            {
                levelCompleteDead = true;
            }
        }
    }

    public IEnumerator RespawnCo()
    {
        theBall.myRigidBody.isKinematic = false;
        
        yield return new WaitForSeconds(0.3f);
        theBall.GetComponent<TrailRenderer>().enabled = true;
    }

    public void LevelComplete()
    {
        theSwipe.canSwipe = false;
        theSwipe.LevelComplete();
        levelCompleteMenu.SetActive(true);
        star1.SetActive(true);
        if(starsAcquired >= 1f) {
            star2.GetComponent<SpriteRenderer>().sprite = starFull;
        }
        star2.SetActive(true);
        if (starsAcquired >= 2f)
        {
            star1.GetComponent<SpriteRenderer>().sprite = starFull;
        }
        star3.SetActive(true);
        if (starsAcquired >= 3f)
        {
            star3.GetComponent<SpriteRenderer>().sprite = starFull;
        }
        levelComplete = true;
    }
}