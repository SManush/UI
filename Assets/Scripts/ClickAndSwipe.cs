using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]


public class ClickAndSwipe : MonoBehaviour
{
    // Next we will define the variables needed for the script:
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;
    private bool swiping = false;

    // Start is called before the first frame update
    void Awake()
    {
        // Need to initialize our variables, as they are all private
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // set swiping to true when the left mouse button is held down.
        // If swiping is true, we will update the mouse position.

        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }
   
    void UpdateMousePosition()
    {
        // set up the GameObject to move with the mouse position
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    // update the TrailRenderer and BoxCollider.
    // In this method we will just set the enabled state to whatever the swiping boolean is

    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    // When we collide with something, we will check if it’s a Target.

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            //Destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }

}
