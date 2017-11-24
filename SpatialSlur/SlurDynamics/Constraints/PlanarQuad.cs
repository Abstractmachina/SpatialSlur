﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialSlur.SlurCore;

using static SpatialSlur.SlurCore.GeometryUtil;

/*
 * Notes
 */

namespace SpatialSlur.SlurDynamics.Constraints
{
    using H = ParticleHandle;

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PlanarQuad : Constraint, IConstraint
    {
        private H _h0 = new H();
        private H _h1 = new H();
        private H _h2 = new H();
        private H _h3 = new H();


        /// <summary>
        /// 
        /// </summary>
        public H Handle0
        {
            get { return _h0; }
        }


        /// <summary>
        /// 
        /// </summary>
        public H Handle1
        {
            get { return _h1; }
        }


        /// <summary>
        /// 
        /// </summary>
        public H Handle2
        {
            get { return _h2; }
        }


        /// <summary>
        /// 
        /// </summary>
        public H Handle3
        {
            get { return _h3; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="i0"></param>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="i3"></param>
        /// <param name="weight"></param>
        public PlanarQuad(int i0, int i1, int i2, int i3, double weight = 1.0)
        {
            _h0.Index = i0;
            _h1.Index = i1;
            _h2.Index = i2;
            _h3.Index = i3;

            Weight = weight;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="particles"></param>
        public void Calculate(IReadOnlyList<IBody> particles)
        {
            var d = LineLineShortestVector(
                particles[_h0].Position,
                particles[_h2].Position,
                particles[_h1].Position,
                particles[_h3].Position) * 0.5;

            _h0.Delta = _h2.Delta = d;
            _h1.Delta = _h3.Delta = -d;
        }


        /// <inheritdoc/>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodies"></param>
        public void Apply(IReadOnlyList<IBody> bodies)
        {
            bodies[_h0].ApplyMove(_h0.Delta, Weight);
            bodies[_h1].ApplyMove(_h1.Delta, Weight);
            bodies[_h2].ApplyMove(_h2.Delta, Weight);
            bodies[_h3].ApplyMove(_h3.Delta, Weight);
        }


        #region Explicit interface implementations

        /// <inheritdoc/>
        /// <summary>
        /// 
        /// </summary>
        bool IConstraint.AppliesRotation
        {
            get { return false; }
        }


        /// <inheritdoc/>
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IHandle> IConstraint.Handles
        {
            get
            {
                yield return _h0;
                yield return _h1;
                yield return _h2;
                yield return _h3;
            }
        }

        #endregion
    }
}
