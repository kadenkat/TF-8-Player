using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerFall;
using Microsoft.Xna.Framework;
using Patcher;
using Monocle;

namespace Mod
{
	[Patch]
	public class MyMatchVariants : MatchVariants
	{
		public MyVariant WeakPrismArrows;
		public MyVariant WeakBoltArrows;

		public MyVariant RedRover;

		public MyVariant GhostsRespawn;
		public MyVariant CuddlyGhosts;
		public MyVariant ArcherGhosts;
		public MyVariant ThreeSpookyFiveMe;

		public MyMatchVariants(bool noPerPlayer = false) : base(noPerPlayer)
		{
			TriggerCorpses.Links.Remove(ReturnAsGhosts);
			ExplodingCorpses.Links.Remove(ReturnAsGhosts);
			ReturnAsGhosts.Links.Remove(TriggerCorpses);
		}
	}
}