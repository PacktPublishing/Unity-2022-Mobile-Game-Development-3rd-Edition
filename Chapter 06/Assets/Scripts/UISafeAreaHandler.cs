using UnityEngine;

public class UISafeAreaHandler : MonoBehaviour
{
    RectTransform panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect area = Screen.safeArea;

        /* Pixel size in screen space of the whole screen */
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        /* Set anchors to percentages of the screen used. */
        panel.anchorMin = area.position / screenSize; 
        panel.anchorMax = (area.position + area.size) / screenSize;

    }
}

