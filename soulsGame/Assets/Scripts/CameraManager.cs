using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManeger inputManeger;
    private Transform targetTransform;
    public Transform cameraPivot;
    private Transform cameraTransform;
    private Vector3 cameraVectorPosition; 
    private float defaultPosition;
    public LayerMask whatIsGround;

    private Vector3 currentFollowVelocity = Vector3.zero;

    private float followSpeed = 0.2f;
    private float cameraLookSpeed = 0.2f;
    private float cameraPivotSpeed = 0.2f;
    private float cameraCollisionRadius = 0.2f;
    private float cameraCollisionOffset  = 0.2f;
    private float minCollisionOffset = 0.2f;
    private float maxPivotAngle = 35f;
    private float minPivotAngle = -35f;

    private float lookAngle;//camera要轉動的yRotation(左右看的角度
    private float pivotAngle;//cameraPivot要轉動的xRotation(上下看的角度

    private void Awake() {
        targetTransform = FindAnyObjectByType<PlayManager>().transform; //camera要跟隨的目標的transform
        inputManeger = FindAnyObjectByType<InputManeger>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;  //預設main camera和cameraPivot的距離(預設為-3)
    }

    public void HandleAllCameraMovement(){
        FollowTarget();
        CameraRotate();
        HandleCameraCollision();
    }

    private void FollowTarget(){
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref currentFollowVelocity, followSpeed);
        transform.position = targetPosition;
    }

    private void CameraRotate(){
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManeger.mouseX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManeger.mouseY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle); //限制視角上下看的角度

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollision(){
        Vector3 direction = Vector3.Normalize(cameraTransform.position - cameraPivot.position);
        RaycastHit hit;
        float targetPosition = defaultPosition;

        //從cameraPivot的位置射出一球體檢測是否碰撞，並計算相機應該因碰撞而移動的距離
        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), whatIsGround)){
            float distance = Vector3.Distance(cameraPivot.transform.position, hit.point);

            targetPosition = -(distance - cameraCollisionOffset);

            if (Mathf.Abs(targetPosition) < minCollisionOffset){
                targetPosition -= minCollisionOffset;
            }
        }
        
        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}



