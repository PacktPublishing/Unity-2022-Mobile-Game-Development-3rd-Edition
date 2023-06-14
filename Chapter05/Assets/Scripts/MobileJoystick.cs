using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; /* IDragHandler, IEndDragHandler */

public class MobileJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// A reference to this object's RectTransform component
    /// </summary>
    RectTransform rt;

    /// <summary>
    /// The original position of the stick used to
    /// calculate the offset of movement 
    /// </summary>
    Vector2 originalAnchored;

    /// <summary>
    /// Gets the value of the joystick in a -1 to 1 manner 
    /// in the same way that Input.GetAxis does
    /// </summary>
    public Vector2 axisValue;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        originalAnchored = rt.anchoredPosition;
    }

    /// <summary>
    /// Will allow the user to move the joystick
    /// </summary>
    /// <param name="eventData">Information about the movement, we are only 
    /// using the position</param>
    public void OnDrag(PointerEventData eventData)
    {
        /* We use our parent's info since the joystick moves */
        var parent = rt.parent.GetComponent<RectTransform>();
        var parentSize = parent.rect.size;
        var parentPoint = eventData.position - parentSize;

        /* Calculate the point relative to the parent's local space */
        Vector2 localPoint = parent.InverseTransformPoint(parentPoint);

        /* Calculates what the new anchor point should be */
        Vector2 newAnchorPos = localPoint - originalAnchored;

        /* Prevent the analog stick from moving too far */
        newAnchorPos = Vector2.ClampMagnitude(newAnchorPos, parentSize.x/2);

        // Update the axis value to the new position
        axisValue = newAnchorPos / (parentSize.x / 2);

        rt.anchoredPosition = newAnchorPos;

    }

    /// <summary>
    /// Will be called when the player lets go of the stick
    /// </summary>
    /// <param name="eventData">Information about the 
    /// movement, unused</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        /* Reset the stick to it's original position */
        rt.anchoredPosition = Vector3.zero;
        axisValue = Vector2.zero;
    }

}
