using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    
    public class View : MonoBehaviour
    {
        [HideInInspector] public CanvasGroup CanvasGroup;
        public Transform Container;
        public bool IsStackable = false;
        protected virtual void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        
        public virtual async void Show()
        {
            GameManager.Instance.GameSpeed = 0;
            await OnShow();
            gameObject.SetActive(true);
            CanvasGroup.interactable = true;
        }

        public virtual async void Hide()
        {
            CanvasGroup.interactable = false;
            await OnHide();
            gameObject.SetActive(false);
            GameManager.Instance.SetPreSpeedGame();
        }

        protected virtual UniTask OnShow()
        {
            return UniTask.CompletedTask;
        }

        protected virtual UniTask OnHide()
        {
            return UniTask.CompletedTask;
        }

        public virtual void OnFinishedShow()
        {
            
        }
    }

}
