using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.UI
{
    public class ViewAnimationController
	{
		public const string ViewAnimationResource = "ScriptableObject/ViewAnimation/";
		
		

		private static Dictionary<ViewAnimationType, ViewAnimation> _animationDictionary = new();
		private static Stack<View> _ViewHolders = new Stack<View>();
		

		

		private static ViewAnimation GetAnimation(ViewAnimationType type)
		{
			if (!_animationDictionary.ContainsKey(type))
			{
				var handler = Resources.Load<ViewAnimation>(ViewAnimationResource + type);
				_animationDictionary.Add(type, handler);
			}
			return _animationDictionary[type];
		}

		
		public static bool IsViewEmpty()
		{
			return _ViewHolders == null || _ViewHolders.Count == 0;
		}
		private static bool CanShowUI(View target)
		{
			if (IsViewEmpty()) return true;
			View currentView = _ViewHolders.Peek();
			return currentView.IsStackable || target.IsStackable;
		}

	
		
		public static async UniTask PlayShowAnimation(ViewAnimationType type, View target)
		{
			if (target == null)
			{
				Debug.LogError("View Target is null!");
				return;
			}
			if(!CanShowUI(target)) return;
			
			target.Show();
			_ViewHolders.Push(target);
			
			Sequence sequence = GetAnimation(type).PlayShowAnimation(target);
			await sequence.AsyncWaitForCompletion();

			target.OnFinishedShow();
			
		}

		
		public static async UniTask PlayForceShowAnimation(ViewAnimationType type, View target)
		{
			await UniTask.WaitUntil(() => CanShowUI(target));
			await PlayShowAnimation(type, target);
		}

		public static async UniTask PlayHideAnimation(ViewAnimationType type)
		{
			
			if(IsViewEmpty()) return;
			View target = _ViewHolders.Pop();
			
			Sequence sequence = GetAnimation(type).PlayHideAnimation(target);
			await UniTask.WaitUntil(() => !sequence.IsActive());
			target.Hide();
			
			
		}

		public static async UniTask PlayHideAllAnimation(ViewAnimationType type)
		{
			while (!IsViewEmpty())
			{
				await PlayHideAnimation(type);
			}

			
		}
	}
}