using UnityEngine;

public class Statue : ReactiveEntity
{


    public override void Interact<T>(T component)
    {
        Debug.Log("I am Karen");
        if (component.GetComponent<Player>() && Player.Instance.GetState() == Player.PlayerState.Ghosting)
        {
            RevivePlayer();
        }
    }

    private void RevivePlayer()
    {
        //Chat("A quote depending on player stats, like starting class");
        Player.Instance.playerStats.currentHealth = 0;
        for (int i = 0; i < Player.Instance.playerStats.maxHealth; i++)
        {
            Player.Instance.OnHealthChange(0, 1);
        }
        Player.Instance.SetState(Player.PlayerState.Idle, true);
    }
}
