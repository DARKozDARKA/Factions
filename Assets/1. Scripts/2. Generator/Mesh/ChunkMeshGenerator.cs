using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.TerrainGenerator
{
    public class ChunkMeshGenerator
    {
        public const int CubeVerticesAmount = 36;

        private List<Vector3> _vertices = null;
        private List<Vector2> _uvs = null;
        private List<int> _triangles = null;
        private int _sizeX = 0;
        private int _sizeY = 0;
        private int _sizeZ = 0;
        private PointPositionsContainer _pointContainer;
        private BlockTextureAtlas _atlas;

        public ChunkMeshGenerator(BlockTextureAtlas atlas, float offset, Vector3Int chunkSize)
        {
            _pointContainer = new PointPositionsContainer(offset);
            _atlas = atlas;

            _sizeX = chunkSize.x;
            _sizeY = chunkSize.y;
            _sizeZ = chunkSize.z;
        }

        public void SetChunkMesh(Mesh mesh, ChunkObject thisChunk, ChunkObject[,] chunks)
        {
            mesh.Clear();

            DefineMeshData(thisChunk, chunks);

            mesh.vertices = _vertices.ToArray();
            mesh.triangles = _triangles.ToArray();
            mesh.uv = _uvs.ToArray();

            mesh.Optimize();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            ClearMemory();
        }



        private void ClearMemory()
        {
            _vertices.Clear();
            _uvs.Clear();
            _triangles.Clear();
        }

        private void DefineMeshData(ChunkObject thisChunk, ChunkObject[,] chunks)
        {
            _vertices = new List<Vector3>();
            _triangles = new List<int>();
            _uvs = new List<Vector2>();

            bool needShow = false;

            for (int z = 0; z < _sizeZ; z++)
            {
                for (int y = 0; y < _sizeY; y++)
                {
                    for (int x = 0; x < _sizeX; x++)
                    {
                        TerrainBlock block = thisChunk.cubeobjects[x, y, z];
                        foreach (QuadType quad in System.Enum.GetValues(typeof(QuadType)))
                        {
                            int value = (int)((x + (y * _sizeX) + (z * _sizeX * _sizeY)) * CubeVerticesAmount);
                            Vector3 position = new Vector3(x, y, z);

                            if (thisChunk.cubeobjects == null || thisChunk.cubeobjects[x, y, z].isVisible == false)
                                DefineQuad(quad, value, position, false, block);
                            else
                                needShow = CheckVisibility(needShow, z, y, x, quad, value, position, chunks, block, thisChunk);
                        }
                    }
                }
            }
        }


        private bool CheckVisibility(bool needShow, int z, int y, int x, QuadType quad, int value, Vector3 position, ChunkObject[,] chunks, TerrainBlock block, ChunkObject thisChunk)
        {
            switch (quad)
            {
                case QuadType.Back:
                    needShow = (z > 0) ? !thisChunk.cubeobjects[x, y, z - 1].isVisible : (thisChunk.chunkOffset.y > 0) ? !chunks[thisChunk.chunkOffset.x, thisChunk.chunkOffset.y - 1].cubeobjects[x, y, _sizeZ - 1].isVisible : false;
                    break;
                case QuadType.Front:
                    needShow = (z < _sizeZ - 1) ? !thisChunk.cubeobjects[x, y, z + 1].isVisible : (thisChunk.chunkOffset.y < chunks.GetLength(1) - 1) ? !chunks[thisChunk.chunkOffset.x, thisChunk.chunkOffset.y + 1].cubeobjects[x, y, 0].isVisible : false;
                    break;
                case QuadType.Top:
                    needShow = (y < _sizeY - 1) ? !thisChunk.cubeobjects[x, y + 1, z].isVisible : false;
                    break;
                case QuadType.Bottom:
                    needShow = (y > 0) ? !thisChunk.cubeobjects[x, y - 1, z].isVisible : false;
                    break;
                case QuadType.Left:
                    needShow = (x > 0) ? !thisChunk.cubeobjects[x - 1, y, z].isVisible : (thisChunk.chunkOffset.x > 0) ? !chunks[thisChunk.chunkOffset.x - 1, thisChunk.chunkOffset.y].cubeobjects[_sizeX - 1, y, z].isVisible : false;
                    break;
                case QuadType.Right:
                    needShow = (x < _sizeX - 1) ? !thisChunk.cubeobjects[x + 1, y, z].isVisible : (thisChunk.chunkOffset.x < chunks.GetLength(0) - 1) ? !chunks[thisChunk.chunkOffset.x + 1, thisChunk.chunkOffset.y].cubeobjects[0, y, z].isVisible : false;
                    break;
            }
            DefineQuad(quad, value, position, needShow, block);
            return needShow;
        }



        private void DefineQuad(QuadType code, int value, Vector3 position, bool show, TerrainBlock block)
        {
            int indexOffset = _pointContainer.quadToIndexOffset[code];
            if (show)
            {
                AddVerticles(indexOffset, value, position, code);
                AddTriangles(indexOffset, value);
                AddUVs(indexOffset, value, block.type, code);
            }
            else
                EmptyVerticles(indexOffset, value);

        }

        private void AddVerticles(int indexOffset, int value, Vector3 position, QuadType quadType)
        {
            _vertices.Add(position + _pointContainer.quadToPointArray[quadType][0]);
            _vertices.Add(position + _pointContainer.quadToPointArray[quadType][1]);
            _vertices.Add(position + _pointContainer.quadToPointArray[quadType][2]);
            _vertices.Add(position + _pointContainer.quadToPointArray[quadType][3]);
            _vertices.Add(position + _pointContainer.quadToPointArray[quadType][4]);
            _vertices.Add(position + _pointContainer.quadToPointArray[quadType][5]);

        }

        private void EmptyVerticles(int indexOffset, int value)
        {
            _vertices.Add(Vector3.zero);
            _vertices.Add(Vector3.zero);
            _vertices.Add(Vector3.zero);
            _vertices.Add(Vector3.zero);
            _vertices.Add(Vector3.zero);
            _vertices.Add(Vector3.zero);
            _uvs.Add(new Vector2(0, 1) / 2);
            _uvs.Add(new Vector2(1, 0) / 2);
            _uvs.Add(new Vector2(0, 0) / 2);
            _uvs.Add(new Vector2(0, 1) / 2);
            _uvs.Add(new Vector2(1, 1) / 2);
            _uvs.Add(new Vector2(1, 0) / 2);

        }

        private void AddTriangles(int indexOffset, int value)
        {
            _triangles.Add(value + 0 + indexOffset);
            _triangles.Add(value + 1 + indexOffset);
            _triangles.Add(value + 2 + indexOffset);
            _triangles.Add(value + 3 + indexOffset);
            _triangles.Add(value + 4 + indexOffset);
            _triangles.Add(value + 5 + indexOffset);
        }

        private void AddUVs(int indexOffset, int value, BlockType blockType, QuadType quadType)
        {
            BlockTexture blockTexture = _atlas.GetBlockTexture(blockType);
            Vector2 coord = _atlas.GetQuadTexture(blockType, quadType).Value;
            Vector2 size = blockTexture.size;
            _uvs.Add(new Vector2(coord.x, coord.y + size.y));
            _uvs.Add(new Vector2(coord.x + size.x, coord.y));
            _uvs.Add(new Vector2(coord.x, coord.y));
            _uvs.Add(new Vector2(coord.x, coord.y + size.y));
            _uvs.Add(new Vector2(coord.x + size.x, coord.y + size.y));
            _uvs.Add(new Vector2(coord.x + size.x, coord.y));

        }
    }
}


