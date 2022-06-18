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
                tankTransitions[type].duration = 0.5f;
                sequence.Append(tankTransitions[type].Move());
            }

            sequence.onComplete = OnAnimationFinished;
            sequence.PlayForward();
        }

        public void ShowTank(int type)
        {
            tankTransitions[type].duration = 1f;
            tankTransitions[type].Move();
        }

        private void OnAnimationFinished()
        {
            AnimationFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}