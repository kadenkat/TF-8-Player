using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerFall;
using Microsoft.Xna.Framework;
using Patcher;
using Monocle;
using System.Xml;

namespace Mod
{
	[Patch]
	public class MyPlayerGhost : PlayerGhost
	{
		//
		// ALLOW TRIGGER CORPSES AND GHOSTS SIMULTANEOUSLY
		//

		MyPlayerCorpse ownerCorpse;

		public MyPlayerGhost(PlayerCorpse corpse) : base(corpse)
		{
			ownerCorpse = (MyPlayerCorpse)corpse;
		}

		public override bool OnArrowHit(Arrow arrow)
		{
			if ((((MyMatchVariants)arrow.Level.Session.MatchSettings.Variants).ThreeSpookyFiveMe) && (arrow.ArrowType != ArrowTypes.Toy))
				return false;

			return base.OnArrowHit(arrow);
		}

		public override void Die(int killerIndex, Arrow arrow = null, Explosion explosion = null, ShockCircle shock = null)
		{
			ownerCorpse.RespawnGhost();
			base.Die(killerIndex, arrow, explosion, shock);
		}
	}
}