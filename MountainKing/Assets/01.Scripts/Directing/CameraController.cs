﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float xMoveSpeed = 500;
    [SerializeField]
    private float yMoveSpeed = 250;
    private float yMinLimit = 5;
    private float yMaxLimit = 80;
    private float x, y;
    private float distance;

    void Awake()
    {
        distance = Vector3.Distance(transform.position, target.position);
        // 카메라 회전값 저장
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        // 카메라 있나 없나 여부확인
        if (target == null) return;

        // 마우스를 x,y축 움직임 찾기
        x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        transform.rotation = Quaternion.Euler(y, x, 0);
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = transform.rotation * new Vector3(0, 0, -distance) + target.position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
