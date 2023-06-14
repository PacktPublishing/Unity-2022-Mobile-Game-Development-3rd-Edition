using TMPro; //TextMeshProUGUI
using UnityEngine;

/// <summary>
/// Responsible for moving the player automatically and
/// receiving input.
/// </summary> 
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// A reference to the Rigidbody component
    /// </summary>
    private Rigidbody rb;

    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5;

    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0, 10)]
    public float rollSpeed = 5;

    public enum MobileHorizMovement
    {
        Accelerometer, 
        ScreenTouch
    }

    [Tooltip("What horizontal movement type should be used")]
    public MobileHorizMovement horizMovement = MobileHorizMovement.Accelerometer;


    [Header("Swipe Properties")]
    [Tooltip("How far will the player move upon swiping")] 
    public float swipeMove = 2f;

    [Tooltip("How far must the player swipe before we will execute the action (in inches)")]
    public float minSwipeDistance = 0.25f;

    /// <summary>
    /// Used to hold the value that converts minSwipeDistance to 
    /// pixels
    /// </summary>
    private float minSwipeDistancePixels;

    /// <summary>
    /// Stores the starting position of mobile touch events
    /// </summary>
    private Vector2 touchStart;

    [Header("Scaling Properties")]

    [Tooltip("The minimum size (in Unity units) that the player should be")]
    public float minScale = 0.5f;

    [Tooltip("The maximum size (in Unity units) that the player should be")]
    public float maxScale = 3.0f;

    /// <summary>
    /// The current scale of the player
    /// </summary>
    private float currentScale = 1;

    private MobileJoystick joystick;

    [Header("Object References")]
    public TextMeshProUGUI scoreText;

    private float score = 0; 
    public float Score
    {
        get 
        { 
            return score;
        }
        set
        {
            score = value;

            /* Check if scoreText has been assigned */
            if (scoreText == null)
            {

                Debug.LogError("Score Text is not set. " +
                "Please go to the Inspector and assign it");
                /* If not assigned, don't try to update it. */
                return;
            }

            /* Update the text to display the whole number portion
            /*  of the score */ 
            scoreText.text = string.Format("{0:0}", score);
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        // Get access to our Rigidbody component
        rb = GetComponent<Rigidbody>();

        minSwipeDistancePixels = minSwipeDistance * Screen.dpi;

        joystick = GameObject.FindObjectOfType<MobileJoystick>();

        Score = 0;

    }

    /// <summary>
    /// FixedUpdate is a prime place to put physics calculations
    /// happening over a period of time.
    /// </summary>

    void FixedUpdate()
    {
        /* If the game is paused, don't do anything */
        if (PauseScreenBehaviour.paused)
        {
            return;
        }

        Score += Time.deltaTime;

        // Check if we're moving to the side
        var horizontalSpeed = Input.GetAxis("Horizontal") * 
                              dodgeSpeed;

        /* If the joystick is active and the player is moving 
         * the joystick, override the value */
        if (joystick && joystick.axisValue.x != 0)
        {
            horizontalSpeed = joystick.axisValue.x * dodgeSpeed;
        }

        /* Check if we are running either in the Unity editor or in a
         * standalone build.*/
        #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
            /* If the mouse is held down (or the screen is tapped
             * on Mobile */
            if (Input.GetMouseButton(0))
            {
                if(!joystick)
                {
                    var screenPos = Input.mousePosition;
                    horizontalSpeed = CalculateMovement(screenPos);
                }
            }
        /* Check if we are running on a mobile device */
        #elif UNITY_IOS || UNITY_ANDROID

            switch (horizMovement)
            {
                case MobileHorizMovement.Accelerometer:
                    /* Move player based on accelerometer direction */
                    horizontalSpeed = Input.acceleration.x * dodgeSpeed;
                    break;

                case MobileHorizMovement.ScreenTouch:
                    /* Check if Input registered more than zero touches */
                    if (!joystick && Input.touchCount > 0)
                    {
                        /* Store the first touch detected */
                        var firstTouch = Input.touches[0];
                        var screenPos = firstTouch.position;
                        horizontalSpeed = CalculateMovement(screenPos);
                    }
                    break;
            }
           
        #endif

        rb.AddForce(horizontalSpeed, 0, rollSpeed);

    }

    /// <summary>
    /// Will figure out where to move the player horizontally
    /// </summary>
    /// <param name="screenPos">The position the player has
    /// touched/clicked on in screen space</param>
    /// <returns>The direction to move in the x axis</returns> 
    private float CalculateMovement(Vector3 screenPos)
    {
        /* Get a reference to the camera for converting 
        * between spaces */
        var cam = Camera.main;

        /* Converts mouse position to a 0 to 1 range */
        var viewPos = cam.ScreenToViewportPoint(screenPos);

        float xMove = 0;

        /* If we press the right side of the screen */
        if (viewPos.x < 0.5f)
        {
            xMove = -1;
        }
        else
        {
            /* Otherwise we're on the left */
            xMove = 1;
        }

        /* replace horizontalSpeed with our own value */
        return xMove * dodgeSpeed;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        /* Using Keyboard/Controller to toggle pause menu */
        if (Input.GetButtonDown("Cancel"))
        {
            // Get the pause menu
            var pauseBehaviour = GameObject.FindObjectOfType<PauseScreenBehaviour>();

            // Toggle the value
            pauseBehaviour.SetPauseMenu(!PauseScreenBehaviour.paused);
        }

        /* If the game is paused, don't do anything */
        if (PauseScreenBehaviour.paused)
        {
            return;
        }

        /* Check if we are running either in the Unity editor or in a
         * standalone build.*/
        #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        // Rest of the Update function...

        /* If the mouse is tapped */
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = new Vector2(Input.mousePosition.x, 
                                            Input.mousePosition.y);
            TouchObjects(screenPos);
        }

        /* Check if we are running on a mobile device */
        #elif UNITY_IOS || UNITY_ANDROID

            /* Check if Input has registered more than zero touches */
            if (Input.touchCount > 0)
            {
                /* Store the first touch detected */
                Touch touch = Input.touches[0];

                TouchObjects(touch.position);
                SwipeTeleport(touch);
                ScalePlayer();
            }

        #endif
    }

    /// <summary>
    /// Will teleport the player if swiped to the left or right
    /// </summary>
    /// <param name="touch">Current touch event</param> 
    private void SwipeTeleport(Touch touch)
    {
        /* Check if the touch just started */
        if (touch.phase == TouchPhase.Began)
        {
            /* If so, set touchStart */
            touchStart = touch.position;
        }

        /* If the touch has ended */
        else if (touch.phase == TouchPhase.Ended)
        {
            /* Get the position the touch ended at */
            Vector2 touchEnd = touch.position;

            /* Calculate the difference between the beginning and
             * end of the touch on the x axis. */
            float x = touchEnd.x - touchStart.x;

            /* If not moving far enough, don't do the teleport */
            if (Mathf.Abs(x) < minSwipeDistancePixels)
            {
                return;
            }

            Vector3 moveDirection;

            /* If moved negatively in the x axis, move left */
            if (x < 0)
            {
                moveDirection = Vector3.left;
            }
            else
            {
                /* Otherwise player is on the right */
                moveDirection = Vector3.right;
            }

            RaycastHit hit;

            /* Only move if player wouldn't hit something */
            if (!rb.SweepTest(moveDirection, out hit, swipeMove))
            {
                /* Move the player */
                var movement = moveDirection * swipeMove;
                var newPos = rb.position + movement;

                rb.MovePosition(newPos);
            }
        }
    }

    /// <summary>
    /// Will change the player's scale via pinching and stretching two
    /// touch events
    /// </summary>
    private void ScalePlayer()
    {
        /* We must have two touches to check if we are 
         * scaling the object */
        if (Input.touchCount != 2)
        {
            return;
        }
        else
        {
            /* Store the touches detected. */
            Touch touch0 = Input.touches[0]; 
            Touch touch1 = Input.touches[1];

            Vector2 t0Pos = touch0.position;
            Vector2 t0Delta = touch0.deltaPosition;

            Vector2 t1Pos = touch1.position;
            Vector2 t1Delta = touch1.deltaPosition;

            /* Find the previous frame position of each touch. */
            Vector2 t0Prev = t0Pos - t0Delta;
            Vector2 t1Prev = t1Pos - t1Delta;

            /* Find the the distance (or magnitude) between the
             * touches in each frame. */
            float prevTDeltaMag = (t0Prev - t1Prev).magnitude;

            float tDeltaMag = (t0Pos - t1Pos).magnitude;

            /* Find the difference in the distances between 
             * each frame. */
            float deltaMagDiff = prevTDeltaMag - tDeltaMag;

            /* Keep the change consistent no matter what the 
             * framerate is */
            float newScale = currentScale;
            newScale -= (deltaMagDiff * Time.deltaTime);

            // Ensure that the new value is valid
            newScale = Mathf.Clamp(newScale, minScale, maxScale);

            /* Update the player's scale */
            transform.localScale = Vector3.one * newScale;

            /* Set our current scale for the next frame */
            currentScale = newScale;

        }
    }

    /// <summary>
    /// Will determine if we are touching a game object and if so
    /// call events for it
    /// </summary>
    /// <param name="screenPos">The position of the touch in 
    /// screen space</param> 
    private static void TouchObjects(Vector2 screenPos)
    {
        /* Convert the position into a ray */
        Ray touchRay = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        /* Create a LayerMask that will collide with all possible 
         * channels */
        int layerMask = ~0;

        /* Are we touching an object with a collider? */
        if (Physics.Raycast(touchRay, out hit, Mathf.Infinity, 
                    layerMask, QueryTriggerInteraction.Ignore))
        {
            /* Call the PlayerTouch function if it exists on a
             * component attached to this object */

            hit.transform.SendMessage("PlayerTouch",
            SendMessageOptions.DontRequireReceiver);
        }
    }

}
