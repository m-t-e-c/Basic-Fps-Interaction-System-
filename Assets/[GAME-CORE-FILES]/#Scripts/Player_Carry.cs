using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class Player_Carry : MonoBehaviour
{
    public static Action<ICarryable> onCarryObject;

    private Camera cam;

    [SerializeField]
    private Transform m_CarryingArea;

    [SerializeField]
    private ICarryable m_CarryObject;

    [SerializeField]
    private GameObject m_DropUI;

    [SerializeField]
    private Image m_DropFill;

    [SerializeField]
    private float m_DropHoldTime;

    [SerializeField]
    private float m_DropForce;

    private float m_DrowWaitTime;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        FollowCarryArea();
        DropControl();
    }

    private void FollowCarryArea()
    {
        if (m_CarryObject == null) return;
        m_CarryObject.m_Transform.position = m_CarryingArea.position;
        m_CarryObject.m_Transform.rotation = m_CarryingArea.rotation;
    }

    private void DropControl()
    {
        if (m_CarryObject == null) return;
        m_DropFill.fillAmount = m_DrowWaitTime.Remap01(0, m_DropHoldTime);
        if (Input.GetKey(KeyCode.G))
        {
            m_DrowWaitTime += Time.deltaTime;
            if (m_DrowWaitTime >= m_DropHoldTime)
            {
                m_DrowWaitTime = 0;
                m_CarryObject.Drop(cam.transform.forward, m_DropForce);
                m_CarryObject = null;
                m_DropUI.transform.DOScale(Vector3.zero, 0.25f);
            }
        }
        else m_DrowWaitTime = 0;
    }

    private void OnCarryObject(ICarryable carryObject)
    {
        m_CarryObject = carryObject;
        m_CarryObject.Carry();
        m_DropUI.transform.DOScale(Vector3.one, 0.25f);
    }

    private void OnEnable()
    {
        onCarryObject += OnCarryObject;
    }

    private void OnDisable()
    {
        onCarryObject -= OnCarryObject;
    }
}
