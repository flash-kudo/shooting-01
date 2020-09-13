using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace BarrageShooting
{
    public class BombControll : MonoBehaviour
    {
        public PlayableDirector Director;
        public IngameScreenManager Ingame;

        // Start is called before the first frame update
        void Start()
        {
            if (Director == null) RemoveAction();
            Director.stopped += RemoveAction;
        }

        // Update is called once per frame
        void Update()
        {
            if (Director != null)
            {
                if (Director.state == PlayState.Paused) RemoveAction(Director);
            }
        }

        private void RemoveAction(PlayableDirector obj = null)
        {
            if (Director != null)
            {
                Director.stopped -= RemoveAction;
            }
            this.gameObject.SetActive(false);
            Ingame.OnBombButton0();
        }
    }
}
