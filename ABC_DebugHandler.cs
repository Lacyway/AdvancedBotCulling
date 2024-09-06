#if DEBUG
using System.Collections.Generic;

namespace AdvancedBotCulling
{
	public class ABC_DebugHandler
	{
		private readonly List<ABC_Culler> cullers = [];

		private void ClearCullers()
		{
			foreach (ABC_Culler culler in cullers)
			{
				RemoveCuller(culler);
			}
		}

		public void AddCuller(ABC_Culler culler)
		{
			if (!cullers.Contains(culler))
			{
				cullers.Add(culler);
				return;
			}

			ABC_Plugin.ABC_Logger.LogError("A culler was already part of the list!");
		}

		public void RemoveCuller(ABC_Culler culler)
		{
			if (!cullers.Remove(culler))
			{
				ABC_Plugin.ABC_Logger.LogError("There was an error trying to remove a culler!");
			}
		}

		private void ForceCull(bool enabled)
		{
			foreach (ABC_Culler culler in cullers)
			{
				culler.ForceCull(enabled);
			}
		}

		private void Restore()
		{
			foreach (ABC_Culler culler in cullers)
			{
				culler.Restore();
			}
		}
	}
}

#endif