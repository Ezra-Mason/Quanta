using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingObject : MovingObject
{
    [SerializeField] private BoolVariable _isPlayersTurn;
    [SerializeField] private float _coolDownTime;
    private float _timer;
    protected override void Start()
    {
        base.Start();
        _coolDownTime = _moveSettings.InputCoolDown;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _timer -= Time.deltaTime;
        if (!_isPlayersTurn.Value || _moving || _timer >0)
            return;
        float horizontal = (int)Input.GetAxisRaw("Horizontal");
        float vertical = (int)Input.GetAxisRaw("Vertical");

        //make diagonals verticals
        if (horizontal != 0f)
            vertical = 0f;

        if (horizontal != 0f || vertical != 0f)
            //add adaptation of the input from weapon
            AttemptMove<Interactable>(horizontal, vertical);

        //pass turn with spacebar
        if (Input.GetButton("Jump"))
        {
            _isPlayersTurn.Value = false;
        }
    }

    protected override void AttemptMove<T>(float xDirection, float zDirection)
    {
        base.AttemptMove<T>(xDirection, zDirection);
        _timer = _coolDownTime;
        //animation triggers
        _isPlayersTurn.Value = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        Interactable hitInteractable = component as Interactable;
        hitInteractable.Interact();
        //throw new System.NotImplementedException();
    }

}
