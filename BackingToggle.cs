using UnityEngine;
using UnityEngine.UI;

public class BackingToggle : MonoBehaviour
{
    public Text backingText;

    public void ToggleBacking()
    {
        RippleController.backingType = (RippleController.backingType == "air") ? "liquid" : "air";
        backingText.text = "Backing: " + RippleController.backingType.ToUpper();
    }
}
