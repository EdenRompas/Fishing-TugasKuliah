using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BoatMovement : MonoBehaviour
{
    [Header("Platform")]
    public platform platformType;
    public enum platform {
        desktop,
        mobile
    }
    
    [Header("Movement")]
    [SerializeField] private int moveSpeed;
    [SerializeField] private float yawAmount;
    
    [Space]
    [SerializeField] private float yawRotation;
    [SerializeField] private float rotationSpeed;

    //Internal
    private float horizontalInput;
    private float yaw;

    private bool isTurnLeft;
    private bool isTurnRight;

    // Update is called once per frame
    void Update()
    {
        if (platformType == platform.desktop) {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        else if (platformType == platform.mobile) {
            if (isTurnLeft) {
                horizontalInput = Mathf.Lerp(horizontalInput, -1, yawAmount * Time.deltaTime);
            }
            else if (isTurnRight) {
                horizontalInput = Mathf.Lerp(horizontalInput, 1, yawAmount * Time.deltaTime);
            }
            else {
                horizontalInput = Mathf.Lerp(horizontalInput, 0, yawAmount * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate() {
        Moving();
    }

    private void Moving() {
        
        // Move forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;        

        yaw += horizontalInput * yawAmount * Time.deltaTime;
        float roll = Mathf.Lerp(0, yawRotation, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        // Calculate target rotation
        Quaternion targetRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.forward * roll);

        // Apply rotation smoothly
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnClickTurnLeft() {
        
        isTurnLeft = true;
        isTurnRight = false;
    }

    public void OnClickTurnRight() {

        isTurnRight = true;
        isTurnLeft = false;
    }

    public void OnClickResetTurn() {
        isTurnRight = false;
        isTurnLeft = false;
    }
}
