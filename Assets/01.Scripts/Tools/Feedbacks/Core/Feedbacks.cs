using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Tools
{
    public class Feedbacks : MonoBehaviour
    {
        private Feedback[] _feedbacksList = new Feedback[] { };

        private void Awake()
        {
            _feedbacksList = GetComponents<Feedback>();
        }

        public virtual void PlayFeedbacks()
        {
            foreach (Feedback feedback in _feedbacksList)
            {
                feedback.PlayFeedback();
            }
        }

        public virtual void StopFeedbacks()
        {
            foreach (Feedback feedback in _feedbacksList)
            {
                feedback.StopFeedback();
            }
        }

        public virtual T FindFeedback<T>() where T : Feedback
        {
            Type typeOfSearchAb = typeof(T);
            foreach (Feedback feedback in _feedbacksList)
            {
                if (feedback is T requiredFeedback)
                    return requiredFeedback;
            }
            return null;
        }

    }

}
