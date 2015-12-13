using UnityEngine;
using System.Collections;

using Synctory;
using Synctory.Binders;
using Synctory.Routers;

[RequireComponent(typeof(LocationRouter))]
public class LDBinder : SynctoryBinder {
    public static int CURRENT_LOCATION = 0;

    public delegate void NewStepHandler(string text);

    public static event NewStepHandler OnNewStep;

    public override void UpdateInfo(SynctoryFrameInfo info) {
        if (GetComponent<LocationRouter>().LocationKey == CURRENT_LOCATION) {
            Debug.LogFormat("[{0}]: {1}", GetComponent<LocationRouter>().LocationKey, info.Unit.Key);

            if (OnNewStep != null) {
                OnNewStep(info.Unit.Text);
            }
        }

    }
}
