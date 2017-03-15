using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MOJ.Helpers;

public class MiniMapCamera : MonoBehaviour {

    public GameObject m_objectToFollow;
    public GameObject m_lineDrawer;

    bool m_clockActive = false;
    LineDrawerComponent m_lineDrawerComp;

    private Vector3[] closedLoop;

    // Use this for initialization
    void Start () {
        m_lineDrawerComp = m_lineDrawer.GetComponent<LineDrawerComponent>();

    }
	
	// Update is called once per frame
	void Update () {
        if (m_objectToFollow != null)
        {
            // drawing just finished, set camera to look at center of drawing
            if (!m_clockActive && (m_lineDrawerComp.currentState == LineDrawerComponent.DrawState.Complete))
            {
                m_clockActive = true;
                //transform.position = new Vector3(m_objectToFollow.transform.position.x, m_objectToFollow.transform.position.y + 10, m_objectToFollow.transform.position.z);

                // get center of polygon 
                //transform.position = m_lineDrawerComp.m_bezierSplineComponent.GetPoint(0);
                Vector3[] points = m_lineDrawerComp.m_bezierSplineComponent.GetPoints();
                float start = m_lineDrawerComp.getClosedLoopStartPoint();
                int startIndex = (int)(start * points.Length);

                closedLoop = new Vector3[points.Length - startIndex];
                int j = 0;
                for(int i = startIndex; i < points.Length; i++)
                {
                    closedLoop[j] = points[i];
                    j++;
                }

                Vector3 centroid = GeometryHelper.calculateCentroidPosition(closedLoop);

                float dist = GeometryHelper.getLongestDistanceInPoly(closedLoop);
                transform.position = new Vector3(centroid.x, dist, centroid.z);
            }

            // drawing deactivated
            if (m_clockActive && !(m_lineDrawerComp.currentState == LineDrawerComponent.DrawState.Complete))
            {
                transform.position = new Vector3(0, -10, 0);
                m_clockActive = false;
            }

            //transform.position = new Vector3(m_objectToFollow.transform.position.x, m_objectToFollow.transform.position.y + 10, m_objectToFollow.transform.position.z);
        }
	}
}
