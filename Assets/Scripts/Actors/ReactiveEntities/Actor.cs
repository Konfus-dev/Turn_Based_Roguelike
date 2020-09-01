using UnityEngine;

public class Actor : MonoBehaviour
{

    private NPC npc;

    private void Start()
    {
        npc = this.GetComponent<NPC>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetButtonDown("Interact"))
        {
            
        }
    }

}
