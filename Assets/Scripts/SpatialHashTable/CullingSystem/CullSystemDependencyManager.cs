﻿using System;
using System.Collections.Generic;

namespace SpatialHashTable.CullingSystem
{
    public class CullSystemDependencyManager : SpatialHashDependencyManager<CullableObjectTag>
    {
        List<CenterOfCullSystem> centerOfCullSystems = new List<CenterOfCullSystem>();

        // Start is called before the first frame update
       

        protected override void GetDependencies()
        {
            base.GetDependencies();
            HashManagersList.AddRange(FindObjectsOfType<CullSystemManager>());
            centerOfCullSystems.AddRange(FindObjectsOfType<CenterOfCullSystem>());
        }


        protected override void ConnectDependencies()
        {
            base.ConnectDependencies();

            foreach (var center in centerOfCullSystems)
            {
                center.Initiate((ICullSystemManager) HashManagersList.Find(x =>
                    x.HashTableSystemID == center.HashTableSystemID));
            }
        }
    }
}