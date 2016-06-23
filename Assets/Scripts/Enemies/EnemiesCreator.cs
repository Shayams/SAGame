using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class EnemiesCreator : ScriptableObject
    {
        public GameObject Enemy { get; private set; }
        public GameObject Player { get; private set; }

        public EnemiesCreator(GameObject player, GameObject enemyPrefab)
        {
            Player = player;
            Enemy = Instantiate(enemyPrefab);

            Enemy.SetActive(false);
            Enemy.transform.SetParent(Player.transform.parent);
        }
    }

    public static class EnemiesCreatorExtentions
    {
        private static float GetPlayerWidth(GameObject player)
        {
            var playerRenderer = player.GetComponent<RectTransform>();
            return playerRenderer != null ? playerRenderer.rect.width : 0;
        }

        private static Rect GetEnemyRectBounds(float playerXPosition, float minXPosition, float maxXPosition, float playerWidth)
        {
            var spaceNeedForEnemy = playerWidth + playerWidth / 2;

            var canPlaceOnLeftSide = Mathf.Abs(playerXPosition - minXPosition) > spaceNeedForEnemy;
            var canPlaceOnRightSide = Mathf.Abs(maxXPosition - playerXPosition) > spaceNeedForEnemy;

            var leftSideRect = new Rect(minXPosition, 0, playerXPosition - minXPosition, 0);
            var rightSideRect = new Rect(playerXPosition, 0, maxXPosition - playerXPosition, 0);

            Rect selectedRect = canPlaceOnRightSide ? rightSideRect : leftSideRect;
            if (canPlaceOnLeftSide && canPlaceOnRightSide) selectedRect = Random.Range(0, 100) > 50 ? rightSideRect : leftSideRect;

            return selectedRect;
        }

        public static EnemiesCreator WithRandomPosition(this EnemiesCreator enemiesCreator)
        {
            var playerPosition = enemiesCreator.Player.transform.position;

            var minX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMin, 0, Camera.main.transform.position.z)).x;
            var maxX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMax, 0, Camera.main.transform.position.z)).x;

            var enemyBounds = GetEnemyRectBounds(playerPosition.x, minX, maxX, GetPlayerWidth(enemiesCreator.Player));


            enemiesCreator.Enemy.transform.position = new Vector3()
            {
                x = Random.Range(enemyBounds.xMin, enemyBounds.xMax),
                y = 0
            };

            return enemiesCreator;
        }

        public static EnemiesCreator MoveTowardsTarget(this EnemiesCreator enemiesCreator, GameObject target)
        {
            var moveTowardsTarget = enemiesCreator.Enemy.GetOrAddComponent<MoveTowardsTarget>();
            moveTowardsTarget.Target = target;

            return enemiesCreator;
        }

        public static EnemiesCreator Activate(this EnemiesCreator enemiesCreator)
        {
            enemiesCreator.Enemy.SetActive(true);

            return enemiesCreator;
        }
    }
}
