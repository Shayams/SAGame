using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System;

public class MoveTowardsTarget : MonoBehaviour {

    public GameObject Target;
    public float Speed;
    public float StopMovingXDelta = 40f;
    public float StopMovingYDelta = 40f;
    public LayerMask[] groundLayers = new LayerMask[0];

    private int _randomMovingDir = 1;
    void Start()
    {
        // Range - Max in Exclusive and not inclusize https://docs.unity3d.com/ScriptReference/Random.Range.html
        Func<int> plusMinusGenerator = () => UnityEngine.Random.Range(0, 2) * 2 - 1;

        _randomMovingDir = plusMinusGenerator();

        ObservableCollision2DTrigger collisionObservable = gameObject.GetComponent<ObservableCollision2DTrigger>();
        collisionObservable = collisionObservable == null ? gameObject.AddComponent<ObservableCollision2DTrigger>() : collisionObservable;

        collisionObservable
            .OnCollisionEnter2DAsObservable()
            .Where(coll => coll.gameObject.GetComponent<BoxCollider2D>() != null)
            .Select(coll => coll.gameObject.GetComponent<BoxCollider2D>())
            .Where(boxColl => groundLayers.Any(layer => boxColl.IsTouchingLayers(layer.value)))
            .Select(_ => plusMinusGenerator())
            .Subscribe(randDirection => _randomMovingDir = randDirection);
    }

	void Update () {
        transform.Translate(GetXAxisMovingValue(), 0, 0);
	}

    
    private float GetXAxisMovingValue()
    {
        if (ShouldMoveOnYAxis())
        {
            return Speed * Time.deltaTime * _randomMovingDir;
        }
        else if (ShouldMoveOnXAxis())
        {
            bool isTargetOnRight = Target.transform.position.x > transform.position.x;
            return Speed * Time.deltaTime * (isTargetOnRight ? 1 : -1);
        }
        

        return 0;
    }

    private bool ShouldMoveOnXAxis()
    {
        return Mathf.Abs(Target.transform.position.x - transform.position.x) > StopMovingXDelta;
    }

    private bool ShouldMoveOnYAxis()
    {
        return Mathf.Abs(Target.transform.position.y - transform.position.y) > StopMovingYDelta;
    }
}
