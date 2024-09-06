using EFT;
using SPT.Reflection.Patching;
using System.Reflection;

namespace AdvancedBotCulling
{
	internal class Player_SetOwnerToAIData_Patch : ModulePatch
	{
		protected override MethodBase GetTargetMethod()
		{
			return typeof(Player).GetMethod(nameof(Player.SetOwnerToAIData));
		}

		[PatchPostfix]
		public static void Postfix(Player __instance)
		{
			ABC_Culler culler = __instance.gameObject.AddComponent<ABC_Culler>();
			culler.Initialize(__instance, __instance.PlayerBones);
#if DEBUG
			ABC_Plugin.ABC_Logger.LogWarning($"Adding {__instance.Profile.Nickname} to cullers");
			ABC_Plugin.ABC_DebugHandler.AddCuller(culler);
#endif
		}
	}
}
