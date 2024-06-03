using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScript : MonoBehaviour
{
    public Image image;
    public Sprite oldImage;
    public Sprite newImage;
    public LineOfSight lineOfSight;
    public Button buttonSwitch;


    private void Update()
    {
        if (lineOfSight.hasLineOfSight)
        {
            buttonSwitch.image.color = new Color(255f, 255f, 255f, 255f);
            SwitchImage();
        }
        else
        {
            buttonSwitch.image.color = new Color(255f, 255f, 255f, .50f);
            SwitchBackImage();
        }
    }
    public void SwitchImage()
    {
        image.sprite = newImage;
    }    
    public void SwitchBackImage()
    {
        image.sprite = oldImage;
    }
}
