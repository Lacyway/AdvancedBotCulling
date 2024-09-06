using BepInEx;
using BepInEx.Logging;

namespace AdvancedBotCulling
{
	[BepInPlugin("com.lacyway.abc", "AdvancedBotCulling", "1.0.0")]
	public class ABC_Plugin : BaseUnityPlugin
	{
		public static ManualLogSource ABC_Logger;
#if DEBUG
		public static ABC_DebugHandler ABC_DebugHandler; 
#endif

		protected void Awake()
		{
			ABC_Logger = Logger;
#if DEBUG
			ABC_DebugHandler = new();
#endif
			ABC_Logger.LogInfo($"{nameof(ABC_Plugin)} has been loaded.");
			new Player_SetOwnerToAIData_Patch().Enable();
		}
	}
}
