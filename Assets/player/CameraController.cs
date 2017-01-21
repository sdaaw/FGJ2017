using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    private Transform m_transform;
    public Transform playerTransform;

    private float m_x;
    private float m_y;

    public float xSpeed;
    public float ySpeed;

    public float minY, maxY;

    private Vector3 m_angles;

    public float distanceMin, distanceMax;

    public float distance = 0;

    private Vector3 velocity;

    public float followSpeed;

    public LayerMask lm;

    private void Awake()
    {
        m_transform = transform;

        m_angles = m_transform.eulerAngles;
        m_x = m_angles.y;
        m_y = m_angles.x;
    }

    public enum Mode
    {
        rotatePlayer,
        ignorePlayer
    };

    [SerializeField]
    private Mode cameraMode;

    private void FixedUpdate()
    {

        m_x += Input.GetAxis("Mouse X") * xSpeed;
        m_y -= Input.GetAxis("Mouse Y") * ySpeed;

        Quaternion rotation = Quaternion.Euler(m_y, m_x, 0);

        m_y = ClampAngle(m_y, minY, maxY);

        m_transform.rotation = rotation;

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        Vector3 desiredPos = rotation * new Vector3(1, 1.5f, -distance) + playerTransform.position;
        // Smooth y axis to slowly follow player.
        float smoothY = Mathf.SmoothDamp(m_transform.position.y, desiredPos.y, ref velocity.y, followSpeed);
        m_transform.position = new Vector3(desiredPos.x, smoothY, desiredPos.z);

        if (playerTransform != null && cameraMode == Mode.rotatePlayer)
            playerTransform.localEulerAngles = new Vector3(playerTransform.localEulerAngles.x, m_transform.localEulerAngles.y, playerTransform.localEulerAngles.z);

        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, (m_transform.position - playerTransform.position).normalized, out hit, (distance <= 0 ? -distance : distance), lm))
        {
            m_transform.position = hit.point - (m_transform.position - hit.point).normalized * 1.2f;
        }

    }

    /// <summary>
	/// Clamps the angle according to min/max values.
	/// </summary>
	/// <returns>The angle.</returns>
	/// <param name="angle">Angle.</param>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Max.</param>
	public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}