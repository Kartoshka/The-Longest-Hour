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

	// Rotate a point in space around a vector by the angle.
	// Reference: http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
	public static Vector3 rotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
	{
		Vector3 direction = point - pivot;
		direction = angle * direction;
		point = direction + pivot;
		return point;
	}

	public static HitPoint retrieveHitPoint(RaycastHit hit, bool drawDebug = false)
	{
		HitPoint hitPoint = new HitPoint();
		hitPoint.position = hit.point;
		hitPoint.normal = hit.normal;
		return hitPoint;
	}

	//Reference: http://docs.unity3d.com/ScriptReference/RaycastHit-triangleIndex.html
	//Reference: http://answers.unity3d.com/questions/50846/how-do-i-obtain-the-surface-normal-for-a-point-on.html
	public static HitPoint retrieveHitTriangleOnMesh(RaycastHit hit, bool drawDebug = false)
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

		if(drawDebug)
		{
			Debug.DrawLine(p0, p1);
			Debug.DrawLine(p1, p2);
			Debug.DrawLine(p2, p0);
			Debug.DrawRay(hitPoint.position, hitPoint.normal);
		}

		return hitPoint;
	}

	#endregion
	}

}