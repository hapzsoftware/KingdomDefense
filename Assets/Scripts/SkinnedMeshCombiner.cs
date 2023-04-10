using Anima2D;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshCombiner : MonoBehaviour
{
	[SerializeField]
	private SpriteMeshInstance[] m_SpriteMeshInstances;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private SkinnedMeshRenderer m_CachedSkinnedRenderer;

	public SpriteMeshInstance[] spriteMeshInstances
	{
		get
		{
			return this.m_SpriteMeshInstances;
		}
		set
		{
			this.m_SpriteMeshInstances = value;
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

	public SkinnedMeshRenderer cachedSkinnedRenderer
	{
		get
		{
			if (!this.m_CachedSkinnedRenderer)
			{
				this.m_CachedSkinnedRenderer = base.GetComponent<SkinnedMeshRenderer>();
			}
			return this.m_CachedSkinnedRenderer;
		}
	}

	private void Start()
	{
		Vector3 position = base.transform.position;
		Quaternion rotation = base.transform.rotation;
		Vector3 localScale = base.transform.localScale;
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		List<Transform> list = new List<Transform>();
		List<BoneWeight> list2 = new List<BoneWeight>();
		List<CombineInstance> list3 = new List<CombineInstance>();
		int num = 0;
		for (int i = 0; i < this.spriteMeshInstances.Length; i++)
		{
			SpriteMeshInstance spriteMeshInstance = this.spriteMeshInstances[i];
			if (spriteMeshInstance.cachedSkinnedRenderer)
			{
				num += spriteMeshInstance.mesh.subMeshCount;
			}
		}
		int[] array = new int[num];
		int num2 = 0;
		for (int j = 0; j < this.m_SpriteMeshInstances.Length; j++)
		{
			SpriteMeshInstance spriteMeshInstance2 = this.spriteMeshInstances[j];
			if (spriteMeshInstance2.cachedSkinnedRenderer)
			{
				SkinnedMeshRenderer cachedSkinnedRenderer = spriteMeshInstance2.cachedSkinnedRenderer;
				BoneWeight[] boneWeights = spriteMeshInstance2.sharedMesh.boneWeights;
				for (int k = 0; k < boneWeights.Length; k++)
				{
					BoneWeight boneWeight = boneWeights[k];
					BoneWeight item = boneWeight;
					item.boneIndex0 += num2;
					item.boneIndex1 += num2;
					item.boneIndex2 += num2;
					item.boneIndex3 += num2;
					list2.Add(item);
				}
				num2 += spriteMeshInstance2.bones.Count;
				Transform[] bones = cachedSkinnedRenderer.bones;
				for (int l = 0; l < bones.Length; l++)
				{
					Transform item2 = bones[l];
					list.Add(item2);
				}
				CombineInstance item3 = default(CombineInstance);
				Mesh mesh = new Mesh();
				cachedSkinnedRenderer.BakeMesh(mesh);
				mesh.uv = spriteMeshInstance2.spriteMesh.sprite.uv;
				item3.mesh = mesh;
				array[j] = item3.mesh.vertexCount;
				item3.transform = cachedSkinnedRenderer.localToWorldMatrix;
				list3.Add(item3);
				cachedSkinnedRenderer.gameObject.SetActive(false);
			}
		}
		List<Matrix4x4> list4 = new List<Matrix4x4>();
		for (int m = 0; m < list.Count; m++)
		{
			list4.Add(list[m].worldToLocalMatrix * base.transform.worldToLocalMatrix);
		}
		SkinnedMeshRenderer skinnedMeshRenderer = base.gameObject.AddComponent<SkinnedMeshRenderer>();
		Mesh mesh2 = new Mesh();
		mesh2.CombineMeshes(list3.ToArray(), true, true);
		skinnedMeshRenderer.sharedMesh = mesh2;
		skinnedMeshRenderer.bones = list.ToArray();
		skinnedMeshRenderer.sharedMesh.boneWeights = list2.ToArray();
		skinnedMeshRenderer.sharedMesh.bindposes = list4.ToArray();
		skinnedMeshRenderer.sharedMesh.RecalculateBounds();
		skinnedMeshRenderer.materials = this.spriteMeshInstances[0].sharedMaterials;
		base.transform.position = position;
		base.transform.rotation = rotation;
		base.transform.localScale = localScale;
	}

	private void OnWillRenderObject()
	{
		if (this.cachedSkinnedRenderer && this.materialPropertyBlock != null)
		{
			this.materialPropertyBlock.SetTexture("_MainTex", this.spriteMeshInstances[0].spriteMesh.sprite.texture);
			this.cachedSkinnedRenderer.SetPropertyBlock(this.materialPropertyBlock);
		}
	}
}
