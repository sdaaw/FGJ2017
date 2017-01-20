using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public bool AllowMovement;
    public float speed;

    private Rigidbody m_rigidbody;
    private Vector3 m_moveDirection = Vector3.zero;
    private Transform m_transform;
    private bool m_isGrounded;
    private float m_yVelocity;

    [SerializeField]
    private float m_fallingSpeed;

    [SerializeField]
    private float m_jumpSpeed;

    [SerializeField]
    private LayerMask m_layerMask;

    public bool keyboardTurnMode;
    public float turnSpeed;

    void Awake()
    {
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (AllowMovement)
            DoMovement();
    }
    /// <summary>
    /// Do player movement
    /// </summary>
    void DoMovement()
    {
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.velocity = Vector3.zero;
        Vector3 dist = m_moveDirection;

        if (keyboardTurnMode)
        {
            m_transform.Rotate(0, Input.GetAxis("keyboardTurn") * turnSpeed, 0);

        }
        RaycastHit ray;
        if (Physics.SphereCast(m_transform.position, .5f, Vector3.down, out ray, .5f, m_layerMask))
            m_isGrounded = true;
        else
            m_isGrounded = false;

        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_yVelocity = m_jumpSpeed;
        }
        if (m_isGrounded && m_yVelocity < 0)
        {
            m_yVelocity = -0.1f;
        }
        else
        {
            m_yVelocity += Physics.gravity.y * m_fallingSpeed * Time.deltaTime;
        }

        dist.y = m_yVelocity * Time.deltaTime;

        m_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_moveDirection = m_transform.TransformDirection(m_moveDirection);
        m_moveDirection *= speed;

        m_rigidbody.velocity = dist;
    }
}