using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoiceViewModel : TextViewModel {

    public Color ColorA;

    private ChoiceTuple _Tuple;

    public void SetTuple(ChoiceTuple tuple) {
        _Tuple = tuple;
        UpdateText();
    }

    private void UpdateText() {
        _Text.text = _Tuple != null ?
            FormatStaticLine(_Tuple.Text) :
            string.Empty;
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
