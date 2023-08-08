using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace MenuManagement
{
    public abstract class UIAnimationHelper
    {
        public static IEnumerator ZoomIn(RectTransform transform, float speed, [CanBeNull] UnityEvent onEnd)
        {
            float time = 0f;
            while (time < 1f)
            {
                transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
            
            transform.localScale = Vector3.one;
            
            onEnd?.Invoke();
        }
        
        public static IEnumerator ZoomOut(RectTransform transform, float speed, [CanBeNull] UnityEvent onEnd)
        {
            float time = 0f;
            while (time < 1f)
            {
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
            
            transform.localScale = Vector3.zero;
            
            onEnd?.Invoke();
        }

        public static IEnumerator FadeIn(CanvasGroup canvasGroup, float speed, [CanBeNull] UnityEvent onEnd)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            
            float time = 0f;
            while (time < 1f)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
            
            canvasGroup.alpha = 1;
            
            onEnd?.Invoke();
        }
        
        public static IEnumerator FadeOut(CanvasGroup canvasGroup, float speed, [CanBeNull] UnityEvent onEnd)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            
            float time = 0f;
            while (time < 1f)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, time);
                yield return null;
                time += Time.deltaTime * speed;
            }
            
            canvasGroup.alpha = 0;
            
            onEnd?.Invoke();
        }
        
        public static IEnumerator SlideIn(RectTransform transform, Direction direction, float speed,
            [CanBeNull] UnityEvent onEnd)
        {
            var rect = transform.rect;
            Vector2 startPosition = direction switch
            {
                Direction.Up => new Vector2(0, -rect.height),
                Direction.Right => new Vector2(-rect.width, 0),
                Direction.Down => new Vector2(0, rect.height),
                Direction.Left => new Vector2(rect.width, 0),
                _ => new Vector2(0, -rect.height)
            };
            float time = 0f;
            while (time < 1f)
            {
                transform.anchoredPosition = Vector2.Lerp(startPosition, Vector2.zero, time);
                yield return null;
                time += Time.deltaTime * speed;
            }

            transform.anchoredPosition = Vector2.zero;
            
            onEnd?.Invoke();
        }    
        
        public static IEnumerator SlideOut(RectTransform transform, Direction direction, float speed,
            [CanBeNull] UnityEvent onEnd)
        {
            var rect = transform.rect;
            Vector2 endPosition = direction switch
            {
                Direction.Up => new Vector2(0, -rect.height),
                Direction.Right => new Vector2(-rect.width, 0),
                Direction.Down => new Vector2(0, rect.height),
                Direction.Left => new Vector2(rect.width, 0),
                _ => new Vector2(0, -rect.height)
            };
            float time = 0f;
            while (time < 1f)
            {
                transform.anchoredPosition = Vector2.Lerp(Vector2.zero, endPosition, time);
                yield return null;
                time += Time.deltaTime * speed;
            }

            transform.anchoredPosition = endPosition;
            
            onEnd?.Invoke();
        } 
        
    }
}