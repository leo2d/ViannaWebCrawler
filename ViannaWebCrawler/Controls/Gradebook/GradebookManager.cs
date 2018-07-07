using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViannaWebCrawler
{
    public static class GradebookManager
    {
        public static Gradebook GetGradebookResume(string html)
        {
            var node = SelectGradebookTable(html);

            return new Gradebook()
            {
                GradebookResume = ExtractGradebook(node)
            };
        }
        private static List<Discipline> ExtractGradebook(HtmlNode node)
        {
            var lines = node.Descendants("tr").ToList();

            List<HtmlNode> cols;
            List<List<HtmlNode>> allCols = new List<List<HtmlNode>>() { };

            foreach (var tr in lines)
            {
                if (tr.InnerHtml.Contains("td"))
                {
                    cols = tr.Descendants("td").ToList();
                    allCols.Add(cols);
                }
            }

            List<Discipline> disciplines = new List<Discipline>() { };
            Discipline discipline;

            foreach (var col in allCols)
            {
                discipline = ParseToDiscipline(col);
                disciplines.Add(discipline);
            }

            return disciplines;
        }

        private static Discipline ParseToDiscipline(List<HtmlNode> col)
        {
            Discipline discipline = null;
            List<double> newCol = new List<double>();

            foreach (var item in col)
            {
                var rplaced = item.InnerText.Replace('.', ',');
                double.TryParse(rplaced, out double parsedDouble);

                newCol.Add(parsedDouble);
            }

            int.TryParse(col[6].InnerText, out int parsedInt);

            discipline = new Discipline()
            {
                Name = col[0].InnerText,
                FirstBimesterGrade = newCol[1],
                SecondBimesterGrade = newCol[2],
                Media = newCol[3],
                RetakeTestGrade = newCol[4],
                FinalMedia = newCol[5],
                MissedClasses = parsedInt,
                MissedClassPercentage = newCol[7]
            };

            return discipline;
        }

        private static HtmlNode SelectGradebookTable(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var tableNode = htmlDocument.DocumentNode.SelectSingleNode("//table[@class='table table-striped']");

            if (tableNode == null)
                throw new Exception("The xpath content has not be find.");

            return tableNode;
        }
    }
}
