﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialSlur.SlurGraph
{
    /// <summary>
    /// TODO make generic for attaching attributes
    /// </summary>
    public class Node
    {
        private readonly List<Edge> _edges;
        //private V _data;
        private int _index = -1;
        private int _degree; // hold degree explicitly to keep up with edge/node removal


        /// <summary>
        /// 
        /// </summary>
        internal Node(int index)
        {
            _edges = new List<Edge>();
            _index = index;
        }


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Edge> IncidentEdges
        {
            get
            {
                for(int i = 0; i < _edges.Count; i++)
                {
                    Edge e = _edges[i];
                    if (!e.IsRemoved) yield return e;
                }
            }
        }


        /// <summary>
        /// Returns the number of edges incident to this node.
        /// This accoutns for any edges which have been flagged for removal.
        /// </summary>
        public int Degree
        {
            get { return _degree; }
        }


        /// <summary>
        /// Returns the index of the node within the graph's node list.
        /// This will be set to -1 if the node is removed.
        /// </summary>
        public int Index
        {
            get { return _index; }
            internal set { _index = value; }
        }


        /// <summary>
        /// Returns true if this node has been flagged for removal.
        /// </summary>
        public bool IsRemoved
        {
            get { return _index == -1; }
        }


        /// <summary>
        /// Flags the node for removal.
        /// </summary>
        internal void Remove()
        {
            _index = -1;
        }


        /// <summary>
        /// Removes all flagged edges from the edge list.
        /// </summary>
        internal void Compact()
        {
            int marker = 0;

            for (int i = 0; i < _edges.Count; i++)
            {
                Edge e = _edges[i];
                if (!e.IsRemoved)
                    _edges[marker++] = e;
            }

            _edges.RemoveRange(marker, _edges.Count - marker); // trim list to include only used elements
        }


        /// <summary>
        /// Returns true if an edge exists between this node and the given node.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsConnectedTo(Node other)
        {
            return FindEdgeTo(other) != null;
        }


        /// <summary>
        /// Searched for an edge between this node and the given node.
        /// Returns null if no edge exists.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Edge FindEdgeTo(Node other)
        {
            for(int i = 0; i < _edges.Count; i++)
            {
                Edge e = _edges[i];
                if (e.GetOther(this) == other)
                    return e;
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        internal void AddEdge(Edge edge)
        {
            _edges.Add(edge);
            _degree++;
        }


        /// <summary>
        /// 
        /// </summary>
        internal void RemoveEdge()
        {
            _degree--;
        }
    }
}
