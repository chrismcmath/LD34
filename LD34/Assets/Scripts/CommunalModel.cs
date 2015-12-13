using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CommunalModel : TextViewModel {
    public Color ColorA;
    public Color ColorB;

    public string _ChoiceLine;

    public void Update() {
        if (_ChoiceLine != null) {
            UpdateAlpha();
            string line = FormatChoiceLine(_ChoiceLine);

            _Text.text = _Model + line;
        }
    }

    public void AddLine(string line) {
        _ChoiceLine = null;

        _Model += FormatLine(line);
        _Text.text = _Model;
    }

    public void AddChoiceLine(string line) {
        _ChoiceLine = line;
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
