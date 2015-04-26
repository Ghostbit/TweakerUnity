﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ghostbit.Tweaker.UI
{
	public class InspectorView : MonoBehaviour
	{
		public InspectorBackgroundView BackgroundPrefab;
		public InspectorHeaderView HeaderPrefab;
		public InspectorDescriptionView DescriptionPrefab;
		public InspectorFooterView FooterPrefab;
		public InspectorStringView StringEditPrefab;
		public InspectorStringView StringSmallEditPrefab;
		public InspectorBoolView BoolEditPrefab;
		public InspectorStepperView StepperPrefab;
		public InspectorToggleGroupView ToggleGroupPrefab;
		public InspectorToggleValueView ToggleValuePrefab;
		public InspectorSliderView SliderPrefab;

		public GameObject ContentContainer;
		public GameObject BodyContainer;

		public InspectorBackgroundView Background { get; private set; }
		public InspectorHeaderView Header { get; private set; }
		public InspectorFooterView Footer { get; private set; }

		public void Awake()
		{
			InstatiatePrefabs();
			Resize();
			OnAwake();
		}

		protected virtual void OnAwake()
		{

		}

		public void DestroySelf()
		{
			Destroy(gameObject);
		}

		private void InstatiatePrefabs()
		{
			Background = InstantiateInspectorComponent(BackgroundPrefab, gameObject);
			Header = InstantiateInspectorComponent(HeaderPrefab, ContentContainer);
			Footer = InstantiateInspectorComponent(FooterPrefab, gameObject);

			Header.GetComponent<RectTransform>().SetAsFirstSibling();
			Footer.GetComponent<RectTransform>().SetAsLastSibling();
			Background.GetComponent<RectTransform>().SetAsFirstSibling();
		}

		public TComponent InstantiateInspectorComponent<TComponent>(TComponent prefab, GameObject parent = null)
			where TComponent : Component
		{
			var component = Instantiate(prefab) as TComponent;
			if(parent == null)
			{
				parent = BodyContainer;
			}
			SetComponentParent(component, parent);
			return component;
		}

		private void SetComponentParent(Component child, GameObject parent)
		{
			child.GetComponent<RectTransform>().SetParent(parent.GetComponent<RectTransform>(), false);
			child.GetComponent<RectTransform>().SetAsLastSibling();
		}

		public void Resize()
		{
			var rect = GetComponent<RectTransform>();
			var anchorMax = rect.anchorMax;
			if(TweakerConsoleController.IsLandscape())
			{
				anchorMax.x = 0.5f;
			}
			else
			{
				anchorMax.x = 1f;
			}
			rect.anchorMax = anchorMax;
		}
	}
}
