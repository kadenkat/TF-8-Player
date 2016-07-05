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
		bool hasArrow;
		ArrowTypes stolenArrowType;

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

		public override void DodgeEnter()
		{
			if(hasArrow)
			{
				Arrow arrow = Arrow.Create(stolenArrowType, this, this.Position + Player.ArrowOffset, this.Speed.Angle(), null, null);
				//(Engine.Instance.Scene as Level).Add<Arrow>(arrow);
				//stolenArrow.Level.Add<Arrow>(arrow);
				//stolenArrow = null;
			}

			base.DodgeEnter();
		}

		public override void OnPlayerTouch(Player player)
		{
			if (base.State == 0)
			{
				return;
			}
			if (((MyMatchVariants)this.Level.Session.MatchSettings.Variants).ThreeSpookyFiveMe)
			{
				this.sprite.Play("happy", false);
				player.Speed = (player.Position - this.Position).SafeNormalize(4.5f);
				this.Speed = (this.Position - player.Position).SafeNormalize(3.0f);
			}
			else if (((MyMatchVariants)this.Level.Session.MatchSettings.Variants).GhostsRespawn)
			{
				if (hasArrow == false)
				{
					this.sprite.Play("happy", false);
					stolenArrowType = player.Arrows.UseArrow();
					hasArrow = true;
					player.ArcherData.SFX.ArrowSteal.Play(base.X, 1f);
				}
				player.Speed = (player.Position - this.Position).SafeNormalize(3.2f);
				this.Speed = (this.Position - player.Position).SafeNormalize(3.0f);
			}
			else
			{
				base.OnPlayerTouch(player);
			}
			
		}

		public override void Die(int killerIndex, Arrow arrow = null, Explosion explosion = null, ShockCircle shock = null)
		{
			if (this.PlayerIndex != -1
			&& base.Level.Session.MatchSettings.Variants.ReturnAsGhosts[this.PlayerIndex]
			&& ((MyMatchVariants)this.Level.Session.MatchSettings.Variants).GhostsRespawn
			&& ownerCorpse != null)
			{
				ownerCorpse.RespawnGhost();
			}
			base.Die(killerIndex, arrow, explosion, shock);
		}
	}
}