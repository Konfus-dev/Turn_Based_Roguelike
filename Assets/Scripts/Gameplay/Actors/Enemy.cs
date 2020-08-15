using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int HP = 1;                            
    public int Damage = 1;
    public float MoveTime = 0.1f;

    private Animator Animator;                   
	private Transform Target;                          
    Vector3 MoveTo;                                    
    private bool SkipMove;                             

    protected void Start()
    {
        //animator = GetComponent<Animator>();

        GameManager.Instance.AddEnemyToList(this);

        Target = GameObject.Find("Player").transform;
    }

    public void ManageHealth(int loss, int gain)
    {
        //Set the trigger for the player animator to transition to the playerHit animation.
        //animator.SetTrigger("Hit");
        if (loss > 0)
        {
            //animator.SetTrigger("Hit");
            HP -= loss;

            if (HP <= 0)
            {
                //TODO: play particle effect here
                gameObject.SetActive(false);
            }
        }
        else
        {
            //Set the trigger for player get health
            //somthing to do with a health particle effect probably
            HP += gain;
        }
    }
	
	public void MoveEnemy()
	{
        Debug.Log("enemy moving");

        float distance = Vector3.Distance(Target.transform.position, this.transform.position);
		
	}

    private void RandomMovePos()
    {
        Random rnd = new Random();
        int rand = Random.Range(0, 4);
        if (rand == 0)
            MoveTo = Vector3.forward;
        else if (rand == 1)
            MoveTo = Vector3.left;
        else if (rand == 2)
            MoveTo = Vector3.right;
        else if (rand == 3)
            MoveTo = Vector3.back;
    }
}
