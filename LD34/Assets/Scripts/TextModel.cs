using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextModel : MonoBehaviour {
    public Color ColorA;
    public Color ColorB;
    public float TweenSpeed = 1f;

    private string _Model;
    private string _CurrentLine;
    private Text _Text;

    private float _Alpha = 0f;
    private bool _AlphaTweenFlipFlop = false;

	void Start () {
        _Text = GetComponent<Text>();
	}
	
    public void AddLine(string line) {
        _Model += FormatStaticLine(_CurrentLine);

        _CurrentLine = line;
    }

    public void Update() {
        if (_CurrentLine != null) {
            UpdateAlpha();
            string line = FormatChoiceLine(_CurrentLine);

            _Text.text = _Model + line;
        }
    }

    private void UpdateAlpha() {
        if (_AlphaTweenFlipFlop && _Alpha < 1f) {
            _Alpha += Time.deltaTime * TweenSpeed;
        } else if (!_AlphaTweenFlipFlop && _Alpha > 0f) {
            _Alpha -= Time.deltaTime * TweenSpeed;
        } else {
            _AlphaTweenFlipFlop = !_AlphaTweenFlipFlop;
        }
    }

    private string FormatStaticLine(string line) {
        return FormatLine(line, GetCurrentColor());
    }

    private string FormatChoiceLine(string line) {
        return FormatLine(line, GetChoiceColor());
    }

    private string FormatLine(string line, string color) {
        return string.Format("\n<color=#{0}>{1}</color>", color, line);
    }

    private string GetCurrentColor() {
        if (true) {
            return Helper.ColorToHex(ColorA);
        }
    }

    private string GetChoiceColor() {
        return Helper.ColorToHex(new Color(_Alpha, _Alpha, _Alpha));
    }
}
