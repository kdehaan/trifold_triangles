﻿// <copyright file="GraphDrawer.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// Used to maintain and visualize a representation of an evolving Polygon Graph.
    /// </summary>
    public class GraphDrawer
    {
        private static readonly Dictionary<Colour, Microsoft.Msagl.Drawing.Color> ColourReference
        = new Dictionary<Colour, Microsoft.Msagl.Drawing.Color>
        {
            { Colour.Red, Microsoft.Msagl.Drawing.Color.Red },
            { Colour.Green, Microsoft.Msagl.Drawing.Color.Green },
            { Colour.Blue, Microsoft.Msagl.Drawing.Color.Blue },
        };

        private System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        private Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        private Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        private int nextNodeName = 0;



        /// <summary>
        /// Initializes a new instance of the <see cref="GraphDrawer"/> class.
        /// </summary>
        /// <param name="border">Inital node perimeter.</param>
        public GraphDrawer(List<PolygonNode> border)
        {

            PolygonNode lastNode = new PolygonNode(-1, Colour.Red);
            PolygonNode firstNode = new PolygonNode(-1, Colour.Red);

            // Note: structs are copied on assignment
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct
            foreach (PolygonNode node in border)
            {
                this.CreateNode(node);

                if (lastNode.Id == -1)
                {
                    firstNode = node;
                }
                else
                {
                    this.CreateEdge(lastNode, node);
                }

                lastNode = node;
            }

            this.CreateEdge(lastNode, firstNode);
        }

        /// <summary>
        /// Produces a visualization of the current GraphDrawer object.
        /// </summary>
        public void DrawGraph()
        {
            this.viewer.Graph = this.graph;
            this.form.SuspendLayout();
            this.viewer.Dock = DockStyle.Fill;
            this.form.Controls.Add(this.viewer);
            this.form.ResumeLayout();
            this.form.ShowDialog();
        }

        private void CreateEdge(PolygonNode a, PolygonNode b)
        {
            Microsoft.Msagl.Drawing.Edge edge = this.graph.AddEdge(a.Id.ToString(), b.Id.ToString());
            edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
        }

        private void CreateNode(PolygonNode polygonNode)
        {
            Microsoft.Msagl.Drawing.Node node = this.graph.AddNode(polygonNode.Id.ToString());
            node.Attr.Color = ColourReference[polygonNode.Type];
            node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
        }

        //public void JoinNode(int nodeId, List<>)

    }
}
