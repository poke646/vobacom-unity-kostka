using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    private bool isTouchStarted = false;
    private bool isAngleCaught = false;
    private float currentAngle = 0f;
    private Vector3 startPoint = Vector3.zero;
    private Vector3 basePoint = Vector3.zero;

    public void ResetController()
    {
        isTouchStarted = false;
        isAngleCaught = false;
        currentAngle = 0f;
        startPoint = basePoint = Vector3.zero;
    }

    void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector3 point = GetScreenPointInWorld(Input.mousePosition);

            if (!isTouchStarted)
            {
                startPoint = point;
                basePoint = new Vector3(startPoint.x, 0f, startPoint.z);
                isTouchStarted = true;
            }

            Debug.DrawLine(startPoint, basePoint);

            if (isTouchStarted && Vector3.Distance(startPoint, point) > 1f)
            {
                Debug.DrawLine(startPoint, point);

                if (!isAngleCaught)
                {
                    currentAngle = CalculateAngle(basePoint, point);
                    isAngleCaught = true;
                }

                PlayerController.instance.Move(AngleToDirection(currentAngle));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouchStarted = false;
            isAngleCaught = false;
            currentAngle = 0f;
            startPoint = basePoint = Vector3.zero;
        }
    }

    private Vector3 GetScreenPointInWorld(Vector2 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private float CalculateAngle(Vector3 a, Vector3 b)
    {
        Vector3 normalizedPoint = b - a;
        float angle = Vector3.Angle(Vector3.forward, normalizedPoint);
        Vector3 cross = Vector3.Cross(Vector3.forward, normalizedPoint);

        if (cross.y < 0f)
            angle = 360f - angle;

        return angle;
    }

    private Vector3 AngleToDirection(float angle)
    {
        float fixedAngle = angle - 45f;
        int q = Mathf.FloorToInt(fixedAngle < 0f ? 360f - fixedAngle : fixedAngle) / 90;

        switch(q)
        {
            case 0:
                return Vector3.right;
            case 1:
                return Vector3.back;
            case 2:
                return Vector3.left;
            default:
                return Vector3.forward;
        }
    }
}
