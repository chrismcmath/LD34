using UnityEngine;
using System.Collections;

public class TransformPair {
    public Vector3 PrevTarget;

    private Transform _t;
    public Transform t {
        get { return _t; }
        set {
            _t = value;
            _Target = _t.position;
        }
    }

    private Vector3 _Target;
    public Vector3 Target {
        get { return _Target; }
        set {
            PrevTarget = Target;
            _Target = value;
        }
    }
}

public class Switcher : MonoBehaviour {
    public Transform A;
    public Transform B;

    public float Period = 1f;

    private TransformPair _PairA;
    private TransformPair _PairB;

    private float _SwitchTime = 0f;

    public bool Switch = false;

    public void Start() {
        _PairA = new TransformPair();
        _PairA.t = A;

        _PairB = new TransformPair();
        _PairB.t = B;
    }

    public void Update() {
        if (Switch) {
            _SwitchTime = Time.realtimeSinceStartup;

            Vector3 targetA = _PairA.Target;
            _PairA.Target = _PairB.Target;
            _PairB.Target = targetA;

            Switch = false;
        }

        float delta = (Time.realtimeSinceStartup - _SwitchTime) / Period;
        if (delta < 1f) {
            TweenPair(_PairA, delta);
            TweenPair(_PairB, delta);
        } else {
            SnapPair(_PairA);
            SnapPair(_PairB);
        }
    }

    private void TweenPair(TransformPair pair, float delta) {
        pair.t.position = (pair.Target - pair.PrevTarget) * delta + pair.PrevTarget;

        float doubleDelta = delta*2f;
        float scaleDelta = -0.5f * Mathf.Pow((doubleDelta -1f), 2f) + 0.5f;
        scaleDelta = pair == _PairA ? -scaleDelta : scaleDelta;

        pair.t.localScale = Vector3.one + Vector3.one * scaleDelta;
    }

    private void SnapPair(TransformPair pair) {
        pair.t.position = pair.Target;
        pair.t.localScale = Vector3.one;
    }

}
