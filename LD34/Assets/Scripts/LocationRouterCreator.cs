using UnityEngine;
using System.Collections;

using Synctory.Routers;

public class LocationRouterCreator : MonoBehaviour {

	void Awake () {
        for (int i = 0; i < Synctory.Synctory.LocationsRoot.transform.childCount; i++) {
            GameObject go = new GameObject();
            go.transform.parent = transform;
            go.name = string.Format("{0}", i);

            LocationRouter router = go.AddComponent<LocationRouter>();
            router.LocationKey = i;
            go.AddComponent<LDBinder>();
        }
	}
	
	void Update () {
	
	}
}
