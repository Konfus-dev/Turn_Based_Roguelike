using Doozy.Engine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;

public class UIManager : MonoBehaviour
{
    //[Tooltip("Doozy UI View here")]
    private UIView PlayerUIView;
    //[Tooltip("Image from UIView here")]
    private Image PlayerUIImage;
    //[Tooltip("Text from UIView here")]
    private TMP_Text PlayerUIText;
    //[Tooltip("How long it takes for UI to go away after showing")]
    private readonly float AutoHideDelay = .5f;

    private void Awake()
    {
        PlayerUIView = this.GetComponent<UIView>();
        PlayerUIImage = this.GetComponent<Image>();
        PlayerUIText = this.GetComponentInChildren<TMP_Text>();
        PlayerUIView.ViewName = this.transform.parent.parent.name;
        PlayerUIView.AutoHideAfterShowDelay = AutoHideDelay;
        PlayerUIView.AutoHideAfterShow = true;
    }

    /*private void Update()
    {
        if(transform.parent.parent.tag != "NPC" && transform.parent.parent.tag != "Player")
        {
            transform.rotation = Quaternion.Euler(transform.parent.parent.up);
            transform.position = transform.parent.parent.position + Vector3.up / 2;
        }
    }*/
    
    // Sets UI visiblity to true
    public void Activate()
    {
        PlayerUIView.SetVisibility(true);
    }

    // Sets UI visiblity to false
    public void DeActivate()
    {
        PlayerUIView.SetVisibility(false);
    }

    // Shows UI (will auto hide after HideTime: a variable set publicly in PlayerUIManager) 
    public void ShowItem()
    {
        PlayerUIView.Show();
    }

    // Hides UI 
    public void HideItem()
    {
        PlayerUIView.Hide();
    }

    // Set auto whether or not to auto hide
    public void AutoHideItem(bool autoHide)
    {
        PlayerUIView.AutoHideAfterShow = autoHide;
    }

    // Set auto whether or not to auto hide
    public void setAutoHideTime(float autoHideDelay)
    {
        PlayerUIView.AutoHideAfterShowDelay = autoHideDelay;
    }

    // Sets UI icon
    public void SetIcon(Sprite icon)
    {
        PlayerUIImage.sprite = icon;
    }

    // Sets UI text (Example 'E to pickup' or 'E to open door' or just 'E')
    public void SetText(string text)
    {
        PlayerUIText.text = text;
    }
}
