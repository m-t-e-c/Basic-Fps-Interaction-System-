using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class RunnerControl : MonoBehaviour
{

    private Animator animator;
    private GameManager gameManager;

    [SerializeField] private float moveSens;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Movement();
        AnimationControl();
    }

    private void Movement()
    {
        if (gameManager.gameState != GameState.STARTED) return;
        float mouseX = Input.GetAxis("Mouse X") * moveSens * Time.deltaTime;
        var clampedPos = new Vector3(transform.position.x + mouseX, transform.position.y, transform.position.z);
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        transform.position = clampedPos;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void AnimationControl()
    {
        if (gameManager.gameState != GameState.STARTED)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
    }

    private void OnGameManagerCreated(GameManager manager) => gameManager = manager;

    private void OnEnable()
    {
        EventManager.OnGameManagerCreated += OnGameManagerCreated;
    }

    private void OnDisable()
    {
        EventManager.OnGameManagerCreated -= OnGameManagerCreated;
    }
}
