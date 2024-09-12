using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonUpdater : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        Invoke("UpdateCollider",0.05f);
    }

    void UpdateCollider()
    {
        if (spriteRenderer.sprite != null)
        {
            // Получаем координаты обрезки спрайта  
            Vector2[] spriteVertices = GetSpriteVertices(spriteRenderer.sprite);
            polygonCollider.points = spriteVertices;
        }
    }

    private Vector2[] GetSpriteVertices(Sprite sprite)
    {
        return sprite.vertices; // Получаем вертексы спрайта  
    }
}
