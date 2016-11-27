using UnityEngine;
using UnityEditor;

using MOJ.Helpers;

[CustomEditor(typeof(MoverComponent))]
public class MoverComponentInspector : Editor
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		MoverComponent moverComponent = target as MoverComponent;

		EditorGUI.BeginChangeCheck();
		bool alwaysUpdate = EditorGUILayout.Toggle("Always Update", moverComponent.getAlwaysUpdate());
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverComponent, "Always Update");
			EditorUtility.SetDirty(moverComponent);
			moverComponent.setAlwaysUpdate(alwaysUpdate);
		}

		EditorGUI.BeginChangeCheck();
		MoverComponentData.MoverType moverType = (MoverComponentData.MoverType)EditorGUILayout.EnumPopup("Mover Type", moverComponent.getMoverType());
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverComponent, "Mover Type");
            EditorUtility.SetDirty(moverComponent);
			moverComponent.setMoverType(moverType);
        }

		switch (moverType)
		{
			case (MoverComponentData.MoverType.Linear):
			{
				LinearInputMoverBehavior linearMover = moverComponent.getMoverBehavior() as LinearInputMoverBehavior;
				if(linearMover != null)
				{
                    EditorGUI.BeginChangeCheck();
					Vector3 stepSize = EditorGUILayout.Vector3Field("Step Size", linearMover.getStepSize());
					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(linearMover, "Step Size");
						EditorUtility.SetDirty(linearMover);
						linearMover.setStepSize(stepSize);
					}

					EditorGUI.BeginChangeCheck();
					bool enableUserInput = EditorGUILayout.Toggle("Enable User Input", linearMover.getEnableUserInput());
					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(linearMover, "Enable User Input");
						EditorUtility.SetDirty(linearMover);
						linearMover.setEnableUserInput(enableUserInput);
					}
				}
				break;
			}
		}

		EditorGUI.BeginChangeCheck();
		InterpolationHelper.InterpolationType interpolationType = (InterpolationHelper.InterpolationType)EditorGUILayout.EnumPopup("Interpolation Type", moverComponent.getInterpolationType());
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverComponent, "Interpolation Type");
			EditorUtility.SetDirty(moverComponent);
			moverComponent.setInterpolationType(interpolationType);
		}

		if(interpolationType != InterpolationHelper.InterpolationType.None)
		{
			if(interpolationType != InterpolationHelper.InterpolationType.Linear)
			{
				EditorGUI.BeginChangeCheck();
				InterpolationHelper.EasingType easingType = (InterpolationHelper.EasingType)EditorGUILayout.EnumPopup("Easing Type", moverComponent.getEasingType());
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(moverComponent, "Easing Type");
					EditorUtility.SetDirty(moverComponent);
					moverComponent.setEasingType(easingType);
				}
			}

			EditorGUI.BeginChangeCheck();
			float duration = EditorGUILayout.FloatField("Duration (seconds)", moverComponent.getDuration());
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(moverComponent, "Duration");
				EditorUtility.SetDirty(moverComponent);
				moverComponent.setDuration(duration);
			}

			EditorGUI.BeginChangeCheck();
			float updateRate = EditorGUILayout.FloatField("Update Rate (seconds)", moverComponent.getUpdateRate());
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(moverComponent, "Update Rate");
				EditorUtility.SetDirty(moverComponent);
				moverComponent.setUpdateRate(updateRate);
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

	private void OnSceneGUI()
	{
	}
}