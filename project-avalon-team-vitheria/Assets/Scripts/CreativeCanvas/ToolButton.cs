using UnityEngine;

public class ToolButton : MonoBehaviour
{
    public enum ToolType
    {
        Erase,
        EraseAll,
        BlankCanvas,
        ArtPiece
    }

    public ToolType tool;
    public Texture2D artImage;

    public PaintCanvas canvas;

    public Material highlightMaterial;

    private Renderer rend;
    private Material originalMaterial;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    public void Highlight(bool enable)
    {
        if (enable)
            rend.material = highlightMaterial;
        else
            rend.material = originalMaterial;
    }

    public void Activate()
    {
        if (canvas == null) return;

        Debug.Log("Activated tool: " + tool);

        switch (tool)
        {
            case ToolType.Erase:
                RightHandPaintInteractor.selectedColor = Color.white;
                break;

            case ToolType.EraseAll:
                canvas.ClearCanvas(Color.white);
                break;

            case ToolType.BlankCanvas:
                canvas.ClearCanvas(Color.white);
                break;

            case ToolType.ArtPiece:
                canvas.LoadImageAsWritableCopy(artImage);
                break;
        }
    }
}