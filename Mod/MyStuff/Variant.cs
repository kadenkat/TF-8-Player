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
	public class MyVariant : Variant
	{
		public MyVariant(Monocle.Subtexture icon, string title, string description, Pickups[] itemExclusions, bool perPlayer, string header, UnlockData.Unlocks? unlocker, bool scrollEffect, bool hidden, bool canRandom, bool tournamentRule1v1, bool tournamentRule2v2, bool unlisted, bool darkWorldDLC, int coOpValue) : base(icon, title, description, itemExclusions, perPlayer, header, unlocker, scrollEffect, hidden, canRandom, tournamentRule1v1, tournamentRule2v2, unlisted, darkWorldDLC, coOpValue)
		{
			
		}
		
	}
}
