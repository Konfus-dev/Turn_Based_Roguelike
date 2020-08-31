using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public GameObject heart;

    public void AddHearts(int num)
    {
        for(int h = 0; h < num; h++)
        {
            GameObject _heart = Instantiate(heart);
            _heart.transform.parent = this.transform;
            _heart.transform.localScale = heart.transform.localScale;
        }
    }

    public void SyncHealth()
    {
        
    }
}
