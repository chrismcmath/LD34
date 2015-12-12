using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class TextModel : MonoBehaviour {
    public float TweenSpeed = 1.5f;

    protected string _Model;
    protected string _CurrentLine;
    protected Text _Text;

    protected float _Alpha = 0f;
    protected bool _AlphaTweenFlipFlop = false;

	void Start () {
        _Text = GetComponent<Text>();
	}
	
    public void AddLine(string line) {
        _Model += FormatLine(_CurrentLine);

        _CurrentLine = line;
    }

    protected void UpdateAlpha() {
        if (_AlphaTweenFlipFlop && _Alpha < 1f) {
            _Alpha += Time.deltaTime * TweenSpeed;
        } else if (!_AlphaTweenFlipFlop && _Alpha > 0f) {
            _Alpha -= Time.deltaTime * TweenSpeed;
        } else {
            _AlphaTweenFlipFlop = !_AlphaTweenFlipFlop;
        }
    }

    protected abstract string FormatLine(string line);

    protected string FormatChoiceLine(string line) {
        return FormatLine(line, GetChoiceColor());
    }

    protected string FormatLine(string line, string color) {
        return string.Format("\n<color=#{0}>{1}</color>", color, line);
    }

    protected string GetChoiceColor() {
        float a = 0.5f + (_Alpha * 0.5f);
        return Helper.ColorToHex(new Color(a, a, a));
    }
}
