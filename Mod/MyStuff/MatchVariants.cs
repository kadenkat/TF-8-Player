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
		public Variant WeakPrismArrows;
		public Variant WeakBoltArrows;
		public Variant RedRover;
		public Variant ThreeSpookyFiveMe;
		public Variant GhostsRespawn;
	}
}