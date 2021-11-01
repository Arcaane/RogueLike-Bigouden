using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(AttackSystemSpline))]
public class AttackSystemInspector : Editor
{
    private AttackSystemSpline spline;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int lineSteps = 10;
    private const float directionScale = 0.5f;

    private void OnSceneGUI()
    {
        spline = target as AttackSystemSpline;
        handleTransform = spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;
        Vector3 p01 = ShowPoint(0, spline.arrayVector[0].pointAttack);
        Vector3 p02 = ShowPoint(0, spline.arrayVector[1].pointAttack);
        Vector3 p03 = ShowPoint(0, spline.arrayVector[2].pointAttack);

        DrawCurve(p01, spline.arrayVector[0].pointAttack, spline.arrayVector[0].color);
        DrawCurve(p02, spline.arrayVector[1].pointAttack, spline.arrayVector[1].color);
        DrawCurve(p03, spline.arrayVector[2].pointAttack, spline.arrayVector[2].color);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        spline = target as AttackSystemSpline;
        if (spline.addPoint == true)
        {
            if (GUILayout.Button("Add Curve for Attack One"))
            {
                Undo.RecordObject(spline, "Add Curve for Attack One");
                spline.AddCurve(0);
                EditorUtility.SetDirty(spline);
            }

            if (GUILayout.Button("Add Curve for Attack Two"))
            {
                Undo.RecordObject(spline, "Add Curve for Attack Two");
                spline.AddCurve(1);
                EditorUtility.SetDirty(spline);
            }

            if (GUILayout.Button("Add Curve for Attack Three"))
            {
                Undo.RecordObject(spline, "Add Curve for Attack Three");
                spline.AddCurve(2);
                EditorUtility.SetDirty(spline);
            }
        }
    }

    private void ShowDirections(Vector3[] pointsAttack, Color color)
    {
        Handles.color = color;
        Vector3 point = spline.GetPoint(0f, pointsAttack);
        Handles.DrawLine(point, point + spline.GetDirection(0f, pointsAttack));
        for (int i = 1; i <= lineSteps; i++)
        {
            point = spline.GetPoint(i / (float) lineSteps, pointsAttack);
            Handles.DrawLine(point, point + spline.GetDirection(i / (float) lineSteps, pointsAttack) * directionScale);
        }
    }


    private Vector3 ShowPoint(int index, Vector3[] pointAttack)
    {
        Vector3 newPoint = new Vector3(pointAttack[index].x, pointAttack[index].y,
            pointAttack[index].z) * spline.radiusAttack;
        Vector3 point = handleTransform.TransformPoint(newPoint);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Move Point");
            EditorUtility.SetDirty(spline);
            pointAttack[index] = handleTransform.InverseTransformPoint(point / spline.radiusAttack);
        }

        return point;
    }

    private void DrawCurve(Vector3 firstPoint, Vector3[] attackPoint, Color colorLine)
    {
        for (int j = 1; j < attackPoint.Length; j += 3)
        {
            Vector3 p1 = ShowPoint(j, attackPoint);
            Vector3 p2 = ShowPoint(j + 1, attackPoint);
            Vector3 p3 = ShowPoint(j + 2, attackPoint);

            Handles.color = Color.gray;
            Handles.DrawLine(firstPoint, p1);
            Handles.DrawLine(p2, p3);

            Handles.DrawBezier(firstPoint, p3, p1, p2, colorLine, null, 2f);
            firstPoint = p3;
        }

        ShowDirections(attackPoint, colorLine);
    }
}