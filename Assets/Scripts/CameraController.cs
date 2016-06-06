using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public RectTransform Player;

    public Vector2 Margin; // How many steps the player can move until the camera corrects it's position
    public Vector2 Smoothing;

    public BoxCollider2D Bounds;

    private Vector3 _min, _max;

    public bool IsFollowing { get; set; }

	void Start () {
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;

        
        IsFollowing = true;
    }
	
	void Update () {
        var x = transform.position.x;
        var y = transform.position.y;

        if (IsFollowing)
        {
            // If the camera is not within the margin we set - we need to move it 
            if (Mathf.Abs(x - Player.position.x) > Margin.x) x = Mathf.Lerp(x, Player.anchoredPosition.x, Smoothing.x * Time.deltaTime);
            if (Mathf.Abs(y - Player.position.y) > Margin.y) y = Mathf.Lerp(y, Player.anchoredPosition.y, Smoothing.y * Time.deltaTime);
        }

        var cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, _min.y + Camera.main.orthographicSize, _max.y - Camera.main.orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
