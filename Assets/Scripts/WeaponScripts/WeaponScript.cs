using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 pos;
    private float angle;
    public float distance;

    private void Update()
    {
        rotateAround();
        PointWeapon();
    }
    private void rotateAround()
    {
        pos = Input.mousePosition;
        pos.z = (player.transform.position.z - Camera.main.transform.position.z);
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos = pos - player.transform.position;
        angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;
        transform.localPosition = new Vector3(player.transform.position.x + xPos * 4, (player.transform.position.y + yPos * 4), -1);
    }
    private void PointWeapon()
    {
        var posW = Camera.main.WorldToScreenPoint(transform.position);
        var dirW = Input.mousePosition - posW;
        var angleW = (Mathf.Atan2(dirW.y, dirW.x) * Mathf.Rad2Deg)-90;
        transform.rotation = Quaternion.AngleAxis(angleW, Vector3.forward);
    }
}
