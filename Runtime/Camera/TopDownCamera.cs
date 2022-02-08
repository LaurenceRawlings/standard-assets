using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField]
    private int angle = 25;

    [SerializeField]
    private float mainSpeed = 50f;

    [SerializeField]
    private float sprintAdd = 50f;

    [SerializeField]
    private float maxSpeed = 200f;

    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField]
    private KeyCode raiseKey = KeyCode.Space;

    [SerializeField]
    private KeyCode lowerKey = KeyCode.LeftControl;

    private float totalSprint;
    private bool sprint;
    private Transform _transform;

    private void Awake()
    {
        totalSprint = 1f;
        sprint = false;
        _transform = transform;
    }

    private void Start()
    {
        _transform.rotation = Quaternion.Euler(new Vector3(angle, _transform.rotation.y, _transform.rotation.z));
    }

    void Update()
    {
        Vector3 velocity = GetDirection();

        if (velocity.sqrMagnitude <= 0)
        {
            return;
        }

        UpdateInput();
        totalSprint = sprint ? totalSprint + Time.deltaTime : Mathf.Max(totalSprint * 0.5f, 1f);
        velocity *= sprint ? sprintAdd * totalSprint : mainSpeed;
        velocity *= Time.deltaTime;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
        velocity.z = Mathf.Clamp(velocity.z, -maxSpeed, maxSpeed);

        _transform.Translate(velocity, Space.World);
    }

    private void UpdateInput()
    {
        sprint = Input.GetKey(sprintKey);
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");

        if (Input.GetKey(raiseKey))
        {
            direction += Vector3.up;
        }
        else if (Input.GetKey(lowerKey))
        {
            direction += Vector3.down;
        }

        return direction;
    }
}
