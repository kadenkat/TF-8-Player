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
	public class MyTeamReviver : TeamReviver
	{
		//public MyLevel myLevel;
		public MyTeamReviver(PlayerCorpse corpse, TeamReviver.Modes mode) : base(corpse, mode)
		{
			//myLevel = (Engine.Instance.Scene as MyLevel);
		}

		public override void ReviveUpdate()
		{
			if ((this.Finished == true)
			&& (((MyMatchVariants)(Engine.Instance.Scene as Level).Session.MatchSettings.Variants).RedRover)
			&& (((MyMatchSettings)(Engine.Instance.Scene as Level).Session.MatchSettings).Teams[this.Corpse.PlayerIndex] != this.Corpse.TeamColor))
			{
				Engine.Instance.Commands.Log("TChange: " + this.Corpse.Allegiance + " => " + this.Corpse.TeamColor);
				((MyMatchSettings)(Engine.Instance.Scene as Level).Session.MatchSettings).Teams[this.Corpse.PlayerIndex] = this.Corpse.TeamColor;
			}
			base.ReviveUpdate();
		}
	}
}
