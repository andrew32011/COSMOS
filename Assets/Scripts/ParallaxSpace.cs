using UnityEngine;

public class InfiniteParallaxSpace : MonoBehaviour
{
    public Transform[] backgrounds;
    public Transform player;
    public Transform planet;
    public float parallaxScaleX = 0.5f;
    public float parallaxScaleY = 0.3f;
    public float smoothing = 1f;

    private Vector3 previousPlayerPosition;
    private Vector3 previousPlanetPosition;
    private float[] parallaxLayerPositionsX;
    private float[] parallaxLayerPositionsY;
    private float[] layerSizesX;
    private float[] layerSizesY;

    void Start()
    {
        previousPlayerPosition = player.position;
        previousPlanetPosition = planet.position;
        int length = backgrounds.Length;
        parallaxLayerPositionsX = new float[length];
        parallaxLayerPositionsY = new float[length];
        layerSizesX = new float[length];
        layerSizesY = new float[length];

        for (int i = 0; i < length; i++)
        {
            var background = backgrounds[i];
            var spriteRenderer = background.GetComponent<SpriteRenderer>();
            parallaxLayerPositionsX[i] = background.position.x;
            parallaxLayerPositionsY[i] = background.position.y;
            layerSizesX[i] = spriteRenderer.bounds.size.x;
            layerSizesY[i] = spriteRenderer.bounds.size.y;
        }
    }

    void Update()
    {
        Vector3 deltaPlayerMove = player.position - previousPlayerPosition;
        Vector3 deltaPlanetMove = planet.position - previousPlanetPosition;
        previousPlayerPosition = player.position;
        previousPlanetPosition = planet.position;

        if (deltaPlayerMove.sqrMagnitude > 0.01f || deltaPlanetMove.sqrMagnitude > 0.01f)
        {
            ApplyParallaxEffect(deltaPlayerMove, deltaPlanetMove);
        }
    }

    private void ApplyParallaxEffect(Vector3 deltaPlayerMove, Vector3 deltaPlanetMove)
    {
        int length = backgrounds.Length;
        for (int i = 0; i < length; i++)
        {
            float playerParallaxX = (deltaPlayerMove.x * parallaxScaleX) * (i + 1);
            float playerParallaxY = (deltaPlayerMove.y * parallaxScaleY) * (i + 1);
            float planetParallaxX = (deltaPlanetMove.x * parallaxScaleX) * i;
            float planetParallaxY = (deltaPlanetMove.y * parallaxScaleY) * i;

            parallaxLayerPositionsX[i] += playerParallaxX + planetParallaxX;
            parallaxLayerPositionsY[i] += playerParallaxY + planetParallaxY;

            parallaxLayerPositionsX[i] = RepeatPosition(parallaxLayerPositionsX[i], layerSizesX[i]);
            parallaxLayerPositionsY[i] = RepeatPosition(parallaxLayerPositionsY[i], layerSizesY[i]);

            Vector3 backgroundTargetPos = new Vector3(parallaxLayerPositionsX[i], parallaxLayerPositionsY[i], backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
    }

    private float RepeatPosition(float position, float size)
    {
        if (position >= size)
        {
            return position - size;
        }
        else if (position <= -size)
        {
            return position + size;
        }
        return position;
    }
}

