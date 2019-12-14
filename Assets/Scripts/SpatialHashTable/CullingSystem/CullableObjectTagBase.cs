﻿using System.Collections.Generic;

namespace SpatialHashTable.CullingSystem
{
    public class CullableObjectTagBase : SpatialTagBase
    {
        List<ICullableObject> cullableObjects = new List<ICullableObject>();
        private bool isCulled;

        private void Start()
        {
            Initiate();
        }

        protected override void Initiate()
        {
            var cullableComponents = GetComponentsInChildren<ICullableObject>();
            foreach (var cullableObject in cullableComponents)
            {
                if (cullableObject.CullSystemID == HashManagerSystemID)
                {
                    cullableObjects.Add(cullableObject);
                }
            }


            var dependencyManager = FindObjectOfType<CullSystemDependencyManager>();
            dependencyManager.AddTagToManagerSystem(this, isStatic);
        }

        public void CullChildrenTags()
        {
            if (isCulled) return;
            foreach (var cullTag in cullableObjects)
            {
                cullTag.OnCulled();
            }

            isCulled = true;
        }

        public void UnCullChildrenTags()
        {
            if (!isCulled) return;
            foreach (var cullables in cullableObjects)
            {
                cullables.OnUnCulled();
            }

            isCulled = false;
        }
    }
}