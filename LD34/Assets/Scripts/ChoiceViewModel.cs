using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoiceViewModel : TextViewModel {
    public delegate void ChoiceHandler(ChoiceTuple choice);

    public static event ChoiceHandler OnChoiceSelected;

    private ChoiceTuple _Tuple;

    public void Start() {
        ChoiceViewModel.OnChoiceSelected += WipeChoice;
    }

    public void SetTuple(ChoiceTuple tuple) {
        _Tuple = tuple;
        UpdateText();
    }

    public void Select() {
        if (OnChoiceSelected != null) {
            OnChoiceSelected(_Tuple);
        }
    }

    public void WipeChoice(ChoiceTuple tuple) {
        _Text.text = string.Empty;
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
}
