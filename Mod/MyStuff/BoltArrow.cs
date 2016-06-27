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
	public class MyBoltArrow : BoltArrow
	{
		public override bool TryTurn(float turnAngle)
		{
			if ((Turns >= 1) && ((MyMatchVariants)Level.Session.MatchSettings.Variants).WeakBoltArrows)
				return false;

			return base.TryTurn(turnAngle);
		}
	}
}