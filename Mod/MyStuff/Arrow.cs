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
	public abstract class MyArrow : Arrow
	{
		public void OnGhostCollect(MyPlayerGhost ghost, bool force = false)
		{
			if (force || this.IsCollectible)
			{
				if ((ghost != null) && (ghost.CollectArrow(this.ArrowType)))
				{
					if (this.BuriedIn != null)
					{
						this.BuriedIn.OnCollect();
					}
					if (base.Scene != null)
					{
						base.RemoveSelf();
					}
					this.Collidable = false;
				}
			}
		}
	}
}