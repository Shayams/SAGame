using UnityEngine;
using UniRx;

public class HealthBarUpdater : MonoBehaviour {

	void Start () {
        var healthBar = GetComponent<HealthBar>();
        var lifeCounter = GetComponentInParent<LifeCounter>();

        if (lifeCounter != null && healthBar != null)
        {
            healthBar.CurrentHealthPoints = lifeCounter.Life;
            Debug.Log("created");
            var subscriber = lifeCounter.IsCountingStarted
                .DoOnCompleted(()=>
                    lifeCounter.LifeObservable
                    .Subscribe(currentLife => 
                    healthBar.CurrentHealthPoints = currentLife)
                    )
                .Subscribe();
        }
	}
}
