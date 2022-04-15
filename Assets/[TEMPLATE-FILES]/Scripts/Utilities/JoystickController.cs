using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class JoystickController : MonoBehaviour
{
    // References
    private Joystick joystick;
    private Rigidbody rb;
    private Animator animator;

    [Header("Properties")]
    [SerializeField] private float speed;
    [SerializeField] private bool useRigidBody;
    [Range(0, 1)] private float joystickDeadzone = 0.1f;

    [SerializeField] private bool isMoving;

    #region Unity Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MovementControl();
        AnimationControl();
    }
    #endregion

    #region JoystickController Methods
    private void MovementControl()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        rb.angularVelocity = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            float heading = Mathf.Atan2(x * 100f, y * 100f);
            if (y >= joystickDeadzone || y <= -joystickDeadzone || x >= joystickDeadzone || x <= -joystickDeadzone)
            {
                isMoving = true;
                transform.rotation = Quaternion.Euler(0f, (heading * Mathf.Rad2Deg), 0f);
                /*if (useRigidBody) */rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
                //else transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
        else
        {
            isMoving = false;
            if (useRigidBody) rb.velocity = Vector3.zero;
        }
    }

    private void AnimationControl()
    {
        if (!animator) return;
        animator.SetBool("Run", isMoving);
    }

    #endregion

    #region Action Methods

    private void OnJoystickCreated(Joystick manager) => joystick = manager;

    private void OnEnable()
    {
        EventManager.OnJoystickCreated += OnJoystickCreated;
    }

    private void OnDisable()
    {
        EventManager.OnJoystickCreated -= OnJoystickCreated;
    }
    #endregion
}
