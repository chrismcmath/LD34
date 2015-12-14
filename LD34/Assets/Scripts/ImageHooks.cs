using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class ImageHooks : MonoBehaviour {
    public const float HIGHLIGHT = 0.15f;

    private Image img;

    public void Awake() {
        img = GetComponent<Image>();
    }

    public void SetHighlighted() {
        img.color = new Color(HIGHLIGHT, HIGHLIGHT, HIGHLIGHT);
    }

    public void SetUnhighlighted() {
        img.color = Color.black;
    }
}
