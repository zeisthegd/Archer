using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;

namespace Penwyn.Tools
{

    public class PMath
    {
        public static class Position
        {
            public static Vector3 GetCenterOf(List<Transform> objects)
            {
                return GetCenterOf(objects.ToArray());
            }

            public static Vector3 GetCenterOf<T>(T[] objects) where T : Transform
            {
                Vector3 culmulativePositions = new Vector3();
                for (int i = 0; i < objects.Length; i++)
                {
                    culmulativePositions += @objects[i].transform.position;
                }
                return culmulativePositions / objects.Length;
            }
        }
    }
}
