using UnityEngine;
using System.Collections.Generic;

namespace CodeBase.TerrainGenerator
{
    internal class PointPositionsContainer
    {
        public Dictionary<QuadType, Vector3[]> quadToPointArray;
        public Dictionary<QuadType, int> quadToIndexOffset = new Dictionary<QuadType, int>()
        {
            {QuadType.Back, 0},
            {QuadType.Front, 6},
            {QuadType.Top, 12},
            {QuadType.Bottom, 18},
            {QuadType.Left, 24},
            {QuadType.Right, 30}

        };

        public PointPositionsContainer(float offset)
        {
            var A = new Vector3(0, offset, 0);
            var B = new Vector3(offset, offset, 0);
            var C = new Vector3(0, 0, 0);
            var D = new Vector3(offset, 0, 0);
            var E = new Vector3(0, offset, offset);
            var F = new Vector3(offset, offset, offset);
            var G = new Vector3(0, 0, offset);
            var H = new Vector3(offset, 0, offset);

            quadToPointArray = new Dictionary<QuadType, Vector3[]>()
            {
                {QuadType.Back, new Vector3[6]{A, D,C, A, B, D }},
                {QuadType.Front, new Vector3[6]{F, G, H, F, E, G }},
                {QuadType.Top, new Vector3[6]{E, B, A, E, F, B }},
                {QuadType.Bottom, new Vector3[6]{C, H, G, C, D, H }},
                {QuadType.Left, new Vector3[6]{E, C, G, E, A, C }},
                {QuadType.Right, new Vector3[6]{B, H, D, B, F, H }}
            };
        }
    }

}