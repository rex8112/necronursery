using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstraints : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float topY;
    [SerializeField] float botY;
    [SerializeField] float rightX;
    [SerializeField] float leftX;
    private void OnDrawGizmosSelected()
    {
        topY = transform.position.y + transform.lossyScale.y / 2;
        botY = transform.position.y - transform.lossyScale.y / 2;
        rightX = transform.position.x + transform.lossyScale.x / 2;
        leftX = transform.position.x - transform.lossyScale.x / 2;
        Gizmos.color = new Color(0, 0, 1, 0.7f);
        Gizmos.DrawLine(new Vector2(rightX, topY), new Vector2(rightX, botY));
        Gizmos.DrawLine(new Vector2(leftX, topY), new Vector2(leftX, botY));
        Gizmos.DrawLine(new Vector2(rightX, topY), new Vector2(leftX, topY));
        Gizmos.DrawLine(new Vector2(rightX, botY), new Vector2(leftX, botY));
    }

    // Update is called once per frame
    void Update()
    {
        topY = transform.position.y + transform.lossyScale.y / 2;
        botY = transform.position.y - transform.lossyScale.y / 2;
        rightX = transform.position.x + transform.lossyScale.x / 2;
        leftX = transform.position.x - transform.lossyScale.x / 2;

        if (cam.transform.position.x < leftX)
            cam.transform.position = new Vector3(leftX, cam.transform.position.y, -10);
        else if (cam.transform.position.x > rightX)
            cam.transform.position = new Vector3(rightX, cam.transform.position.y, -10);

        if (cam.transform.position.y < botY)
            cam.transform.position = new Vector3(cam.transform.position.x, botY, -10);
        else if (cam.transform.position.y > topY)
            cam.transform.position = new Vector3(cam.transform.position.x, topY, -10);
    }
}
