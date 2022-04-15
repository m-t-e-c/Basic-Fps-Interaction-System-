using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollower : MonoBehaviour
{
    [Header("All Cameras")]
    [SerializeField] private Transform PlayerCam;

    // References
    private GameManager gameManager;
    private Camera cam;
    private Transform target;
    private float defaultFov;

    [Header("Camera Properties")]
    [SerializeField] private DOTweenAnimation idleTween;
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(30f, 110f)] private float fov;
    [SerializeField, Range(0f, 1f)] private float lerpTime;
    [SerializeField, Range(0f,360f)] private float cameraXRotation;
    [SerializeField, Range(0f,360f)] private float cameraYRotation;
    [SerializeField, Range(0f,360f)] private float cameraZRotation;
    [SerializeField] private bool useLookAt;
    [SerializeField] private bool useCameraAnim;

    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        defaultFov = cam.fieldOfView;
        target = PlayerCam;
    }

    private void Update()
    {
        /*if (gameManager.gameState == GameState.IN_MENU)
        {
            transform.LookAt(target);
            return;
        }
        else idleTween.DOKill();*/
        FollowControl();
    }

    private void FollowControl()
    {
        if (!target) return;
        cam.fieldOfView = fov;

        Vector3 followPos = new Vector3(target.position.x, target.position.y, target.position.z) + offset;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, followPos.x, lerpTime),followPos.y, followPos.z);
        if (useLookAt)
        {
            transform.LookAt(target);
            return;
        }
        transform.rotation = Quaternion.Euler(cameraXRotation, cameraYRotation, cameraZRotation);
    }

    #region Action Methods
    private void OnGameManagerCreated(GameManager x) => gameManager = x;

    private void OnEnable()
    {
        EventManager.OnGameManagerCreated += OnGameManagerCreated;
    }

    private void OnDisable()
    {
        EventManager.OnGameManagerCreated -= OnGameManagerCreated;
    }
    #endregion
}
