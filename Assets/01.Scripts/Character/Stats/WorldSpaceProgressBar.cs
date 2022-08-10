using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.UI;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class WorldSpaceProgressBar : MonoBehaviour
    {
        [Header("Bar Spawning")]
        public Canvas Canvas;
        public ProgressBar ProgressBarPrefab;
        public Vector3 ProgressBarOffset;
        public bool ParentCanvasUnderThis = true;  //Unparent the canvas from this object.

        protected ProgressBar _progressBar;
        protected Canvas _canvas;

        protected virtual void Update()
        {
            _progressBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + ProgressBarOffset);
        }

        public virtual void AssignValue(FloatValue value)
        {
            CreateSliders();
            SetSlidersValue(value);
        }

        /// <summary>
        /// Instantiate the bars from prefabs.
        /// </summary>
        public virtual void CreateSliders()
        {
            if (_progressBar == null)
            {
                _canvas = Instantiate(Canvas, transform.position, Quaternion.identity);
                if (ParentCanvasUnderThis)
                    _canvas.transform.SetParent(this.transform);
                _progressBar = Instantiate(ProgressBarPrefab, transform.position, Quaternion.identity, _canvas.transform);
                _progressBar.transform.SetParent(_canvas.transform);
            }
        }

        /// <summary>
        /// Set the 2 sliders' max value.
        /// </summary>
        public virtual void SetSlidersValue(FloatValue newValue)
        {
            if (_progressBar != null)
            {
                _progressBar.AssignStoredValue(newValue);
            }
        }
    }
}

