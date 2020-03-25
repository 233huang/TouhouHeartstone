﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TouhouCardEngine;
using TouhouCardEngine.Interfaces;
namespace TouhouHeartstone.Builtin
{
    public class BudFairy : ServantCardDefine
    {
        public override int id { get; set; } = 1;
        public override int cost
        {
            get { return 0; }
        }
        public override int attack
        {
            get { return 1; }
        }
        public override int life
        {
            get { return 1; }
        }
        public override IEffect[] effects { get; }
    }
}