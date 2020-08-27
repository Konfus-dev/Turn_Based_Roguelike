using UnityEngine;

public class Actor : MonoBehaviour
{

    private void Start()
    {
        NPC npc = this.GetComponent<NPC>();
        if(npc != null)
        {
            npc.CurrentState = NPC.EnemyState.Docile;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Input.GetButtonDown("Interact"))
        {
            
        }
    }

}
