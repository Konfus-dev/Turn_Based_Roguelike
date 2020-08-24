using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact<T>(T component) where T : Component;
}
