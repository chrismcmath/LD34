using UnityEngine;
using System.Collections;

using Synctory;
using Synctory.Binders;
using Synctory.Routers;

[RequireComponent(typeof(LocationRouter))]
public class LDBinder : SynctoryBinder {
    public static int CURRENT_LOCATION = 0;

    public override void UpdateInfo(SynctoryFrameInfo info) {
        if (GetComponent<LocationRouter>().LocationKey == CURRENT_LOCATION) {
            Debug.LogFormat("[{0}]: {1}", GetComponent<LocationRouter>().LocationKey, info.Unit.Key);
            GameObject.FindObjectOfType<TextModel>().AddLine(info.Unit.Text);
        }

    }
}
