﻿using BotlyOrbit.Game.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BotlyOrbit.Game.Objects
{
    internal class EntityFactory
    {

        static readonly Dictionary<Regex, EntityType> TypeRegexMap = new Dictionary<Regex, EntityType>()
        {
            { new Regex(@"^box_.*", RegexOptions.IgnoreCase), EntityType.Box },
            { new Regex(@"^ore_.*", RegexOptions.IgnoreCase), EntityType.Loot },
        };

        public static Entity Create(IntPtr address)
        {
            Entity ent = new Entity();
            ent.update(address);
            if (ent.Address == IntPtr.Zero)
                return null;
            
            switch (GetEntityTypeFromName(ent.AssetName))
            {
                case EntityType.Box: return new Box(address);
                case EntityType.Loot: return new Box(address);
                case EntityType.Ship: return new Ship(address);
                default:
                    {
                        if (ent.IsShip())
                            return new Ship(address);  
                        return ent;
                    }
            }
        }

        private static EntityType GetEntityTypeFromName(string name)
        {
            foreach (var pair in TypeRegexMap)
            {
                if (pair.Key.IsMatch(name))
                    return pair.Value;
            }

            return EntityType.Unknown;
        }

    }
}
