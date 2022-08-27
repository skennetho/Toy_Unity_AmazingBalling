using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    private enum RotateState
    {
        Idle, Vertical, Horizontal, Ready, End
    }
    RotateState currentState = RotateState.Idle;
    public float verticalSpeed = 360f;
    public float horizontalSpeed = 360f;
    public BallShooter ballShooter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity; 
        currentState = RotateState.Idle;
        ballShooter.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case RotateState.Idle:
                ballShooter.enabled = false;
                if (Input.GetButtonDown("Fire1"))
                {
                    currentState = RotateState.Horizontal;
                }
                break;
            case RotateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(Vector3.up, horizontalSpeed * Time.deltaTime,Space.World);
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    currentState = RotateState.Vertical;
                }
                break;
            case RotateState.Vertical:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3( -verticalSpeed * Time.deltaTime, 0, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    currentState = RotateState.Ready;
                }
                break;
            case RotateState.Ready:
                ballShooter.enabled = true;
                break;

        }
    }
}
