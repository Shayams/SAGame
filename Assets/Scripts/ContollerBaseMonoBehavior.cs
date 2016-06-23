using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class ContollerBaseMonoBehavior : MonoBehaviour
    {
        public GameObject Player;

        protected BoxCollider2D _playerCollider;

        virtual protected void Start()
        {
            Init();
        }

        virtual public void Init()
        {
            if (Player == null) return;

            _playerCollider = Player.GetComponent<BoxCollider2D>();
        }
    }
}
