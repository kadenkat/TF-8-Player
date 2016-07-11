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
		public struct GraphicProperties
		{
			public Vector2 Position;
			public Vector2 Origin;
			public Vector2 Scale;
			public float Zoom;
			public float Rotation;

			public void SaveProperties(GraphicsComponent graphic)
			{
				Position = graphic.Position;
				Origin = graphic.Origin;
				Scale = graphic.Scale;
				Zoom = graphic.Zoom;
				Rotation = graphic.Rotation;
			}

			public static void RestoreProperties(GraphicsComponent graphic, GraphicProperties properties)
			{
				graphic.Position = properties.Position;
				graphic.Origin = properties.Origin;
				graphic.Scale = properties.Scale;
				graphic.Zoom = properties.Zoom;
				graphic.Rotation = properties.Rotation;
			}
		}

		MyPlayerCorpse ownerCorpse;
		int characterIndex;
		public CharacterSounds archerSounds;
		public ArrowTypes ghostArrow = ArrowTypes.Toy;
		public bool hasArrow = false;
		public GraphicProperties haloProperties;
		public WrapHitbox arrowPickupHitbox;

		public MyPlayerGhost(PlayerCorpse corpse) : base(corpse)
		{
			ownerCorpse = (MyPlayerCorpse)corpse;
			characterIndex = ownerCorpse.PlayerIndex;
			archerSounds = TowerFall.ArcherData.Archers[characterIndex].SFX;
			haloProperties.SaveProperties(halo);

			this.arrowPickupHitbox = new WrapHitbox(22f, 30f, -11f, -16f);
		}

		public void AddArrow(ArrowTypes arrowType, SFX sound)
		{
			this.halo.Position = new Vector2(0f, -12f);
			this.halo.Zoom = haloProperties.Zoom * 1.5f;
			this.halo.Rotation = 1.5f;

			if(this.sprite.CurrentAnimID == "normal")
				this.sprite.Play("happy", false);
			
			ghostArrow = arrowType;
			sound.Play(base.X, 1f);
			hasArrow = true;
		}

		public void DropArrow()
		{
			if (hasArrow == true)
			{
				Arrow arrow = Arrow.Create(ghostArrow, this, this.Position, Monocle.Calc.Random.Range(-2.3561945f, 1.57079637f), characterIndex, characterIndex);
				arrow.Drop((-(int)this.Facing));
				((MyLevel)base.Level).AddEntity(arrow);
				GraphicProperties.RestoreProperties(halo, haloProperties);
				hasArrow = false;
			}
		}

		public bool CollectArrow(ArrowTypes arrowType)
		{
			if ((((MyMatchVariants)this.Level.Session.MatchSettings.Variants).ArcherGhosts)
				&& (sprite.CurrentAnimID != "dodge")
				&& (hasArrow == false))
			{
				AddArrow(arrowType, archerSounds.ArrowRecover);
				return true;
			}
			return false;
		}

		public void StealArrow(Player victim)
		{
			if ((((MyMatchVariants)this.Level.Session.MatchSettings.Variants).ArcherGhosts)
				&& (hasArrow == false)
				&& (victim.Arrows.HasArrows))
			{
				AddArrow(victim.Arrows.UseArrow(), victim.ArcherData.SFX.ArrowSteal);
			}
		}

		public void ShootArrow()
		{
			if (hasArrow == true)
			{
				archerSounds.FireArrow.Play(this.X, 1f);
				Arrow arrow = Arrow.Create(ghostArrow, this, this.Position + Player.ArrowOffset, this.Speed.Angle(), characterIndex, characterIndex);
				((MyLevel)base.Level).AddEntity(arrow);
				GraphicProperties.RestoreProperties(halo, haloProperties);
				this.sprite.Play("normal", false);
				hasArrow = false;
			}
		}

		public override bool OnArrowHit(Arrow arrow)
		{
			if ((((MyMatchVariants)arrow.Level.Session.MatchSettings.Variants).ThreeSpookyFiveMe) && (arrow.ArrowType != ArrowTypes.Toy))
				return false;

			return base.OnArrowHit(arrow);
		}

		public override void DodgeEnter()
		{
			ShootArrow();
			base.DodgeEnter();
		}

		public override void OnPlayerTouch(Player player)
		{
			if (base.State == 0)
				return;

			StealArrow(player);

			if (((MyMatchVariants)this.Level.Session.MatchSettings.Variants).CuddlyGhosts)
			{
				player.Speed = (player.Position - this.Position).SafeNormalize(4.5f);
				this.Speed = (this.Position - player.Position).SafeNormalize(3.0f);
			}
			else
				base.OnPlayerTouch(player);
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
			DropArrow();
			base.Die(killerIndex, arrow, explosion, shock);
		}

		public override void Update()
		{
			base.Update();

			//if ((this.sprite.CurrentAnimID == "normal") && (hasArrow))
			//	this.sprite.Play("happy", false);

			if (this.Collidable)
			{
				Monocle.Collider collider = base.Collider;
				base.Collider = collider;
				base.Collider = this.arrowPickupHitbox;
				Monocle.Entity entity = base.CollideFirst(Monocle.GameTags.Arrow);
				if ((entity != null) && (entity as Arrow))
				{
					((MyArrow)entity).OnGhostCollect(this, false);
				}
				base.Collider = collider;
			}
		}
	}
}