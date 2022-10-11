using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode, RequireComponent(typeof(GridLayoutGroup))]
public class GridLayoutHeightSetter : MonoBehaviour
{
    private RectTransform rect;

    private void Start()
    {
        rect = (RectTransform)(this.transform);
    }
    private void Update()
    {
        GridLayoutGroup layout = this.GetComponent<GridLayoutGroup>();
        float x = rect.sizeDelta.x;
        float spacing = layout.spacing.y;
        float padding = layout.padding.top + layout.padding.bottom;
        int childCount = this.transform.childCount;
        rect.sizeDelta = new Vector2(x, childCount * layout.cellSize.y + (childCount + 1) * spacing + padding);
    }
}