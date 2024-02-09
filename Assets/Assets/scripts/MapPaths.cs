using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class MapPaths : Graphic
{
   

    [SerializeField] GameObject mesh;
     public MapEncounters scb;
     public float thickness = 10f;
     public List<Vector2> Map;
     public int paths = 0;
     protected override void OnPopulateMesh(VertexHelper vh) //rysuje dwa trójk¹ty tworz¹ce razem prostok¹t, jedn¹ ze œcie¿ek
     {
         RectTransform meshrect = mesh.GetComponent<RectTransform>();
         scb = GetComponent<MapEncounters>();
         Map = scb.Map;
         vh.Clear();
         for (int i = 0; i < Map.Count; i++)
         {                
             Vector2 point = Map[i];                
             DrawLine(point, vh, meshrect);
         }
         for (int i = 0; MapEncounters.enc[i] != null; i++)
         {
             int index1 = i * 2;
             for (int j = 3; j <= 8 ? MapEncounters.enc[i][j] != 0 : false; j++)
             {
                 int index2 = MapEncounters.enc[i][j]*2;
                 vh.AddTriangle(index1, index1+1, index2 + 1);
                 vh.AddTriangle(index2 + 1, index2, index1);
             }
         }
     }
     void DrawLine(Vector2 point, VertexHelper vh, RectTransform meshrect) 
     {                                                                     
        UIVertex vertex = UIVertex.simpleVert;                             
        vertex.color = color;

        Vector3[] corners = new Vector3[4];
        meshrect.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[0];  

        Vector3 worldPoint = new Vector3(bottomLeft.x + point.x, bottomLeft.y + point.y, 0f);

        Vector3 localPoint1 = meshrect.InverseTransformPoint(worldPoint + new Vector3(-thickness / 2, 0));
        Vector3 localPoint2 = meshrect.InverseTransformPoint(worldPoint + new Vector3(thickness / 2, 0));

        vertex.position = localPoint1;
        vh.AddVert(vertex);

        vertex.position = localPoint2;
        vh.AddVert(vertex);

    }
}
