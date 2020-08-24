using UnityEngine;

public interface IMovement
{
    void TryMove(); //if cant move gets component if there is one in attached to thing in space obj is trying to move to, and call OnCantMove
    void OnCantMove<T>(T component) where T : Component; //handles logic if obj cant move
}
