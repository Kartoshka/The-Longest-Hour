using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace MOJ.Helpers
{

public class InspectorHelper
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	////////////////////////////////////////////////////////////////////////////////////////// 

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	////////////////////////////////////////////////////////////////////////////////////////// 

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	// TODO: If Unity ever fixes this issue, optimize this.
	// The LayerMask class looks at the full 32 bit layer, which has a lot of empty space.
	// The UnityEditorInternal.InternalEditorUtility.layers only looks at the layers which aren't blank.
	// To convert from one to the other requires bit-shifting.
	public static LayerMask convert32BitLayerMaskToTrimmedMask(LayerMask originalMask)
	{
		int trimmedLayerMask = 0;
		LayerMask surfaceLayerMask = originalMask;
		if (surfaceLayerMask > -1)
		{
			for (int fullLayerIndex = 0;
					surfaceLayerMask > 0;
					surfaceLayerMask = surfaceLayerMask >> 1, ++fullLayerIndex)
			{
				if ((surfaceLayerMask & 1) > 0)
				{
					for (int partialLayerIndex = 0; partialLayerIndex < UnityEditorInternal.InternalEditorUtility.layers.Length; ++partialLayerIndex)
					{
						if (LayerMask.LayerToName(fullLayerIndex).Equals(UnityEditorInternal.InternalEditorUtility.layers[partialLayerIndex]))
						{
							trimmedLayerMask |= (1 << partialLayerIndex);
						}
					}
				}
			}
		}
		else
		{
			// 'Everything' is represented as -1.
			trimmedLayerMask = (1 << UnityEditorInternal.InternalEditorUtility.layers.Length) - 1;
		}

		return trimmedLayerMask;
    }

	// TODO: If Unity ever fixes this issue, optimize this.
	// The UnityEditorInternal.InternalEditorUtility.layers only looks at the layers which aren't blank.
	// The LayerMask class looks at the full 32 bit layer, which has a lot of empty space.
	// To convert from one to the other requires bit-shifting.
	public static LayerMask convertTrimmedLayerMaskTo32BitMask(int trimmedLayerMask)
	{
		int full32BitMask = trimmedLayerMask;
		if (trimmedLayerMask > -1)
		{
			List<string> selectedLayers = new List<string>();
			for (int index = 0;
				trimmedLayerMask > 0;
				trimmedLayerMask = trimmedLayerMask >> 1, ++index)
			{
				if ((trimmedLayerMask & 1) > 0)
				{
					selectedLayers.Add(UnityEditorInternal.InternalEditorUtility.layers[index]);
				}
			}
			full32BitMask = LayerMask.GetMask(selectedLayers.ToArray());
		}
		return full32BitMask;
    }

	#endregion
}
}
