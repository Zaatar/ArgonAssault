using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("The speed of the ship")]
    [SerializeField] float controlSpeed = 10.0f;
    [Tooltip("The range of movement on the x axis of the ship")]
    [SerializeField] float xRange = 50.0f;
    [Tooltip("The range of movement on the y axis of the ship")]
    [SerializeField] float yRange = 8.0f;

    [Header("Ship Lasers")]
    [Tooltip("Laser Particle Systems of Ship")]
    [SerializeField] GameObject[] lasers;

    [Header("Ship Movement")]
    [SerializeField] float positionPitchFactor = -2.0f;
    [SerializeField] float controlPitchFactor = -10.0f;
    [SerializeField] float positionYawFactor = 2.0f;
    [SerializeField] float controlYawFactor = 10.0f;
    [SerializeField] float positionRollFactor = 2.0f;
    [SerializeField] float controlRollFactor = -20.0f;

    float xThrow, yThrow;
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControl = xThrow * controlYawFactor;
        float yaw = yawDueToPosition + yawDueToControl;
        float rollDueToPosition = transform.localPosition.z * positionRollFactor;
        float rollDueToControl = xThrow * controlRollFactor;
        float roll = rollDueToPosition + rollDueToControl;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            EmissionModule emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isActive;
        }
    }
}
