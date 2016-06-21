﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Notes
 */ 

namespace SpatialSlur.SlurMesh
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class HeElement
    {
        private int _index = -1;
        private int _tag; // tag for topological searches, validation etc.


        /// <summary>
        /// Returns the element's position within the collection of the parent mesh.
        /// </summary>
        public int Index
        {
            get { return _index; }
            internal set { _index = value; }
        }

        
        /// <summary>
        /// 
        /// </summary>
        internal int Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
 

        /// <summary>
        /// Returns true if the element lies on the mesh boundary.
        /// </summary>
        public abstract bool IsBoundary { get; }


        /// <summary>
        /// Returns true if the element has been flagged for removal.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsUnused { get; }


        /// <summary>
        /// Returns false for non-manifold elements.
        /// </summary>
        internal abstract bool IsValid { get; }


        /// <summary>
        /// Flags the element for removal.
        /// </summary>
        internal abstract void MakeUnused();


        /// <summary>
        /// Throws an exception for unused elements.
        /// </summary>
        internal void UsedCheck()
        {
            if (IsUnused)
                throw new ArgumentException("The given element must be in use.");
        }
    }
}
