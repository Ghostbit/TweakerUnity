﻿using UnityEngine;
using System.Collections;
using Ghostbit.Tweaker.UI;
using Ghostbit.Tweaker.Core;
using UnityEngine.UI;
using System;

namespace Ghostbit.Tweaker.UI
{
	public class HexTileView : MonoBehaviour
	{
		public Image TileImage;
		public HexGridCell<BaseNode> Cell;
		public Text NameText;
		public GameObject UIContrainer;

		public event Action<HexTileView> Tapped;
		public event Action<HexTileView> Selected;
		public event Action<HexTileView> Deselected;

		// Debug Elements
		public Text XText;
		public Text YText;

		public Color TileColor
		{
			get { return TileImage.color; }
			set { TileImage.color = value; }
		}

		public float TileAlpha
		{
			get { return TileImage.color.a; }
			set 
			{
				Color color = TileImage.color;
				color.a = value;
				TileImage.color = color; 
			}
		}

		public string Name
		{
			get { return NameText.text; }
			set { NameText.text = value; }
		}

		public void OnTapped()
		{
			if (Tapped != null)
			{
				Tapped(this);
			}
		}

		public void OnSelected()
		{
			if (Selected != null)
			{
				Selected(this);
			}
		}

		public void OnDeselected()
		{
			if (Deselected != null)
			{
				Deselected(this);
			}
		}
	}
}
