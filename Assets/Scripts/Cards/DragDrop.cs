using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool _isDragging;
    private bool _isInPlayArea;
    private Vector2 _initialPosition;
    private Vector3 _initialScale;
    public GameObject mainPlayArea;
    public CardScript cardScript;

    public void StoreCardPosition()
    {
        _initialPosition = transform.position;
        _initialScale = transform.localScale;
    }

    private void Update()
    {
        if (_isDragging)
            transform.position = Input.mousePosition;
    }

    public void OnCardDrag()
    {
        if (_initialPosition == null)
            StoreCardPosition();

        _isDragging = true;
    }

    public void OnCardDrop()
    {
        if (_isInPlayArea)
        {
            PlayCard();
        }
        else
            transform.position = _initialPosition;

        _isDragging = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isInPlayArea = true;
        transform.localScale = Vector3.one;
        mainPlayArea = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isInPlayArea = false;
        mainPlayArea = null;
        transform.localScale = _initialScale;
    }

    private void PlayCard()
    {
        if (mainPlayArea != null)
        {
            PlayAreaScript script = mainPlayArea.GetComponent<PlayAreaScript>();
            Card card = cardScript.card;

            if (Playable.isValidPlay(card.Value, script.lastPlayedValue))
            {
                script.PlayCard(cardScript.card);
                transform.SetParent(null, false);
                transform.position = Vector3.zero;
                gameObject.SetActive(false);
            }
            else
            {
                transform.position = _initialPosition;
            }
        }
    }
}
