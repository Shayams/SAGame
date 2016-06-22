using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public int MaxHealthPoints = 100;
    public int CurrentHealthPoints { get; set; }

    public Texture2D ProgressBarEmpty;
    public Texture2D ProgressBarFull;

    private Rect _size;

    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;

        RectTransform rectTransform = GetComponent<RectTransform>();
        _size = rectTransform != null ? rectTransform.rect : new Rect(0,0, 40, 6);
    }

    void OnGUI()
    {
        float barDisplayPercentage = (float)CurrentHealthPoints / (float)MaxHealthPoints;

        // calc the position of the bar
        var barPosition = Camera.main.WorldToScreenPoint(transform.position);
        barPosition.y = Screen.height - barPosition.y;

        // Draw the background
        GUI.BeginGroup(new Rect(barPosition.x - _size.width / 4, barPosition.y, _size.width, _size.height));

        GUI.depth = 2;
        GUI.DrawTexture(new Rect(0, 0, _size.width / 2, _size.height), ProgressBarEmpty, ScaleMode.ScaleAndCrop);

        // Draw the fill
        GUI.depth = 1;
        GUI.DrawTexture(new Rect(0, 0, _size.width / 2 * barDisplayPercentage, _size.height), ProgressBarFull, ScaleMode.ScaleAndCrop);

        GUI.EndGroup();
    }
}
