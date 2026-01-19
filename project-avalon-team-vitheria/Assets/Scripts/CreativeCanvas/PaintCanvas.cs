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

        Debug.Log($"Painting at UV ({uv.x:F2},{uv.y:F2}) -> pixel ({x},{y}) with color {color}");

        for (int i = -brushSize; i <= brushSize; i++)
        {
            for (int j = -brushSize; j <= brushSize; j++)
            {
                int px = Mathf.Clamp(x + i, 0, textureSize - 1);
                int py = Mathf.Clamp(y + j, 0, textureSize - 1);
                texture.SetPixel(px, py, color);
            }
        }

        texture.Apply();
    }

    Vector2 GetUV(Vector3 hitPoint)
    {
        Vector3 local = transform.InverseTransformPoint(hitPoint);

        float u = (local.x / transform.localScale.x) + 0.5f;
        float v = (local.y / transform.localScale.y) + 0.5f;

        u = Mathf.Clamp01(u);
        v = Mathf.Clamp01(v);

        return new Vector2(u, v);
    }
}

