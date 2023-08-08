using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MenuManagement
{
    [RequireComponent(typeof(Canvas))]
    [DisallowMultipleComponent]
    public class MenuController : MonoBehaviour
    {
        [SerializeField] 
        private Page initialPage;

        [SerializeField] private GameObject firstFocusItem;
        [SerializeField] private Button backButton;
        
        private Canvas _rootCanvas;
        
        private Stack<Page> _pageStack = new();
        
        private void Awake()
        {
            _rootCanvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            if (firstFocusItem != null)
            {
                EventSystem.current.SetSelectedGameObject(firstFocusItem);
            }

            if (initialPage != null)
            {
                PushPage(initialPage);
            }
            
            if (backButton != null)
            {
                backButton.onClick.AddListener(OnCancel);
                ManageBackButton();
            }
        }

        public void PushPage(Page page)
        {
            page.Enter(true);
            
            if (_pageStack.Count > 0)
            {
                Page currentPage = _pageStack.Peek();

                if (currentPage.exitOnNewPagePush)
                {
                    currentPage.Exit(false);
                }
            }
            
            _pageStack.Push(page);
            ManageBackButton();
        }
        
        public void PopPage()
        {
            if (_pageStack.Count > 1)
            {
                Page page = _pageStack.Pop();
                page.Exit(true);
                
                Page newCurrentPage = _pageStack.Peek();

                if (newCurrentPage.exitOnNewPagePush)
                {
                    newCurrentPage.Enter(false);
                }
            }
            else
            {
                Debug.LogWarning("Cannot pop the last page in the stack.");
            }
            
            ManageBackButton();
        }
        
        public void PopAllPages()
        {
            for (int i = 1; i < _pageStack.Count; i++)
            {
                PopPage();
            }
        }
        
        public bool IsPageInStack(Page page)
        {
            return _pageStack.Contains(page);
        }
        
        public bool IsPageOnTopOfStack(Page page)
        {
            return _pageStack.Count > 0 && _pageStack.Peek() == page;
        }

        private void OnCancel()
        {
            if (_rootCanvas.enabled && _rootCanvas.gameObject.activeInHierarchy)
            {
                if (_pageStack.Count != 0)
                {
                    PopPage();
                }
            }
        }

        private void ManageBackButton()
        {
            if (backButton == null)
            {
                return;
            }
            backButton.gameObject.SetActive(!IsPageOnTopOfStack(initialPage));
        }
    }
}
