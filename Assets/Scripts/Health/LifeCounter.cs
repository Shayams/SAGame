using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class LifeCounter : MonoBehaviour
{
    public int Life;
    public IObservable<int> LifeObservable;
    // Ugly workaround to know when the Start function is done ( we cannot move the LifeObservable creation to ctor cause it does not have the gameObject
    public ISubject<bool> IsCountingStarted;
    public Common.TagsEnum[] TagsToIgnoreOnCollision;


    public LifeCounter()
    {
        IsCountingStarted = new Subject<bool>();
    }

    void Start()
    {
        ObservableCollision2DTrigger collisionObservable = gameObject.GetComponent<ObservableCollision2DTrigger>();
        collisionObservable = collisionObservable == null ? gameObject.AddComponent<ObservableCollision2DTrigger>() : collisionObservable;

        LifeObservable = collisionObservable
            .OnCollisionEnter2DAsObservable()
            .Where(coll => coll.gameObject.GetComponent<DamageInfo>() != null)
            .Where(coll => TagsToIgnoreOnCollision.All(tag => coll.gameObject.tag != tag.ToString()))
            .Select(coll => coll.gameObject.GetComponent<DamageInfo>().DamageHitPoints)
            .Do(damadgeHitPoints=>Life-=damadgeHitPoints)
            .Select(_ => Life)
            .Share();

        IsCountingStarted.OnCompleted(); // Publish that the LifeObservable is initialized
        
        LifeObservable.Where(currentLife => currentLife <= 0)
            .Subscribe(_ => Destroy(gameObject));
    }
}
