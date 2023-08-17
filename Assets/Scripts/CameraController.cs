using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float thisLevelClampXMin;
    public float thisLevelClampXMax;
    public float biasX;
    private float axisY;
    private float axisX;
    public Transform target;
    public float speed;
    public float deadZone;

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > deadZone)
        {
            axisY = target.position.y;
            axisY = Mathf.Clamp(axisY, 0, axisY);
            axisX = target.position.x + biasX;
            axisX = Mathf.Clamp(axisX, thisLevelClampXMin, thisLevelClampXMax);
            Vector3 targetPosition = new Vector3(axisX, axisY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
