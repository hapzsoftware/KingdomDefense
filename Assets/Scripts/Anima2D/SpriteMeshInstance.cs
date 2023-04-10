using System;
using System.Collections.Generic;
using UnityEngine;

namespace Anima2D
{
	[ExecuteInEditMode]
	public class SpriteMeshInstance : MonoBehaviour
	{
		[SerializeField]
		private SpriteMesh m_SpriteMesh;

		[SerializeField]
		private Color m_Color = Color.white;

		[SerializeField]
		private Material[] m_Materials;

		[SerializeField]
		private int m_SortingLayerID;

		[SerializeField]
		private int m_SortingOrder;

		[HideInInspector, SerializeField]
		private Transform[] m_BoneTransforms;

		private List<Bone2D> m_CachedBones = new List<Bone2D>();

		private MaterialPropertyBlock m_MaterialPropertyBlock;

		private Renderer mCachedRenderer;

		private MeshFilter mCachedMeshFilter;

		private SkinnedMeshRenderer mCachedSkinnedRenderer;

		private Mesh m_InitialMesh;

		private Mesh m_CurrentMesh;

		public SpriteMesh spriteMesh
		{
			get
			{
				return this.m_SpriteMesh;
			}
			set
			{
				this.m_SpriteMesh = value;
			}
		}

		public Material sharedMaterial
		{
			get
			{
				if (this.m_Materials.Length > 0)
				{
					return this.m_Materials[0];
				}
				return null;
			}
			set
			{
				this.m_Materials = new Material[]
				{
					value
				};
			}
		}

		public Material[] sharedMaterials
		{
			get
			{
				return this.m_Materials;
			}
			set
			{
				this.m_Materials = value;
			}
		}

		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		public int sortingLayerID
		{
			get
			{
				return this.m_SortingLayerID;
			}
			set
			{
				this.m_SortingLayerID = value;
			}
		}

		public string sortingLayerName
		{
			get
			{
				if (this.cachedRenderer)
				{
					return this.cachedRenderer.sortingLayerName;
				}
				return "Default";
			}
			set
			{
				if (this.cachedRenderer)
				{
					this.cachedRenderer.sortingLayerName = value;
					this.sortingLayerID = this.cachedRenderer.sortingLayerID;
				}
			}
		}

		public int sortingOrder
		{
			get
			{
				return this.m_SortingOrder;
			}
			set
			{
				this.m_SortingOrder = value;
			}
		}

		public List<Bone2D> bones
		{
			get
			{
				if (this.m_BoneTransforms != null && this.m_CachedBones.Count != this.m_BoneTransforms.Length)
				{
					this.m_CachedBones = new List<Bone2D>(this.m_BoneTransforms.Length);
					for (int i = 0; i < this.m_BoneTransforms.Length; i++)
					{
						Bone2D item = null;
						if (this.m_BoneTransforms[i])
						{
							item = this.m_BoneTransforms[i].GetComponent<Bone2D>();
						}
						this.m_CachedBones.Add(item);
					}
				}
				for (int j = 0; j < this.m_CachedBones.Count; j++)
				{
					if (this.m_CachedBones[j] && this.m_BoneTransforms[j] != this.m_CachedBones[j].transform)
					{
						this.m_CachedBones[j] = null;
					}
					if (!this.m_CachedBones[j] && this.m_BoneTransforms[j])
					{
						this.m_CachedBones[j] = this.m_BoneTransforms[j].GetComponent<Bone2D>();
					}
				}
				return this.m_CachedBones;
			}
			set
			{
				this.m_CachedBones = new List<Bone2D>(value);
				this.m_BoneTransforms = new Transform[this.m_CachedBones.Count];
				for (int i = 0; i < this.m_CachedBones.Count; i++)
				{
					Bone2D bone2D = this.m_CachedBones[i];
					if (bone2D)
					{
						this.m_BoneTransforms[i] = bone2D.transform;
					}
				}
				if (this.cachedSkinnedRenderer)
				{
					this.cachedSkinnedRenderer.bones = this.m_BoneTransforms;
				}
			}
		}

		private MaterialPropertyBlock materialPropertyBlock
		{
			get
			{
				if (this.m_MaterialPropertyBlock == null)
				{
					this.m_MaterialPropertyBlock = new MaterialPropertyBlock();
				}
				return this.m_MaterialPropertyBlock;
			}
		}

		public Renderer cachedRenderer
		{
			get
			{
				if (!this.mCachedRenderer)
				{
					this.mCachedRenderer = base.GetComponent<Renderer>();
				}
				return this.mCachedRenderer;
			}
		}

		public MeshFilter cachedMeshFilter
		{
			get
			{
				if (!this.mCachedMeshFilter)
				{
					this.mCachedMeshFilter = base.GetComponent<MeshFilter>();
				}
				return this.mCachedMeshFilter;
			}
		}

		public SkinnedMeshRenderer cachedSkinnedRenderer
		{
			get
			{
				if (!this.mCachedSkinnedRenderer)
				{
					this.mCachedSkinnedRenderer = base.GetComponent<SkinnedMeshRenderer>();
				}
				return this.mCachedSkinnedRenderer;
			}
		}

		private Texture2D spriteTexture
		{
			get
			{
				if (this.spriteMesh && this.spriteMesh.sprite)
				{
					return this.spriteMesh.sprite.texture;
				}
				return null;
			}
		}

		public Mesh sharedMesh
		{
			get
			{
				if (this.m_InitialMesh)
				{
					return this.m_InitialMesh;
				}
				return null;
			}
		}

		public Mesh mesh
		{
			get
			{
				if (this.m_CurrentMesh)
				{
					return UnityEngine.Object.Instantiate<Mesh>(this.m_CurrentMesh);
				}
				return null;
			}
		}

		private void OnDestroy()
		{
			if (this.m_CurrentMesh)
			{
				UnityEngine.Object.Destroy(this.m_CurrentMesh);
			}
		}

		private void Awake()
		{
			this.UpdateCurrentMesh();
		}

		private void UpdateInitialMesh()
		{
			this.m_InitialMesh = null;
			if (this.spriteMesh && this.spriteMesh.sharedMesh)
			{
				this.m_InitialMesh = this.spriteMesh.sharedMesh;
			}
		}

		private void UpdateCurrentMesh()
		{
			this.UpdateInitialMesh();
			if (this.m_InitialMesh)
			{
				if (!this.m_CurrentMesh)
				{
					this.m_CurrentMesh = new Mesh();
					this.m_CurrentMesh.hideFlags = HideFlags.DontSave;
					this.m_CurrentMesh.MarkDynamic();
				}
				this.m_CurrentMesh.Clear();
				this.m_CurrentMesh.name = this.m_InitialMesh.name;
				this.m_CurrentMesh.vertices = this.m_InitialMesh.vertices;
				this.m_CurrentMesh.uv = this.m_InitialMesh.uv;
				this.m_CurrentMesh.normals = this.m_InitialMesh.normals;
				this.m_CurrentMesh.tangents = this.m_InitialMesh.tangents;
				this.m_CurrentMesh.boneWeights = this.m_InitialMesh.boneWeights;
				this.m_CurrentMesh.bindposes = this.m_InitialMesh.bindposes;
				this.m_CurrentMesh.bounds = this.m_InitialMesh.bounds;
				this.m_CurrentMesh.colors = this.m_InitialMesh.colors;
				for (int i = 0; i < this.m_InitialMesh.subMeshCount; i++)
				{
					this.m_CurrentMesh.SetTriangles(this.m_InitialMesh.GetTriangles(i), i);
				}
				this.m_CurrentMesh.ClearBlendShapes();
				for (int j = 0; j < this.m_InitialMesh.blendShapeCount; j++)
				{
					string blendShapeName = this.m_InitialMesh.GetBlendShapeName(j);
					for (int k = 0; k < this.m_InitialMesh.GetBlendShapeFrameCount(j); k++)
					{
						float blendShapeFrameWeight = this.m_InitialMesh.GetBlendShapeFrameWeight(j, k);
						Vector3[] deltaVertices = new Vector3[this.m_InitialMesh.vertexCount];
						this.m_InitialMesh.GetBlendShapeFrameVertices(j, k, deltaVertices, null, null);
						this.m_CurrentMesh.AddBlendShapeFrame(blendShapeName, blendShapeFrameWeight, deltaVertices, null, null);
					}
				}
				this.m_CurrentMesh.hideFlags = HideFlags.DontSave;
			}
			else
			{
				this.m_InitialMesh = null;
				if (this.m_CurrentMesh)
				{
					this.m_CurrentMesh.Clear();
				}
			}
			if (this.m_CurrentMesh)
			{
				if (this.spriteMesh && this.spriteMesh.sprite && this.spriteMesh.sprite.packed)
				{
					this.SetSpriteUVs(this.m_CurrentMesh, this.spriteMesh.sprite);
				}
				this.m_CurrentMesh.UploadMeshData(false);
			}
			this.UpdateRenderers();
		}

		private void SetSpriteUVs(Mesh mesh, Sprite sprite)
		{
			Vector2[] uv = sprite.uv;
			if (mesh.vertexCount == uv.Length)
			{
				mesh.uv = sprite.uv;
			}
		}

		private void UpdateRenderers()
		{
			Mesh sharedMesh = null;
			if (this.m_InitialMesh)
			{
				sharedMesh = this.m_CurrentMesh;
			}
			if (this.cachedSkinnedRenderer)
			{
				this.cachedSkinnedRenderer.sharedMesh = sharedMesh;
			}
			else if (this.cachedMeshFilter)
			{
				this.cachedMeshFilter.sharedMesh = sharedMesh;
			}
		}

		private void LateUpdate()
		{
			if (!this.spriteMesh || (this.spriteMesh && this.spriteMesh.sharedMesh != this.m_InitialMesh))
			{
				this.UpdateCurrentMesh();
			}
		}

		private void OnWillRenderObject()
		{
			this.UpdateRenderers();
			if (this.cachedRenderer)
			{
				this.cachedRenderer.sortingLayerID = this.sortingLayerID;
				this.cachedRenderer.sortingOrder = this.sortingOrder;
				this.cachedRenderer.sharedMaterials = this.m_Materials;
				this.cachedRenderer.GetPropertyBlock(this.materialPropertyBlock);
				if (this.spriteTexture)
				{
					this.materialPropertyBlock.SetTexture("_MainTex", this.spriteTexture);
				}
				this.materialPropertyBlock.SetColor("_Color", this.color);
				this.cachedRenderer.SetPropertyBlock(this.materialPropertyBlock);
			}
		}
	}
}
