using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommunalModel : TextViewModel {
    public const int DASH_MAX = 60;

    public List<string> _ChoiceLines = new List<string>();

    public void Start() {
        ChoiceViewModel.OnChoiceSelected += OnChoiceSelected;
    }

    public void OnChoiceSelected(ChoiceTuple tuple) {
        AddLine(string.Format("\n\"{0}\"\n", tuple.Text));
    }


    public void Update() {
        if (_ChoiceLines.Count > 0) {
            UpdateAlpha();
            string line = FormatChoiceLines(_ChoiceLines);

            _Text.text = _Model + line;
        }
    }

    public void AddLine(string line) {
        _ChoiceLines.Clear();

        _Model += string.Format("\n{0}", FormatLine(line));
        _Text.text = _Model;
    }

    public void AddChoiceLines(List<string> lines) {
        _ChoiceLines = lines;
    }

    protected override string FormatLine(string line) {
        return FormatStaticLine(line);
    }

    private string FormatStaticLine(string line) {
        return FormatLine(line, GetCurrentColor());
    }

    protected string FormatChoiceLines(List<string> lines) {
        int dashCount = (int) Mathf.Max(lines[0].Length, lines[1].Length);
        return string.Format("\n\n{0}\n{1}\n{2}",
                FormatQuotation(lines[0], GetChoiceColor(false)),
                GetDashString(dashCount),
                FormatQuotation(lines[1], GetChoiceColor(true)));
    }

    protected string GetDashString(int count) {
        count = Mathf.Min(count, DASH_MAX);
        string str = "";
        for (int i = 0; i < count; i++) {
            str += "-";
        }
        return FormatLine(str, GetCurrentColor());
    }

    protected string GetChoiceColor(bool inverse) {
        float a;
        if (inverse) {
            a = 1f - _Alpha;
        } else {
            a = _Alpha;
        }

        a = 0.5f + (a * 0.5f);
        return Helper.ColorToHex(new Color(a, a, a));
    }
}
