using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IcelandMoss
{/// <summary>
/// the type of animation
/// </summary>
    public enum AnimationType
    {
        Scale,
        Opacity,
        TranslationX,
        TranslationY,
        Rotation,
        Layout
    }
    /// <summary>
    /// an animation dependancy class
    /// </summary>
    public class Storyboard
    {/// <summary>
    /// a dictionary with animation states
    /// </summary>
        readonly Dictionary<string, ViewTransition[]> _stateTransitions = new Dictionary<string, ViewTransition[]>();
        /// <summary>
        /// add a state of animation
        /// </summary>
        /// <param name="state"></param>
        /// <param name="viewTransitions"></param>
        public void Add(object state, ViewTransition[] viewTransitions)
        {
            var stateStr = state?.ToString().ToUpperInvariant();

            if (string.IsNullOrEmpty(stateStr) || viewTransitions == null)
                throw new NullReferenceException("Value of 'state', 'viewTransitions' cannot be null");

            if (_stateTransitions.ContainsKey(stateStr))
                throw new ArgumentException($"State {state} already added");

            _stateTransitions.Add(stateStr, viewTransitions);
        }
/// <summary>
/// starts the animation step
/// </summary>
/// <param name="newState">new animation state</param>
/// <param name="withAnimation">will be animation</param>
        public void Go(object newState, bool withAnimation = true)
        {
            var newStateStr = newState?.ToString().ToUpperInvariant();

            if (string.IsNullOrEmpty(newStateStr))
                throw new NullReferenceException("Value of newState cannot be null");

            if (!_stateTransitions.ContainsKey(newStateStr))
                throw new KeyNotFoundException($"There is no state {newState}");

            // Get all ViewTransitions 
            var viewTransitions = _stateTransitions[newStateStr];

            // Get transition tasks
            var tasks = viewTransitions.Select(viewTransition => viewTransition.GetTransition(withAnimation));

            // Run all transition tasks
            Task.WhenAll(tasks);
        }
    }
    /// <summary>
    /// a class for making animation with different states
    /// </summary>
    public class ViewTransition
    {
        readonly AnimationType _animationType;
        readonly int _delay;
        readonly uint _length;
        readonly Easing _easing;
        readonly double _endValue;
        readonly Rectangle _rectangle;
        readonly WeakReference<VisualElement> _targetElementReference;
        /// <summary>
        /// a constructor for animation with the last time of animation
        /// </summary>
        /// <param name="targetElement">the element we change</param>
        /// <param name="animationType">the type scene</param>
        /// <param name="endValue">the end value of time</param>
        /// <param name="length">continious of animation</param>
        /// <param name="easing">the type of easing</param>
        /// <param name="delay">the time start time delayed</param>
        public ViewTransition(VisualElement targetElement, AnimationType animationType, double endValue, uint length = 250, Easing easing = null, int delay = 0)
        {
            _targetElementReference = new WeakReference<VisualElement>(targetElement);
            _animationType = animationType;
            _length = length;
            _endValue = endValue;
            _delay = delay;
            _easing = easing;
        }
        /// <summary>
        /// a constructor for animation with the last phase of animation
        /// </summary>
        /// <param name="targetElement">the element we change</param>
        /// <param name="animationType">the type scene</param>
        /// <param name="endLayout">the last position</param>
        /// <param name="length">continious of animation</param>
        /// <param name="easing">the type of easing</param>
        /// <param name="delay">the time start time delayed</param>
        public ViewTransition(VisualElement targetElement, AnimationType animationType, Rectangle endLayout, uint length = 250, Easing easing = null, int delay = 0)
        {
            _targetElementReference = new WeakReference<VisualElement>(targetElement);
            _animationType = animationType;
            _length = length;
            _rectangle = endLayout;
            _delay = delay;
            _easing = easing;
        }
        /// <summary>
        /// The massive method for transition
        /// </summary>
        /// <param name="withAnimation"> can we do animation</param>
        /// <returns>a method to wait for</returns>
        public async Task GetTransition(bool withAnimation)
        {
            VisualElement targetElement;
            if (!_targetElementReference.TryGetTarget(out targetElement))
                throw new ObjectDisposedException("Target VisualElement was disposed");

            if (_delay > 0)
                await Task.Delay(_delay);

            withAnimation &= _length > 0;

            switch (_animationType)
            {
                case AnimationType.Layout:
                    if (withAnimation)
                        await targetElement.LayoutTo(_rectangle, _length, _easing);
                    else
                        await targetElement.LayoutTo(_rectangle, 0, null);
                    break;
                case AnimationType.Scale:
                    if (withAnimation)
                        await targetElement.ScaleTo(_endValue, _length, _easing);
                    else
                        targetElement.Scale = _endValue;
                    break;

                case AnimationType.Opacity:
                    if (withAnimation)
                    {
                        if (!targetElement.IsVisible && _endValue <= 0)
                            break;

                        if (targetElement.IsVisible && _endValue < targetElement.Opacity)
                        {
                            await targetElement.FadeTo(_endValue, _length, _easing);
                            targetElement.IsVisible = _endValue > 0;
                        }
                        else if (targetElement.IsVisible && _endValue > targetElement.Opacity)
                        {
                            await targetElement.FadeTo(_endValue, _length, _easing);
                        }
                        else if (!targetElement.IsVisible && _endValue > targetElement.Opacity)
                        {
                            targetElement.Opacity = 0;
                            targetElement.IsVisible = true;
                            await targetElement.FadeTo(_endValue, _length, _easing);
                        }
                    }
                    else
                    {
                        targetElement.Opacity = _endValue;
                        targetElement.IsVisible = _endValue > 0;
                    }
                    break;

                case AnimationType.TranslationX:
                    if (withAnimation)
                        await targetElement.TranslateTo(_endValue, targetElement.TranslationY, _length, _easing);
                    else
                        targetElement.TranslationX = _endValue;
                    break;

                case AnimationType.TranslationY:
                    if (withAnimation)
                        await targetElement.TranslateTo(targetElement.TranslationX, _endValue, _length, _easing);
                    else
                        targetElement.TranslationY = _endValue;
                    break;

                case AnimationType.Rotation:
                    if (withAnimation)
                        await targetElement.RotateTo(_endValue, _length, _easing);
                    else
                        targetElement.Rotation = _endValue;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
