using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCanvas : MonoBehaviour
{
    public int textureSize = 512;
    public int brushSize = 6;
    public Material canvasMaterial;

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


        rend.material = new Material(canvasMaterial);
        rend.material.mainTexture = texture;
    }

    public void Paint(RaycastHit hit, Color color)
    {
        color.a = 1f;

        Vector2 uv = hit.textureCoord;

        int x = (int)(uv.x * textureSize);
        int y = (int)(uv.y * textureSize);

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

    public void ClearCanvas(Color clearColor)
    {
        Color[] fill = new Color[textureSize * textureSize];

        for (int i = 0; i < fill.Length; i++)
            fill[i] = clearColor;

        texture.SetPixels(fill);
        texture.Apply();
    }

    public void LoadImage(Texture2D image)
    {
        texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);

        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                Color col = image.GetPixelBilinear(
                    (float)x / textureSize,
                    (float)y / textureSize);

                texture.SetPixel(x, y, col);
            }
        }

        texture.Apply();
        rend.material.mainTexture = texture;
    }

}
