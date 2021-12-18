using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingObject : MovingObject
{
    [SerializeField] private BoolVariable _isPlayersTurn;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPlayersTurn.Value)
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
