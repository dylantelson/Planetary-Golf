    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;


    public class Swipe : MonoBehaviour {

    public Camera myCamera;
    private LineRenderer lineRenderer; 
    public Vector3 mousePositionStart;
    public Vector3 mousePositionCurrent;
    public GameObject myBall;
    public Rigidbody2D myBallRigidBody;
    public LevelManager theLevelManager;
    public BallController myBallController;

    public float numberOfSwipes;
    public float totalSwipes;

    public Text swipeText;
    private float swipesLeft;

    public bool canSwipe;

    // Use this for initialization
    void Start () {
        canSwipe = true;
        theLevelManager = FindObjectOfType<LevelManager>();
        myBallController = FindObjectOfType<BallController>();
        mousePositionStart = new Vector3(0, 0, 0);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        myBallRigidBody = myBall.GetComponent<Rigidbody2D>();
        swipesLeft = totalSwipes;
        swipeText.text = "Swipes: " + swipesLeft;
        lineRenderer.sortingLayerName = "UI";
    }

    void Update()
    {
        if (numberOfSwipes < totalSwipes && canSwipe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineRenderer.enabled = true;
                mousePositionStart = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            }
            if (Input.GetMouseButtonUp(0) && lineRenderer.enabled == true)
            {
                numberOfSwipes++;
                swipesLeft = totalSwipes - numberOfSwipes;
                swipeText.text = "Swipes: " + swipesLeft;
                myBallRigidBody.velocity = new Vector3(0, 0);
                myBallRigidBody.AddForce((mousePositionStart - mousePositionCurrent) * 80f);
            }
            if (Input.GetMouseButton(0))
            {
                mousePositionCurrent = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                lineRenderer.SetPosition(0, mousePositionCurrent);
                lineRenderer.SetPosition(1, mousePositionStart);
            }
            else
            {
                lineRenderer.enabled = false;
            }
        } else if(Input.GetMouseButtonDown(0) && canSwipe)
        {
            theLevelManager.Respawn();
        }
    }

    public void Reset()
    {
        numberOfSwipes = 0;
        swipesLeft = totalSwipes;
        swipeText.text = "Swipes: " + swipesLeft;
    }

    public void LevelComplete()
    {
        lineRenderer.enabled = false;
        
    }

    }
