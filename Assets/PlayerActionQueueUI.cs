using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionQueueUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image[] _images;
    [Header("Sprites")]
    [SerializeField] private Sprite _empty;
    [SerializeField] private Sprite _attack;
    [SerializeField] private Sprite _block;
    [SerializeField] private Sprite _move;
    [SerializeField] private Sprite _wait;
    
    [Header("Action Queue")]
    [SerializeField] private TurnActionRuntimeCollection _queuedActions;
    [SerializeField] private IntVariable _actionExecuted;
    private ActionType _previewedType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove()
    {
        _images[_actionExecuted.Value].sprite = _empty;
    }
    public void OnPreviewedAction()
    {
        _previewedType = _queuedActions.List()[_queuedActions.Count() -1].Type;
        switch (_previewedType)
        {
            case ActionType.MOVE:
                _images[_queuedActions.Count() - 1].sprite = _move;
                break;
            case ActionType.ATTACK:
                _images[_queuedActions.Count() - 1].sprite = _attack;
                break;
            case ActionType.BLOCK:
                _images[_queuedActions.Count() - 1].sprite = _block;
                break;
            case ActionType.WAIT:
                _images[_queuedActions.Count() - 1].sprite = _wait;
                break;
            default:
                break;
        }
    }
}
