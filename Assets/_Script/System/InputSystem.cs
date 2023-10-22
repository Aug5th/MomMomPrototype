using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : Singleton<InputSystem>
{
     [SerializeField]
    private Camera _sceneCamera;
    private bool _isTouched = false;
    private  Touch _currentTouch;

    private void Update() {
        if(Input.GetMouseButtonDown(0)) // Check if mouse/touch is holding down
        {
            _isTouched = true;
        }

         if(Input.GetMouseButtonUp(0))
        {
            _isTouched = false;
        }
    }
    public Vector3 getCurrentTouchPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = _sceneCamera.ScreenToWorldPoint(mousePos);
        return mousePos;
    }

    public bool IsScreenTouched()
    {
        return _isTouched;
    }

    public bool IsTouchOnLayer(LayerMask layerMask) // Check if mouse/touch hit specific layermask
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, layerMask);
        if(hit.collider != null)
		{	
            return true;
		} 
		return false;
    }
}
