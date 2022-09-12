using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ParticleSystem explosionEffect;
    public Player player;

    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        MakeSingleton();
    }

    public void MakeSingleton()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        Vector2 position = asteroid.transform.position;
        explosionEffect.transform.position = position;
        explosionEffect.Play();

        if (asteroid.size < 0.7f)
        {
            SetScore(score + 100); // small asteroid
        }
        else if (asteroid.size < 1.4f)
        {
            SetScore(score + 50); // medium asteroid
        }
        else
        {
            SetScore(score + 25); // large asteroid
        }
    }

    public void PlayerDeadEffect(Player player)
    {
        Vector2 position = player.transform.position;
        explosionEffect.transform.position = position;
        explosionEffect.Play();

        SetLives(lives - 1);
        if (lives <= 0)
        {
            //GameOver();
        }
        else
        {
            //Respwan
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }
    public void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    public void SetScore(int score)
    {
        this.score = score;
        //UI score text
    }
    public void SetLives(int lives)
    {
        this.lives = lives;
        //UI lives text
    }
}
