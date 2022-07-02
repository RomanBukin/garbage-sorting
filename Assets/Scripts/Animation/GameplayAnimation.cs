using System;
using DG.Tweening;
using UnityEngine;

namespace Animation
{
    public class GameplayAnimation : MonoBehaviour
    {
        public bool debug;

        public event EventHandler AnimationFinished;

        [Header("Gameplay start transitions")] [SerializeField]
        private Transition[] initialTransitions;

        [Header("Difficulty change transitions")] [SerializeField]
        private Transition[] tankTransitions;

        private void Awake()
        {
            foreach (var transition in initialTransitions)
            {
                transition.enabled = !debug;
            }
        }

        public void ShowGameplay(int[] tankTypes)
        {
            var sequence = DOTween.Sequence();
            foreach (var transition in initialTransitions)
            {
                sequence.Append(transition.Move());
            }

            if (tankTypes == null)
            {
                return;
            }

            foreach (var type in tankTypes)
            {
                var tankTransition = tankTransitions[type];
                
                tankTransition.gameObject.SetActive(true);
                tankTransition.duration = 0.5f;
                sequence.Append(tankTransition.Move());
            }

            sequence.onComplete = OnAnimationFinished;
            sequence.PlayForward();
        }

        public void ShowTank(int type)
        {
            var tankTransition = tankTransitions[type];
            
            tankTransition.gameObject.SetActive(true);
            tankTransition.duration = 1f;
            tankTransition.Move();
        }

        private void OnAnimationFinished()
        {
            AnimationFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}