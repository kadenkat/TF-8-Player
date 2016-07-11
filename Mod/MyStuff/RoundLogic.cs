using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerFall;
using Microsoft.Xna.Framework;
using Patcher;
using Monocle;
using System.Xml;
using System.Collections;

namespace Mod
{
	//[Patch]
	public class MyRoundLogic : RoundLogic
	{
		public MyRoundLogic(Session session, bool canHaveMiasma) : base(session, canHaveMiasma)
		{
		}

		public override void OnPlayerDeath(Player player, PlayerCorpse corpse, int playerIndex, DeathCause cause, Vector2 position, int killerIndex)
		{
			//if(cause == DeathCause.Arrow) && base.Level.Session.
			//base.OnPlayerDeath(player, corpse, playerIndex, cause, position, killerIndex);
		}
	}
}