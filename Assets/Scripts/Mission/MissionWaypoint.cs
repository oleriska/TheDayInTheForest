using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    [SerializeField]
    private Image img;
    [SerializeField]
    private TMPro.TextMeshProUGUI meter;
    [SerializeField]
    private Vector3 offset;

    public Transform target;

    private void Update()
    {
        if (target == null)
        {
            img.gameObject.SetActive(false);
            return;
        }

        if(!img.gameObject.activeInHierarchy)
        {
            img.gameObject.SetActive(true);
        }

        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
    }
}