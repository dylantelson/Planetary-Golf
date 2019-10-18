using UnityEngine;
using System.Collections;
using Water2DTool;

public class BallController : MonoBehaviour {

    public LevelManager theLevelManager;
    public Vector3 respawnPosition;
    public Rigidbody2D myRigidBody;

    public bool touchingGround;
    public bool touchingMagnet;

    private Vector3 ballPosition;
    private Vector3 bombPosition;

    public GameObject bombExplosion;
    public GameObject groundHitParticles;

    private bool waitingForGroundParticles;

    private float previousVelocity;

    private TrailRenderer theTrailRenderer;
    private SpriteRenderer spriteRenderer;
    private bool seen;

    public GameObject splatterPrefab;
    public GameObject splatterGroundPrefab;

    public bool waitMagnet;
    public GameObject magnet;

	// Use this for initialization
	void Start () {
        theLevelManager = FindObjectOfType<LevelManager>();
        respawnPosition = transform.position;
        myRigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        theTrailRenderer = GetComponent<TrailRenderer>();
        theTrailRenderer.sortingLayerName = "Midground";
       // magnetPointEffector = FindObjectOfType<PointEffector2D>();
        myRigidBody.isKinematic = false;
        waitMagnet = false;

    }

    // Update is called once per frame

    //If I have to end up changing the values of gravityScale because other worlds have different gravityscales, then change the numbers to variables, and change the value it resets to on LevelManager script's Respawn function because for now it's 2

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GravitySwitch")
        {
            if (myRigidBody.gravityScale == 2)
            {
                myRigidBody.gravityScale = -2;
            }
            else
            {
                myRigidBody.gravityScale = 2;
            }
        }

        if (other.tag == "Bomb")
        {
            ballPosition = new Vector3(transform.position.x, transform.position.y);
            bombPosition = new Vector3(other.transform.position.x, other.transform.position.y);
            myRigidBody.velocity = new Vector3(0, 0, 0);
            myRigidBody.AddForce((ballPosition - bombPosition) * ((previousVelocity * 20) + 800));
            Instantiate(bombExplosion, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Water2D")
        {
            theLevelManager.waters = other.GetComponent<Water2D_Simulation>();
        }
    }

        void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Magnet")
        {
            touchingMagnet = true;
        }

        if (other.gameObject.tag == "Ground")
        {
            touchingGround = true;
        }

        if (other.gameObject.tag == "Spikes")
        {
            GameObject splatter = Instantiate(splatterPrefab, new Vector3(transform.position.x, transform.position.y + 0.2f), Quaternion.identity) as GameObject;
            splatter.transform.SetParent(other.transform);
            theLevelManager.Respawn();
        }

        if (other.gameObject.tag == "Ground" && !waitingForGroundParticles)
        {
            GameObject splatterGround = Instantiate(splatterGroundPrefab, new Vector3(transform.position.x, transform.position.y + 0.2f), Quaternion.identity) as GameObject;
            splatterGround.transform.SetParent(other.transform);
            waitingForGroundParticles = true;
            StartCoroutine("GroundParticleWait");
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && !touchingGround)
        {
            touchingGround = true;
        }
        if (other.gameObject.tag == "Magnet" && !touchingMagnet)
        {
            touchingMagnet = true;
        }
    }

    void OnTriggerStay2D (Collider2D other)
    {
        if (other.gameObject.tag == "Damager" && touchingGround)
        {
            GameObject splatter = Instantiate(splatterPrefab, new Vector3(transform.position.x, transform.position.y + 0.2f), Quaternion.identity) as GameObject;
            splatter.transform.SetParent(other.transform);
            theLevelManager.Respawn();
        }
        if (other.gameObject.tag == "KillPlane" && !spriteRenderer.isVisible)
        {
            theLevelManager.Respawn();
        }
        //if (other.gameObject.tag == "Magnet" && touchingMagnet && waitMagnet == false && myRigidBody.isKinematic == false)
        //{
        //    myRigidBody.isKinematic = true;
        //}
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            touchingGround = false;
        }
        if (other.gameObject.tag == "Magnet")
        {
            touchingMagnet = false;
        }
    }

    public IEnumerator GroundParticleWait()
    {
        yield return new WaitForSeconds(0.2f);
        waitingForGroundParticles = false;
    }

    //public IEnumerator MagnetWait()
    //{
    //    yield return new WaitForSeconds(1f);
    //    waitMagnet = false;
        //magnetPointEffector.enabled = true;
    //}

    void Update()
    {
    previousVelocity = myRigidBody.velocity.magnitude;
        if(transform.position.y < -20)
        {
            theLevelManager.Respawn();
        }
    }

}
