using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CommunalModel : TextModel {
    public Color ColorA;
    public Color ColorB;

    public void Update() {
        if (_CurrentLine != null) {
            UpdateAlpha();
            string line = FormatChoiceLine(_CurrentLine);

            _Text.text = _Model + line;
        }
    }

    protected override string FormatLine(string line) {
        return FormatStaticLine(line);
    }

    private string FormatStaticLine(string line) {
        return FormatLine(line, GetCurrentColor());
    }

    private string GetCurrentColor() {
        if (true) {
            return Helper.ColorToHex(ColorA);
        }
    }
}
