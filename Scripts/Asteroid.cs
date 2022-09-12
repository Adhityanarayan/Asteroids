using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxLifeTime = 30.0f;

    private int count = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
            this.transform.localScale = Vector3.one * this.size;

            _rigidbody.mass = this.size;
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);

        Destroy(this.gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Bullet")
        //{
        //    //size divide by 2 = multiply by 0.5
        //    if((this.size * 0.5f) >= this.minSize)
        //    {
        //        count++;
        //        if(count > 2)
        //        {
        //            CreateSplit();
        //            CreateSplit();
        //            Destroy(this.gameObject);
        //        }
        //    }
        //    else
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //size divide by 2 = multiply by 0.5
            if ((this.size * 0.5f) >= this.minSize)
            {
                count++;
                if (count > 2)
                {
                    CreateSplit();
                    CreateSplit();
                    Destroy(this.gameObject);
                    GameManager.instance.AsteroidDestroyed(this);
                }
            }
            else
            {
                Destroy(this.gameObject);
                GameManager.instance.AsteroidDestroyed(this);
            }
        }
    }

    public void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
