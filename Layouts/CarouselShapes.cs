using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.Localization;

namespace Conekt.Bootstrap.ProjectionLayout.Layouts
{
    public class CarouselShapes : IDependency {
        public CarouselShapes() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

		[Shape]
		public void Carousel(dynamic Display, TextWriter Output, HtmlHelper Html, string Id, IEnumerable<dynamic> Items, IEnumerable<string> OuterClasses, IDictionary<string, string> OuterAttributes, IEnumerable<string> InnerClasses, IDictionary<string, string> InnerAttributes, IEnumerable<string> FirstItemClasses, IDictionary<string, string> FirstItemAttributes, IEnumerable<string> ItemClasses, string Interval, IDictionary<string, string> ItemAttributes)
		{
			if (Items == null) return;

			var items = Items.ToList();
			var itemsCount = items.Count;

			if (itemsCount < 1) return;

			var outerDivTag = GetTagBuilder("div", Id, OuterClasses, OuterAttributes);
			var innerDivTag = GetTagBuilder("div", string.Empty, InnerClasses, InnerAttributes);
			var firstItemTag = GetTagBuilder("div", string.Empty, FirstItemClasses, FirstItemAttributes);
			var itemTag = GetTagBuilder("div", string.Empty, ItemClasses, ItemAttributes);
			var paginationTag = GetTagBuilder("ol", string.Empty, new List<string>() { "carousel-indicators" }, null);

			Output.Write(outerDivTag.ToString(TagRenderMode.StartTag));

			//Carousel Pagination
			Output.Write(paginationTag.ToString(TagRenderMode.StartTag));
			int slide = 0;
			foreach (var item in items)
			{
				Output.Write("<li " + (slide == 0 ? "class=\"active\"" : null) + " data-target=\"#" + Id + "\" data-slide-to=\"" + slide + "\"></li>", Id);
				slide++;
			}
			Output.Write(paginationTag.ToString(TagRenderMode.EndTag));

			Output.Write(innerDivTag.ToString(TagRenderMode.StartTag));

			int i = 0;

			foreach (var item in items)
			{
				if (i == 0)
					Output.Write(firstItemTag.ToString(TagRenderMode.StartTag));
				else
					Output.Write(itemTag.ToString(TagRenderMode.StartTag));

				Output.Write(Display(item));
				Output.Write(itemTag.ToString(TagRenderMode.EndTag));
				i++;
			}

			Output.Write(innerDivTag.ToString(TagRenderMode.EndTag));

			//Carousel Nav
			Output.Write("<a href=\"#{0}\" class=\"carousel-control left\" data-slide=\"prev\">&lsaquo;</a>", Id);
			Output.Write("<a href=\"#{0}\" class=\"carousel-control right\" data-slide=\"next\">&rsaquo;</a>", Id);

			Output.Write(outerDivTag.ToString(TagRenderMode.EndTag));

			Output.Write("<script>$(function () {$('#" + Id + "').carousel({" + (!string.IsNullOrEmpty(Interval) ? "interval: " + Interval : null) + "});}); </script>");
		}

        static TagBuilder GetTagBuilder(string tagName, string id, IEnumerable<string> classes, IDictionary<string, string> attributes) {
            var tagBuilder = new TagBuilder(tagName);
            tagBuilder.MergeAttributes(attributes, false);
            foreach (var cssClass in classes ?? Enumerable.Empty<string>())
                tagBuilder.AddCssClass(cssClass);
            if (!string.IsNullOrWhiteSpace(id))
                tagBuilder.GenerateId(id);
            return tagBuilder;
        }

    }
}
