using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    public GameObject PlayArea;

    private bool _isSelected;
    private bool _isPositionStored;
    private readonly Vector2 _hoverOffset = new Vector2(0, 15);
    private Vector2 _initialPosition;

    public void OnHover()
    {
        if (!_isPositionStored)
        {
            _initialPosition = transform.position;
            _isPositionStored = true;
        }
        
        transform.position = _initialPosition + _hoverOffset;
    }

    public void ExitHover()
    {
        if (!_isSelected)
            transform.position = _initialPosition;
    }

    public void OnSelect()
    {
        if (!_isSelected)
            _isSelected = true;
        else
        {
            _isSelected = false;
            ExitHover();
        }
    }
}
