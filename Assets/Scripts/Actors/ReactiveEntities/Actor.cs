using UnityEngine;

public class Actor : MonoBehaviour
{

    private void Start()
    {
        NPC npc = this.GetComponent<NPC>();
        if(npc != null)
        {
            npc.MoveState = NPC.MovementState.Docile;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Input.GetButtonDown("Interact"))
        {
            
        }
    }

}
