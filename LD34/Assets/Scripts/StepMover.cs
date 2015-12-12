using UnityEngine;
using System.Collections;

public class StepMover : MonoBehaviour {

    public int StepKey = 0;
    public bool IncreaseStep = false;

	public void Start() {
        Synctory.Synctory.SetStep(StepKey);
	}
	
	public void Update() {
        if (IncreaseStep) {
            StepKey++;
            Synctory.Synctory.SetStep(StepKey);
            IncreaseStep = false;
        }
	}
}
