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
	[Patch]
	public class MyPlayerCorpse : PlayerCorpse
	{
		public MyPlayerCorpse(Player player, int killerIndex) : base(player, killerIndex)
		{
			// New---
			if (killerIndex != -1)
			{
				MatchTeams teams = ((MyMatchSettings)player.Level.Session.MatchSettings).Teams;
				int teamSize = teams.GetAmountOfPlayersOfAllegiance(this.Allegiance);

				Engine.Instance.Commands.Log(killerIndex + " killed " + this.PlayerIndex);
				Engine.Instance.Commands.Log("Killer's Team: " + teams[killerIndex]);
				Engine.Instance.Commands.Log("Player's Team: " + player.Allegiance);

				if ((((MyMatchVariants)player.Level.Session.MatchSettings.Variants).RedRover)
				&& (teamSize > 1)
				&& (this.Allegiance != Allegiance.Neutral))
				{
					this.Allegiance = teams[killerIndex];
					this.TeamColor = teams[killerIndex];
					Engine.Instance.Commands.Log(player.Allegiance + " => " + this.Allegiance);

					if (player.Level.Session.CurrentLevel.IsPlayerAlive(killerIndex) == false)
					{
						Engine.Instance.Commands.Log("Killer is dead.");
						teams[player.PlayerIndex] = teams[killerIndex];
					}
						
				}
			}
		}

		private MyPlayerCorpse(string corpseSpriteID, Allegiance teamColor, Vector2 position, Facing facing, int playerIndex, int killerIndex) : base(corpseSpriteID, teamColor, position, facing, playerIndex, killerIndex)
		{
			// ADDED--
			if (this.TeamColor != teamColor)
				teamColor = this.TeamColor;
			// --ADDED
		}

		public override bool CanDoPrismHit(Arrow arrow)
		{
			if (((MyMatchVariants)arrow.Level.Session.MatchSettings.Variants).WeakPrismArrows)
				return false;

			return base.CanDoPrismHit(arrow);
		}

		public void RespawnGhost()
		{
			base.ghostCoroutine = new Monocle.Coroutine(base.GhostSpawnSequence());
		}
	}
}