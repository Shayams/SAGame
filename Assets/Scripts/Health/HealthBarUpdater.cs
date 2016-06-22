using UnityEngine;
using UniRx;

public class HealthBarUpdater : MonoBehaviour {

	void Start () {
        var healthBar = GetComponent<HealthBar>();
        var lifeCounter = GetComponentInParent<LifeCounter>();

        if (lifeCounter != null && healthBar != null)
        {
            healthBar.CurrentHealthPoints = lifeCounter.Life;
            var subscriber = lifeCounter.IsCountingStarted
                .Where(x => x == true)
                .First()
                .Subscribe(_ =>
                    lifeCounter.LifeObservable
                    .Subscribe(currentLife => 
                    healthBar.CurrentHealthPoints = currentLife)
                    );
        }
	}
}
