using System.Collections;
using UnityEngine;

namespace MenuManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class Page : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        [SerializeField] private float animationSpeed = 1f;
        public bool exitOnNewPagePush = false;

        [SerializeField]
        private MovementMode entryMode = MovementMode.None;
        [SerializeField] 
        private Direction entryDirection = Direction.Left;
        [SerializeField]
        private MovementMode exitMode = MovementMode.None;
        [SerializeField]
        private Direction exitDirection = Direction.Left;
        
        private Coroutine _animationCoroutine;
        private Coroutine _audioCoroutine;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Enter(bool playAudio)
        {
            switch (entryMode)
            {
                case MovementMode.None:
                    Appear(playAudio);
                    break;
                case MovementMode.Slide:
                    SlideIn(playAudio);
                    break;
                case MovementMode.Zoom:
                    ZoomIn(playAudio);
                    break;
                case MovementMode.Fade:
                    FadeIn(playAudio);
                    break;

            }
        }
        
        public void Exit(bool playAudio)
        {
            switch (exitMode)
            {
                case MovementMode.None:
                    Disappear(playAudio);
                    break;
                case MovementMode.Slide:
                    SlideOut(playAudio);
                    break;
                case MovementMode.Zoom:
                    ZoomOut(playAudio);
                    break;
                case MovementMode.Fade:
                    FadeOut(playAudio);
                    break;

            }
        }

        private void Appear(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }
            
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;
            _canvasGroup.alpha = 1f;
        }
        
        private void Disappear(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            var rect = _rectTransform.rect;
            Vector2 endPosition = exitDirection switch
            {
                Direction.Up => new Vector2(0, -rect.height),
                Direction.Right => new Vector2(-rect.width, 0),
                Direction.Down => new Vector2(0, rect.height),
                Direction.Left => new Vector2(rect.width, 0),
                _ => new Vector2(0, -rect.height)
            };
            
            _rectTransform.anchoredPosition = endPosition;
            _rectTransform.localScale = Vector3.zero;
            _canvasGroup.alpha = 0f;
        }

        private void SlideIn(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine =
                StartCoroutine(UIAnimationHelper.SlideIn(_rectTransform, entryDirection, animationSpeed, null));
        }       
        
        private void SlideOut(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine =
                StartCoroutine(UIAnimationHelper.SlideOut(_rectTransform, exitDirection, animationSpeed, null));
        }     
        
        private void ZoomIn(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine =
                StartCoroutine(UIAnimationHelper.ZoomIn(_rectTransform, animationSpeed, null));
        }       
        
        private void ZoomOut(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine =
                StartCoroutine(UIAnimationHelper.ZoomOut(_rectTransform, animationSpeed, null));
        }      
        
        private void FadeIn(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine =
                StartCoroutine(UIAnimationHelper.FadeIn(_canvasGroup, animationSpeed, null));
        }       
        
        private void FadeOut(bool playAudio)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine =
                StartCoroutine(UIAnimationHelper.FadeOut(_canvasGroup, animationSpeed, null));
        }
    }
}
