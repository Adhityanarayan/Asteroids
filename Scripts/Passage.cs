using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public float bOffset = 0.01f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Vector2 position = collision.transform.position;
            position.x = collision.transform.position.x;
            position.y = -collision.transform.position.y + bOffset;
            collision.transform.position = position;
            
        }
        
    }
}
