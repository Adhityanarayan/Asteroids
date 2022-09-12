using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool isPressed;
    public Player player;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        InvokeRepeating("ShootBullet", 0f, 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        CancelInvoke("ShootBullet");
    }

    public void ShootBullet()
    {
        if (isPressed)
        {
            player.Shoot();
        }
    }
    //private void FixedUpdate()
    //{
    //    if (isPressed)
    //    {
    //        player.Shoot();
    //    }
    //}
}
