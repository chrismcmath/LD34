using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChoiceTuple {
    public string Text;
    public int Destination;

    public ChoiceTuple(string text, int dest) {
        Text = text;
        Destination = dest;
    }
}

public class StepParser : MonoBehaviour {
    public const char TRUTH_PREFIX = '+';
    public const char LIE_PREFIX = '-';

    public CommunalModel Communal;
    public ChoiceViewModel TruthVM;
    public ChoiceViewModel LieVM;
    public float Delay = 0.5f;

    private ChoiceTuple _TruthTuple;
    private ChoiceTuple _LieTuple;
    private Queue<string> _Lines = new Queue<string>();

	void Awake () {
        LDBinder.OnNewStep += OnNewStep;
    }

    public void OnNewStep(string text) {
        _TruthTuple = null;
        _LieTuple = null;
        ParseText(text);
        StartCoroutine(TriggerDisplay());
    }

    private void ParseText(string text) {
        string[] components = text.Split('\n');

        for (int i = 0; i < components.Length; i++) {
            string line = components[i];
            if (line != null && line != string.Empty) {
                if (!IsChoice(line)) {
                    _Lines.Enqueue(line);
                }
            }
        }
    }

    private bool IsChoice(string line) {
        if (line.Length <= 0) return false;

        if (line[0] == TRUTH_PREFIX) {
            _TruthTuple = GetTupleFromLine(line);
            return true;
        } else if (line[0] == LIE_PREFIX) {
            _LieTuple = GetTupleFromLine(line);
            return true;
        }

        return false;
    }

    private ChoiceTuple GetTupleFromLine(string line) {
        int destStartIndex = line.IndexOf('(') + 1;
        int destEndIndex = line.IndexOf(')');
        int destStringLength = destEndIndex - destStartIndex; 
        string destString = line.Substring(destStartIndex, destStringLength);

        string cleanLine = line.Substring(2, line.Trim().Length - 3 - 2).Trim();

        ChoiceTuple tuple = new ChoiceTuple(cleanLine, int.Parse(destString));
        return tuple;
    }

    private IEnumerator TriggerDisplay() {
        while (_Lines.Count > 0) {
            Communal.AddLine(_Lines.Dequeue());
            yield return new WaitForSeconds(Delay);
        }

        Communal.AddChoiceLine(string.Format("{0}\n{1}", _TruthTuple.Text, _LieTuple.Text));

        TruthVM.SetTuple(_TruthTuple);
        LieVM.SetTuple(_LieTuple);
    }
}
