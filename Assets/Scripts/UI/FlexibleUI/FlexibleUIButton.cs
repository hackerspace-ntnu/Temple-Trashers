using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ButtonType
{
    DEFAULT,
    CONFIRM,
    DECLINE,
    WARNING,
}

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class FlexibleUIButton : FlexibleUI
{
    Image image;
    Button button;
    public ButtonType buttonType;

    public Image Icon { get; set; }

    protected override void OnSkinUI()
    {
        base.OnSkinUI();

        image = GetComponent<Image>();
        button = GetComponent<Button>();
        // icon = transform.Find("Icon").GetComponent<Image>();

        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        //Setting the UI-objects to match the scriptableObject:
        //image.sprite = skinData.buttonSprite;
        image.type = Image.Type.Sliced;
        button.spriteState = skinData.buttonSpriteState;

        switch (buttonType)
        {
            case ButtonType.CONFIRM:
                image.color = skinData.confirmColor;
                // icon.sprite = skinData.confirmIcon;
                break;

            case ButtonType.DECLINE:
                image.color = skinData.declineColor;
                //  icon.sprite = skinData.declineIcon;
                break;

            case ButtonType.WARNING:
                image.color = skinData.warningColor;
                //  icon.sprite = skinData.warningIcon;
                break;

            case ButtonType.DEFAULT:
                image.color = skinData.defaultColor;
                //  icon.sprite = skinData.defaultIcon;
                break;
        }
    }
}
