using UnityEngine;
using System.Collections;

public class StepController : MonoBehaviour {

    public enum Player {ONE=0, TWO}

    public static int CurrentStep = 0;
    public static int CurrentLocation = 0;
    public static Player CurrentPlayer = Player.ONE;
    public static Player ThisPlayer;

	void Start () {
        Synctory.Synctory.SetStep(CurrentStep);

        ChoiceViewModel.OnChoiceSelected += OnChoiceSelected;
	}

    public void OnChoiceSelected(ChoiceTuple tuple) {
        Debug.LogFormat("selected: {0}", tuple.Text);

        CurrentLocation = tuple.Destination;

        if (CurrentPlayer == Player.ONE) {
            CurrentPlayer = Player.TWO;
        } else {
            CurrentPlayer = Player.ONE;
        }

        CurrentStep++;
        Synctory.Synctory.SetStep(CurrentStep);
    }
}
