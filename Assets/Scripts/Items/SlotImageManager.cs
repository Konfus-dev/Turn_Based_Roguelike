using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotImageManager : MonoBehaviour
{
    public Image Icon;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount > 0) Icon.gameObject.SetActive(false);
        else Icon.gameObject.SetActive(true);
    }
}
