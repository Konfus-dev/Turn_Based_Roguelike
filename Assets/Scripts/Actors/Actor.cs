using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Interactable
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

    public override void Interact<T>(T component)
    {
        throw new System.NotImplementedException();
    }
}
