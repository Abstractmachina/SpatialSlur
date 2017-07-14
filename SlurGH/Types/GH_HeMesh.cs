﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

using SpatialSlur.SlurCore;
using SpatialSlur.SlurMesh;

using SpatialSlur.SlurRhino;

/*
 * Notes
 */

namespace SpatialSlur.SlurGH.Types
{
    using M = HeMesh<HeMesh3d.V, HeMesh3d.E, HeMesh3d.F>;

    /// <summary>
    /// 
    /// </summary>
    public class GH_HeMesh : GH_GeometricGoo<M>
    {
        private BoundingBox _bbox = BoundingBox.Unset;


        /// <summary>
        /// 
        /// </summary>
        public GH_HeMesh()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public GH_HeMesh(M value)
        {
            Value = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        public GH_HeMesh(GH_HeMesh other)
        {
            Value = other.Value;
        }


        /// <summary>
        /// 
        /// </summary>
        public override bool IsValid
        {
            get { return true; }
        }


        /// <summary>
        /// 
        /// </summary>
        public override string TypeName
        {
            get { return "HeMesh"; }
        }


        /// <summary>
        /// 
        /// </summary>
        public override string TypeDescription
        {
            get { return "HeMesh"; }
        }


        /// <summary>
        /// 
        /// </summary>
        public override BoundingBox Boundingbox
        {
            get
            {
                if(Value != null && !_bbox.IsValid)
                    _bbox = new Domain3d(Value.Vertices.Select(v => v.Position)).ToBoundingBox();

                return _bbox;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IGH_Goo Duplicate()
        {
            return DuplicateGeometry();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            return new GH_HeMesh(Value.Duplicate());
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object ScriptVariable()
        {
            return Value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override bool CastFrom(object source)
        {
            if(source is GH_Goo<M>)
            {
                Value = ((GH_Goo<M>)source).Value;
                return true;
            }

            if (source is M)
            {
                Value = (M) source;
                return true;
            }

            if(source is GH_Mesh)
            {
                Value = ((GH_Mesh)source).Value.ToHeMesh();
                return true;
            }

            if (source is Mesh)
            {
                Value = ((Mesh)source).ToHeMesh();
                return true;
            }

            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public override bool CastTo<Q>(ref Q target)
        {
            if (typeof(Q) == typeof(GH_Goo<M>))
            {
                object obj = new GH_HeMesh(Value);
                target = (Q)obj;
                return true;
            }

            if (typeof(Q) == typeof(M))
            {
                object obj = Value;
                target = (Q)obj;
                return true;
            }

            if (typeof(Q) == typeof(GH_Mesh))
            {
                object obj = new GH_Mesh(Value.ToMesh());
                target = (Q)obj;
                return true;
            }

            if (typeof(Q) == typeof(Mesh))
            {
                object obj = Value.ToMesh();
                target = (Q)obj;
                return true;
            }

            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xform"></param>
        /// <returns></returns>
        public override BoundingBox GetBoundingBox(Transform xform)
        {
            var b = Boundingbox;
            b.Transform(xform);
            return b;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xform"></param>
        /// <returns></returns>
        public override IGH_GeometricGoo Transform(Transform xform)
        {
            var copy = Value.Duplicate(); // TODO check if need to copy before transform?
            copy.Transform(xform);
            return new GH_HeMesh(copy);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmorph"></param>
        /// <returns></returns>
        public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
        {
            var copy = Value.Duplicate(); // TODO check if need to copy before transform?
            copy.SpaceMorph(xmorph);
            return new GH_HeMesh(copy);
        }
    }
}