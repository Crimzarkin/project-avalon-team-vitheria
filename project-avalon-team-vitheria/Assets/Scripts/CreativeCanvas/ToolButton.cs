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

    public void Activate()
    {
        if (canvas == null) return;

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
                canvas.LoadImage(artImage);
                break;
        }
    }
}