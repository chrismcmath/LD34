using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class TextViewModel : MonoBehaviour {
    public static Color ColorA;
    public static Color ColorB;

    /*
    public const string ColorAHex = "#9578A8";
    public const string ColorBHex = "#89B292";
    */

    public const string ColorAHex = "#76BB57";
    public const string ColorBHex = "#E7766F";

    public float TweenSpeed = 1.5f;

    protected string _Model;
    protected Text _Text;

    protected float _Alpha = 0f;
    protected bool _AlphaTweenFlipFlop = false;

	void Awake () {
        ColorA = Helper.HexToColor(ColorAHex);
        ColorB = Helper.HexToColor(ColorBHex);

        _Text = GetComponent<Text>();
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

    protected string GetCurrentColor() {
        if (StepController.CurrentPlayer == StepController.Player.ONE) {
            return Helper.ColorToHex(TextViewModel.ColorA);
        } else {
            return Helper.ColorToHex(TextViewModel.ColorB);
        }
    }

    protected abstract string FormatLine(string line);

    protected string FormatLine(string line, string color) {
        return string.Format("<color=#{0}>{1}</color>", color, line);
    }

    protected string FormatQuotation(string line, string color) {
        return string.Format("<color=#{0}>\"{1}\"</color>", color, line);
    }
}
