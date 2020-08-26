using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Interactable
{

    private void Start()
    {
        Enemy enemy = this.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.CurrentState = Enemy.EnemyState.Docile;
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
