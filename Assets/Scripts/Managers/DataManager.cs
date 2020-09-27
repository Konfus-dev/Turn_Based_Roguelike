using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Data/EntityData")]
public class DataManager : ScriptableObject
{
    //for the player, all of this should be initialized in the start menu, like their name and initial starting position.
    public string entityName = "[standard name]";
    public ActorStats entityStats;
    public List<ItemData> entityInventory;
    public List<ItemData> entityEquippedItems;
    public Vector3 loadingPosition;
    public string latestAttacker;
    public string latestAttackedByMe;
    //public Dictionary<int, byte[]> data;


}