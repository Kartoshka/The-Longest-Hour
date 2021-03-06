﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

using MOJ.Helpers;

[CustomEditor(typeof(MoverComponent), true)]
public class MoverComponentInspector : Editor
{
	Dictionary<MoverComponent.ActionTypeFlag, bool> m_actionTypeFlagFoldoutDisplay = new Dictionary<MoverComponent.ActionTypeFlag, bool>();
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

		EditorGUILayout.LabelField("Affected Transform");
		EditorGUI.BeginChangeCheck();
		Transform transformComponent = (Transform)EditorGUILayout.ObjectField(moverComponent.getTransform(), typeof(Transform), true);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverComponent, "Affected Transform");
			EditorUtility.SetDirty(moverComponent);
			moverComponent.setTransform(transformComponent);
		}

		EditorGUI.BeginChangeCheck();
		MoverComponent.ActionTypeFlag allActions = (MoverComponent.ActionTypeFlag)EditorGUILayout.EnumMaskField("Mover Actions", moverComponent.getActionTypeFlags());
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverComponent, "Mover Actions");
			EditorUtility.SetDirty(moverComponent);
			moverComponent.setActionTypeFlags(allActions);
		}

		Dictionary<MoverComponent.ActionTypeFlag, Mover> actionMoverBehaviors = moverComponent.getActionMovers();

		int actionTypeFlagCount = System.Enum.GetValues(typeof(MoverComponent.ActionTypeFlag)).Length;
        for (int i = 0; i < actionTypeFlagCount; ++i)
		{
			MoverComponent.ActionTypeFlag mask = (MoverComponent.ActionTypeFlag)(1 << i);
            MoverComponent.ActionTypeFlag maskedActionTypeFlag = mask & allActions;
            if (maskedActionTypeFlag != 0)
			{
				bool displayFoldout;
				if(!m_actionTypeFlagFoldoutDisplay.TryGetValue(maskedActionTypeFlag, out displayFoldout))
				{
					m_actionTypeFlagFoldoutDisplay.Add(maskedActionTypeFlag, true);
					displayFoldout = m_defaultFoldout;
                }

				m_actionTypeFlagFoldoutDisplay[maskedActionTypeFlag] = EditorGUILayout.Foldout(displayFoldout, maskedActionTypeFlag.ToString());
				if (displayFoldout)
				{
					moverComponent.createMoverAction(maskedActionTypeFlag);
					Mover mover = actionMoverBehaviors[maskedActionTypeFlag];

					EditorGUILayout.LabelField("Mover Behaviour");
					EditorGUI.BeginChangeCheck();
					MoverBehavior moverBehavior = (MoverBehavior)EditorGUILayout.ObjectField(mover.getMoverBehavior(), typeof(MoverBehavior), true);
					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(moverComponent, "Mover Behaviour");
						EditorUtility.SetDirty(moverComponent);
						mover.setMoverBehavior(moverBehavior);
					}

					if(moverBehavior != null)
					{
						createMoverBehaviorUI(moverBehavior);

						EditorGUI.BeginChangeCheck();
						InterpolationHelper.InterpolationType interpolationType = (InterpolationHelper.InterpolationType)EditorGUILayout.EnumPopup("Interpolation Type", mover.getInterpolationType());
						if (EditorGUI.EndChangeCheck())
						{
							Undo.RecordObject(moverComponent, "Interpolation Type");
							EditorUtility.SetDirty(moverComponent);
							mover.setInterpolationType(interpolationType);
						}

						if (interpolationType != InterpolationHelper.InterpolationType.None
							&& interpolationType != InterpolationHelper.InterpolationType.Undefined)
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
				moverComponent.removeMoverAction(mask);
			}
		}

		serializedObject.ApplyModifiedProperties();
	}

	private void createMoverBehaviorUI(MoverBehavior moverBehavior)
	{
		EditorGUI.BeginChangeCheck();
		bool moveRelative = EditorGUILayout.Toggle("Relative Movement", moverBehavior.getMoveRelative());
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverBehavior, "Relative Movement");
			EditorUtility.SetDirty(moverBehavior);
			moverBehavior.setMoveRelative(moveRelative);
		}

		EditorGUI.BeginChangeCheck();
		bool enableUserInput = EditorGUILayout.Toggle("Enable User Input", moverBehavior.getEnableUserInput());
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverBehavior, "Enable User Input");
			EditorUtility.SetDirty(moverBehavior);
			moverBehavior.setEnableUserInput(enableUserInput);
		}

		if(moverBehavior.GetType().Equals(typeof(LinearInputMoverBehavior)))
		{
			createLinearInputMoverBehaviorUI(moverBehavior);
        }
		else if(moverBehavior.GetType().Equals(typeof(RigidBodyForceMoverBehavior)))
		{
			createRigidBodyForceMoverBehaviorUI(moverBehavior);
		}
		else if (moverBehavior.GetType().Equals(typeof(SurfaceMoverBehavior)))
		{
			createAttachToSurfaceMoverBehaviorUI(moverBehavior);
		}

		EditorGUI.BeginChangeCheck();
		Vector3 positionOffset = EditorGUILayout.Vector3Field("Position Offset", moverBehavior.getTargetPositionOffset());
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(moverBehavior, "Position Offset");
			EditorUtility.SetDirty(moverBehavior);
			moverBehavior.setTargetPositionOffset(positionOffset);
		}
	}

	private void createLinearInputMoverBehaviorUI(MoverBehavior moverBehavior)
	{
		LinearInputMoverBehavior linearMover = moverBehavior as LinearInputMoverBehavior;
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
		}
	}

	private void createRigidBodyForceMoverBehaviorUI(MoverBehavior moverBehavior)
	{
		RigidBodyForceMoverBehavior rigidBodyForceMover = moverBehavior as RigidBodyForceMoverBehavior;
		if (rigidBodyForceMover != null)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 forceMagnitude = EditorGUILayout.Vector3Field("Force Magnitude", rigidBodyForceMover.getForceMagnitude());
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(rigidBodyForceMover, "Force Magnitude");
				EditorUtility.SetDirty(rigidBodyForceMover);
				rigidBodyForceMover.setForceMagnitude(forceMagnitude);
			}

			EditorGUI.BeginChangeCheck();
			float duration = EditorGUILayout.DelayedFloatField("Duration", rigidBodyForceMover.getDuration());
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(rigidBodyForceMover, "Duration");
				EditorUtility.SetDirty(rigidBodyForceMover);
				rigidBodyForceMover.setDuration(duration);
			}

			EditorGUILayout.LabelField("RigidBody");
			EditorGUI.BeginChangeCheck();
			Rigidbody rigidBody = (Rigidbody)EditorGUILayout.ObjectField(rigidBodyForceMover.getRigidBody(), typeof(Rigidbody), true);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(rigidBodyForceMover, "RigidBody");
				EditorUtility.SetDirty(rigidBodyForceMover);
				rigidBodyForceMover.setRigidBody(rigidBody);
			}

			EditorGUI.BeginChangeCheck();
			bool isAdditiveForce = EditorGUILayout.Toggle("Is Additive Force", rigidBodyForceMover.getIsAdditiveForce());
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(rigidBodyForceMover, "Is Additive Force");
				EditorUtility.SetDirty(rigidBodyForceMover);
				rigidBodyForceMover.setIsAdditiveForce(isAdditiveForce);
			}

			EditorGUILayout.LabelField("Surface Collision Checker Transform");
			EditorGUI.BeginChangeCheck();
			Transform surfaceCheckTransform = (Transform)EditorGUILayout.ObjectField(rigidBodyForceMover.getSurfaceCheckSourceTransform(), typeof(Transform), true);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(rigidBodyForceMover, "Surface Collision Checker Transform");
				EditorUtility.SetDirty(rigidBodyForceMover);
				rigidBodyForceMover.setSurfaceCheckSourceTransform(surfaceCheckTransform);
			}

			if(surfaceCheckTransform)
			{
				EditorGUI.BeginChangeCheck();
				float surfaceCheckRadius = EditorGUILayout.DelayedFloatField("Checker Radius", rigidBodyForceMover.getSurfaceCheckRadius());
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(rigidBodyForceMover, "Checker Radius");
					EditorUtility.SetDirty(rigidBodyForceMover);
					rigidBodyForceMover.setSurfaceCheckRadius(surfaceCheckRadius);
				}

				LayerMask surfaceLayerMask = InspectorHelper.convert32BitLayerMaskToTrimmedMask(rigidBodyForceMover.getSurfaceLayerMask());
				EditorGUI.BeginChangeCheck();
				surfaceLayerMask = EditorGUILayout.MaskField("Surface Layer Mask", surfaceLayerMask, UnityEditorInternal.InternalEditorUtility.layers);
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(rigidBodyForceMover, "Surface Layer Mask");
					EditorUtility.SetDirty(rigidBodyForceMover);

					rigidBodyForceMover.setSurfaceLayerMask(InspectorHelper.convertTrimmedLayerMaskTo32BitMask(surfaceLayerMask));
				}
			}
		}
	}

	private void createAttachToSurfaceMoverBehaviorUI(MoverBehavior moverBehavior)
	{
		SurfaceMoverBehavior surfaceMover = moverBehavior as SurfaceMoverBehavior;
		if (surfaceMover != null)
		{
			//EditorGUI.BeginChangeCheck();
			//Vector3 positionOffset = EditorGUILayout.Vector3Field("Position Offset", surfaceMover.getPositionOffset());
			//if (EditorGUI.EndChangeCheck())
			//{
			//	Undo.RecordObject(surfaceMover, "Position Offset");
			//	EditorUtility.SetDirty(surfaceMover);
			//	surfaceMover.setPositionOffset(positionOffset);
			//}

			EditorGUILayout.LabelField("Surface Collision Checker Transform");
			EditorGUI.BeginChangeCheck();
			Transform surfaceCheckTransform = (Transform)EditorGUILayout.ObjectField(surfaceMover.getSurfaceCheckSourceTransform(), typeof(Transform), true);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(surfaceMover, "Surface Collision Checker Transform");
				EditorUtility.SetDirty(surfaceMover);
				surfaceMover.setSurfaceCheckSourceTransform(surfaceCheckTransform);
			}

			if(surfaceCheckTransform)
			{
				EditorGUI.BeginChangeCheck();
				Vector3 rayCastDirection = EditorGUILayout.Vector3Field("Default RayCast Direction", surfaceMover.getDefaultRaycastDirection());
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(surfaceMover, "Default RayCast Direction");
					EditorUtility.SetDirty(surfaceMover);
					surfaceMover.setDefaultRaycastDirection(rayCastDirection);
				}

				LayerMask surfaceLayerMask = InspectorHelper.convert32BitLayerMaskToTrimmedMask(surfaceMover.getSurfaceLayerMask());
				EditorGUI.BeginChangeCheck();
				surfaceLayerMask = EditorGUILayout.MaskField("Surface Layer Mask", surfaceLayerMask, UnityEditorInternal.InternalEditorUtility.layers);
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(surfaceMover, "Surface Layer Mask");
					EditorUtility.SetDirty(surfaceMover);

					surfaceMover.setSurfaceLayerMask(InspectorHelper.convertTrimmedLayerMaskTo32BitMask(surfaceLayerMask));
				}
			}

			EditorGUI.BeginChangeCheck();
			float friction = EditorGUILayout.FloatField("Friction", surfaceMover.getFrictionValue());
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(surfaceMover, "Friction");
				EditorUtility.SetDirty(surfaceMover);
				surfaceMover.setFrictionValue(Mathf.Clamp01(friction));
			}
		}
	}

	private void OnSceneGUI()
	{
	}
}