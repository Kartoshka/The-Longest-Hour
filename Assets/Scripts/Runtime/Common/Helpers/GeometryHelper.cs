using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MOJ.Helpers
{

public class GeometryHelper
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	public class HitPoint
	{
		public Vector3 position = Vector3.zero;
		public Vector3 normal = Vector3.zero;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	// Reference: http://www.mathopenref.com/coordcentroid.html
	public static Vector3 calculateCentroidPosition(List<Vector3> points)
	{
		Vector3 centroid = Vector3.zero;
		if (points.Count > 0)
		{
			foreach (Vector3 point in points)
			{
				centroid.x += point.x;
				centroid.y += point.y;
				centroid.z += point.z;
			}
			centroid.x /= points.Count;
			centroid.y /= points.Count;
			centroid.z /= points.Count;
		}
		return centroid;
	}

	/// <summary>
	/// Rotate a point in space around a vector by the angle.
	/// </summary>
	/// <param name="point"></param>
	/// <param name="pivot"></param>
	/// <param name="angle"></param>
	/// <returns></returns>
	// Reference: http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
	public static Vector3 rotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
	{
		Vector3 direction = point - pivot;
		direction = angle * direction;
		point = direction + pivot;
		return point;
	}

	// Reference: https://en.wikipedia.org/wiki/Graham_scan
	/// <summary>
	/// Find the bend direction between three points, where P1 is the elbow between P2 and P3.
	/// </summary>
	/// <returns> Returns 0 if colinear, positive if counterClockwise, negative if clockwise.</returns>
	public static float get2DTurnDirection(float x1, float y1, float x2, float y2, float x3, float y3)
	{
		return (x2 - x1)*(y3-y1) - (y2 - y1)*(x3-x1);
	}

	public static HitPoint retrieveHitPoint(RaycastHit hit)
	{
		HitPoint hitPoint = new HitPoint();
		hitPoint.position = hit.point;
		hitPoint.normal = hit.normal;
		return hitPoint;
	}

	//Reference: http://docs.unity3d.com/ScriptReference/RaycastHit-triangleIndex.html
	//Reference: http://answers.unity3d.com/questions/50846/how-do-i-obtain-the-surface-normal-for-a-point-on.html
	public static HitPoint retrieveHitTriangleOnMesh(RaycastHit hit)
	{
		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (meshCollider == null || meshCollider.sharedMesh == null)
			return null;

		HitPoint hitPoint = new HitPoint();
		Mesh mesh = meshCollider.sharedMesh;
		Vector3[] vertices = mesh.vertices;
		Vector3 p0 = vertices[mesh.triangles[hit.triangleIndex * 3 + 0]];
		Vector3 p1 = vertices[mesh.triangles[hit.triangleIndex * 3 + 1]];
		Vector3 p2 = vertices[mesh.triangles[hit.triangleIndex * 3 + 2]];
		Transform hitTransform = hit.collider.transform;
		p0 = hitTransform.TransformPoint(p0);
		p1 = hitTransform.TransformPoint(p1);
		p2 = hitTransform.TransformPoint(p2);

		hitPoint.position = p0; //hit.barycentricCoordinate;

		//Vector3 n0 = mesh.normals[mesh.triangles[hit.triangleIndex * 3 + 0]];
		//Vector3 n1 = mesh.normals[mesh.triangles[hit.triangleIndex * 3 + 1]];
		//Vector3 n2 = mesh.normals[mesh.triangles[hit.triangleIndex * 3 + 2]];
		//Vector3 interpolatedNormal = n0 * hitTriangle.position.x + n1 * hitTriangle.position.y + n2 * hitTriangle.position.z;
		//interpolatedNormal.Normalize();
		//hitTriangle.normal = hit.transform.TransformDirection(interpolatedNormal);

		hitPoint.normal = Vector3.Cross(p1 - p0, p2 - p0);

		//if(drawDebug)
		//{
		//	Debug.DrawLine(p0, p1);
		//	Debug.DrawLine(p1, p2);
		//	Debug.DrawLine(p2, p0);
		//	Debug.DrawRay(hitPoint.position, hitPoint.normal);
		//}

		return hitPoint;
	}

	/// <summary>
	/// Get the slope direction between the closest point to a surface and the hit point in the raycast direction.
	/// </summary>
	/// <param name="raycastData"></param>
	/// <param name="surfaceGradient"></param>
	/// <returns></returns>
	public static bool tryFindSurfaceGradient(ref RaycastData raycastData, out Vector3 surfaceGradient)
	{
		bool success = false;
		surfaceGradient = Vector3.zero;

		RaycastHit hit;
		if (Physics.Raycast(raycastData.sourceTransform.position, raycastData.direction, out hit, raycastData.checkDistance, raycastData.surfaceLayerMask))
		{
			RaycastHit closestSurfacePointHit;
			if (Physics.Raycast(raycastData.sourceTransform.position, hit.normal * -1, out closestSurfacePointHit, raycastData.checkDistance, raycastData.surfaceLayerMask))
			{
				//Vector3 hitDirection = hit.point - raycastData.sourceTransform.position;
				surfaceGradient = hit.point - closestSurfacePointHit.point;

				success = true;
			}
		}
		return success;
	}

	#endregion
	}

}