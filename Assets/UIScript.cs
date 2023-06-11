using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
      // Set the size of the canvas to the size of the screen
      // This is used to make the UI elements responsive to the screen size
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
