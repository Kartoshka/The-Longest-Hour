using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

using MOJ.Helpers;

[CustomEditor(typeof(MoverComponent), true)]
public class MoverComponentInspector : Editor
{
	Dictionary<MoverComponent.ActionTypeMask, bool> m_actionTypeFoldoutDisplay = new Dictionary<MoverComponent.ActionTypeMask, bool>();
	bool m_defaultFoldout = true;

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

		MoverComponent.ActionTypeMask actionTypeMask = (MoverComponent.ActionTypeMask)EditorGUILayout.EnumMaskField("Mover Actions", moverComponent.getActionTypekMask());
		moverComponent.setActionTypeMask(actionTypeMask);

		Dictionary<MoverComponent.ActionTypeMask, Mover> actionMoverBehaviors = moverComponent.getActionMovers();

		int actionTypeMaskCount = System.Enum.GetValues(typeof(MoverComponent.ActionTypeMask)).Length;
        for (int i = 0; i < actionTypeMaskCount; ++i)
		{
			MoverComponent.ActionTypeMask actionType = (MoverComponent.ActionTypeMask)(1 << i) & actionTypeMask;
            if (actionType != 0)
			{
				bool displayFoldout;
				if(!m_actionTypeFoldoutDisplay.TryGetValue(actionType, out displayFoldout))
				{
					m_actionTypeFoldoutDisplay.Add(actionType, true);
					displayFoldout = m_defaultFoldout;
                }

				m_actionTypeFoldoutDisplay[actionType] = EditorGUILayout.Foldout(displayFoldout, actionType.ToString());
				if(displayFoldout)
				{
					moverComponent.createMoverAction(actionType);
					Mover mover = actionMoverBehaviors[actionType];

					EditorGUI.BeginChangeCheck();
					Mover.BehaviorType moverBehaviorType = (Mover.BehaviorType)EditorGUILayout.EnumPopup("Mover Behavior", mover.getBehaviorType());
					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(moverComponent, "Mover Behavior");
						EditorUtility.SetDirty(moverComponent);
						mover.setBehaviorType(moverBehaviorType);
					}

					switch (moverBehaviorType)
					{
						case (Mover.BehaviorType.Linear):
						{
							LinearInputMoverBehavior linearMover = mover.getBehavior() as LinearInputMoverBehavior;
							if (linearMover != null)
							{
								EditorGUI.BeginChangeCheck();
								Vector3 forwardStepSize = EditorGUILayout.Vector3Field("Forward Step Size", linearMover.getForwardStepSize());
								if (EditorGUI.EndChangeCheck())
								{
									Undo.RecordObject(linearMover, "Forward Step Size");
									EditorUtility.SetDirty(linearMover);
									linearMover.setForwardStepSize(forwardStepSize);
								}

								EditorGUI.BeginChangeCheck();
								Vector3 reverseStepSize = EditorGUILayout.Vector3Field("Reverse Step Size", linearMover.getReverseStepSize());
								if (EditorGUI.EndChangeCheck())
								{
									Undo.RecordObject(linearMover, "Reverse Step Size");
									EditorUtility.SetDirty(linearMover);
									linearMover.setReverseStepSize(reverseStepSize);
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

					if(moverBehaviorType != Mover.BehaviorType.Undefined)
					{
						EditorGUI.BeginChangeCheck();
						InterpolationHelper.InterpolationType interpolationType = (InterpolationHelper.InterpolationType)EditorGUILayout.EnumPopup("Interpolation Type", mover.getInterpolationType());
						if (EditorGUI.EndChangeCheck())
						{
							Undo.RecordObject(moverComponent, "Interpolation Type");
							EditorUtility.SetDirty(moverComponent);
							mover.setInterpolationType(interpolationType);
						}

						if (interpolationType != InterpolationHelper.InterpolationType.None)
						{
							if (interpolationType != InterpolationHelper.InterpolationType.Linear)
							{
								EditorGUI.BeginChangeCheck();
								InterpolationHelper.EasingType easingType = (InterpolationHelper.EasingType)EditorGUILayout.EnumPopup("Easing Type", mover.getEasingType());
								if (EditorGUI.EndChangeCheck())
								{
									Undo.RecordObject(moverComponent, "Easing Type");
									EditorUtility.SetDirty(moverComponent);
									mover.setEasingType(easingType);
								}
							}

							EditorGUI.BeginChangeCheck();
							float duration = EditorGUILayout.FloatField("Duration (seconds)", mover.getDuration());
							if (EditorGUI.EndChangeCheck())
							{
								Undo.RecordObject(moverComponent, "Duration");
								EditorUtility.SetDirty(moverComponent);
								mover.setDuration(duration);
							}

							EditorGUI.BeginChangeCheck();
							float updateRate = EditorGUILayout.FloatField("Update Rate (seconds)", mover.getUpdateRate());
							if (EditorGUI.EndChangeCheck())
							{
								Undo.RecordObject(moverComponent, "Update Rate");
								EditorUtility.SetDirty(moverComponent);
								mover.setUpdateRate(updateRate);
							}
						}
					}
				}
			}
			else
			{
				moverComponent.removeMoverAction(actionType);
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

	private void OnSceneGUI()
	{
	}
}