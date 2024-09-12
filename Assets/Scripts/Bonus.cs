using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public bool Positive;
    private PointsCounter pointsCounter;
    public float Effect;
    private bool switched;
    private Spawner spawner;
    
    //[SerializeField] private NegativeBonus negativeBonus;
    private void OnEnable()
    {
        spawner = GameObject.FindFirstObjectByType<Spawner>();
        pointsCounter = GameObject.FindFirstObjectByType<PointsCounter>();
        if (Effect == 0)
        {
            spawner.count -= 1;
            Destroy(gameObject);
        }
        //negativeBonus = gameObject.GetComponent<NegativeBonus>();
    }
    private void Update()
    {
        if (Effect > 0)
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.sprite = spawner.PositiveSprite;
        }
        if (Effect < 0)
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.sprite = spawner.NegativeSprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pointsCounter.AddPoints(Effect);
            Destroy(gameObject);
            spawner.count -= 1;
        }
    }
    //private void ChangeColor()
    //{
    //    if (Positive)
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            
    //    }
    //}
}
