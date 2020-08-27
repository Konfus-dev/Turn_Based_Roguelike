using UnityEngine;

public abstract class ReactiveEntity : MonoBehaviour
{
    public abstract void Interact<T>(T component) where T : Component;
}
