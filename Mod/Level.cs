﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using Patcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TowerFall;

namespace Mod
{
    [Patch]
    public class MyLevel : Level
    {
        public MyLevel(TowerFall.Session session, XmlElement xml) : base(session, xml)
        {
            this.controllerAttachedFlags = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                this.controllerAttachedFlags[i] = true;
            }
        }

		public Entity AddEntity(Entity entity)
		{
			base.Layers[entity.LayerIndex].Add(entity, false);
			return entity;
		}
	}
}
