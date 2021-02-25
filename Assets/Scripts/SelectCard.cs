using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    public GameObject MainPlayArea;

    public bool IsSelected { get; set; }
    private bool _isPositionStored;
    private readonly Vector2 _hoverOffset = new Vector2(0, 15);
    private Vector2 _initialPosition;

    public bool IsPlayable { get; set; }

    public void OnHover()
    {
        if (IsPlayable)
        {
            if (!_isPositionStored)
            {
                _initialPosition = transform.position;
                _isPositionStored = true;
            }

            transform.position = _initialPosition + _hoverOffset;
        }
    }

    public void ExitHover()
    {
        if (!IsSelected && IsPlayable)
            transform.position = _initialPosition;
    }

    public void OnSelect()
    {
        if (!IsSelected && IsPlayable)
            IsSelected = true;
        else
        {
            IsSelected = false;
            ExitHover();
        }
    }
}
