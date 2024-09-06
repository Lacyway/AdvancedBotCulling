using EFT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace AdvancedBotCulling
{
	public class ABC_Culler : MonoBehaviour
	{
		public bool IsVisible
		{
			get
			{
				return cullingMode == CullingMode.Disabled || cullingMode == CullingMode.Visible || (cullingMode == CullingMode.Auto && cullingHandler.State);
			}
		}

		private GClass887 cullingObject;
		private GClass2403.Class1888 cullingHandler;
		private readonly List<Renderer> renderers = new(256);
		private CullingMode cullingMode;
		private Player cullerPlayer;

		public void Initialize(Player player, PlayerBones playerBones)
		{
			cullerPlayer = player;
			cullingObject = new(playerBones.BodyTransform.Original, EFTHardSettings.Instance.CULLING_PLAYER_SPHERE_RADIUS, EFTHardSettings.Instance.CULLING_PLAYER_DISTANCE);
			cullingHandler = new(cullingObject, EFTHardSettings.Instance.CULLING_PLAYER_DISTANCE * EFTHardSettings.Instance.CULLING_PLAYER_DISTANCE);
			cullingHandler.OnChange = (Action<bool>)Delegate.Combine(cullingHandler.OnChange, ToggleState);
			cullingObject.Register();

			cullerPlayer.HealthController.DiedEvent += DisableOnDead;

			cullingMode = CullingMode.Auto;
		}

		private void DisableOnDead(EDamageType obj)
		{
#if DEBUG
			ABC_Plugin.ABC_Logger.LogWarning($"{cullerPlayer.Profile.Nickname} has died, removing culler");
			ABC_Plugin.ABC_DebugHandler.RemoveCuller(this);
#endif
			cullerPlayer.HealthController.DiedEvent -= DisableOnDead;
			Disable();
			Destroy(this);
		}

		public void SetMode(CullingMode mode)
		{
			if (cullingMode == mode)
			{
				return;
			}
			cullingMode = mode;
			ToggleRenderers();
		}

		public void ToggleState(bool state)
		{
			ToggleRenderers();
		}

		public void LateUpdate()
		{
			if (cullingMode == CullingMode.Disabled || !gameObject.activeSelf)
			{
				return;
			}
			cullingHandler.ManualUpdate();
		}

		[Conditional("DEBUG")]
		public void ForceCull(bool enabled)
		{
			SetMode(enabled ? CullingMode.Hidden : CullingMode.Visible);
		}

		[Conditional("DEBUG")]
		public void Restore()
		{
			SetMode(CullingMode.Auto);
		}

		public void ToggleRenderers()
		{
			if (cullerPlayer == null)
			{
				return;
			}

			for (int i = 0; i < renderers.Count; i++)
			{
				renderers[i].forceRenderingOff = false;
			}

			renderers.Clear();
			bool isVisible = IsVisible;			
			cullerPlayer.PlayerBody.GetRenderersNonAlloc(renderers);
			for (int i = 0; i < renderers.Count; i++)
			{
				renderers[i].forceRenderingOff = !isVisible;
			}
#if DEBUG
			ABC_Plugin.ABC_Logger.LogWarning($"{cullerPlayer.Profile.Nickname} is now culled: {!isVisible}");
#endif
		}

		private void OnDestroy()
		{
			if (cullingObject == null)
			{
				return;
			}
			cullingMode = CullingMode.Disabled;
			cullingHandler.OnChange = (Action<bool>)Delegate.Remove(cullingHandler.OnChange, ToggleState);
			cullingObject.Dispose();
			cullingHandler.Dispose();
			cullingHandler = null;
			renderers.Clear();
		}

		public void Disable()
		{
			SetMode(CullingMode.Disabled);
		}
	}

	public enum CullingMode
	{
		Disabled,
		Hidden,
		Visible,
		Auto
	}
}
