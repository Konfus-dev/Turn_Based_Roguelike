using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    private Transform CharTransform;
    private float MaxHieght;
    private float MinHieght;

    void Awake()
    {
        CharTransform = this.GetComponent<Transform>();
        MaxHieght = CharTransform.localScale.y;
        MinHieght = MaxHieght * .95f;
        CharTransform.DOScale(new Vector3(CharTransform.localScale.x, MinHieght, CharTransform.localScale.z), .8f).SetLoops(-1, LoopType.Yoyo);
    }

}
