using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCanvas : MonoBehaviour
{
    public int textureSize = 1024;
    public int brushSize = 6;

    private Texture2D texture;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        Color[] fill = new Color[textureSize * textureSize];
        for (int i = 0; i < fill.Length; i++)
            fill[i] = Color.white;

        texture.SetPixels(fill);
        texture.Apply();

        rend.material = new Material(Shader.Find("Unlit/Texture"));
        rend.material.mainTexture = texture;
    }

    public void Paint(Vector3 hitPoint, Color color)
    {
        Vector2 uv = GetUV(hitPoint);

        int x = (int)(uv.x * textureSize);
        int y = (int)(uv.y * textureSize);

        for (int i = -brushSize; i <= brushSize; i++)
        {
            for (int j = -brushSize; j <= brushSize; j++)
            {
                texture.SetPixel(x + i, y + j, color);
            }
        }

        texture.Apply();
    }

    Vector2 GetUV(Vector3 hitPoint)
    {
        Vector3 local = transform.InverseTransformPoint(hitPoint);
        return new Vector2(local.x + 0.5f, local.y + 0.5f);
    }
}
