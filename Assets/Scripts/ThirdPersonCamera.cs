using System;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private Camera _camera;
    public Vector3 inputMoveDirection;
    [SerializeField] private Transform targetRotation;
    [SerializeField] private float mouseSensitivity;
    private void Awake()
    {
        _camera = Camera.main;
    }
    //Адаптирует движение игрока при повороте камеры
    public Vector3 ConvertToCameraMovement(Vector3 moveDirection, Vector3 inputMove)
    {
        float yMoveValue = moveDirection.y;
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 cameraForwardZ = inputMove.z * cameraForward.normalized;
        Vector3 cameraForwardX = inputMove.x * cameraRight.normalized;

        Vector3 resultMove = cameraForwardX + cameraForwardZ;
        resultMove.y = yMoveValue;
        return resultMove;
    }
    public Quaternion RotateCamera(Vector2 look, Quaternion playerRotation, bool isMove)
    {
        targetRotation.transform.rotation *= Quaternion.AngleAxis(look.x * mouseSensitivity * Time.deltaTime, Vector3.up);
        targetRotation.transform.rotation *= Quaternion.AngleAxis(-look.y * mouseSensitivity * Time.deltaTime, Vector3.right);

        Vector3 angles = targetRotation.transform.localEulerAngles;
        angles.z = 0;
        float angle = targetRotation.transform.localEulerAngles.x;
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if(angle < 180 && angle > 30)
        {
            angles.x = 30;
        }
        targetRotation.transform.localEulerAngles = angles;
        //comment isMove to rotate player in needed duration
        if (isMove)
        {
            playerRotation = Quaternion.Euler(0, targetRotation.rotation.eulerAngles.y, 0);
            targetRotation.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
        return playerRotation;
    }
    public Quaternion RotateCamera(Vector2 look, Quaternion playerRotation)
    {
        targetRotation.transform.rotation *=
            Quaternion.AngleAxis(look.x * mouseSensitivity * Time.deltaTime, Vector3.up);
        targetRotation.transform.rotation *=
            Quaternion.AngleAxis(-look.y * mouseSensitivity * Time.deltaTime, Vector3.right);

        Vector3 angles = targetRotation.transform.localEulerAngles;
        angles.z = 0;
        float angle = targetRotation.transform.localEulerAngles.x;
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 20)
        {
            angles.x = 20;
        }

        targetRotation.transform.localEulerAngles = angles;
        playerRotation = Quaternion.Euler(0, targetRotation.rotation.eulerAngles.y, 0);
        targetRotation.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        return playerRotation;
    }
}