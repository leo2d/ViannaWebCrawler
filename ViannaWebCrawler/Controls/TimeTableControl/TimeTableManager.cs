using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ViannaWebCrawler.Entities;

namespace ViannaWebCrawler.Controls.TimeTableControl
{
    public static class TimeTableManager
    {
        public static TimeTable GetTimeTable(string html)
        {
            return new TimeTable()
            {
                ClassSchedule = SelectTimeTable(html)
            };
        }

        private static List<ClassDay> SelectTimeTable(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            string xpath;

            //Pega a linha que contem os dias da semana
            xpath = "//th[@style='width: 18%;']";
            var nodeDays = document.DocumentNode.SelectNodes(xpath);
            nodeDays = ValidateNodes(nodeDays, xpath);

            var days = nodeDays.Select(n => n.InnerText).ToList();

            var classDays = new List<ClassDay>();

            //Pega a coluna com a ordem dos horarios
            xpath = "//tbody/tr[td]/*[1]";
            var schedulesNodes = document.DocumentNode.SelectNodes(xpath);
            schedulesNodes = ValidateNodes(schedulesNodes, xpath);

            var schedules = schedulesNodes.Select(o => o.InnerText.Replace("&ordm;", String.Empty)).ToList();

            for (int i = 2, j = 0; i < nodeDays.Count + 2; i++, j++)
            {
                xpath = $"//tbody/tr[td]/*[{i}]/abbr";
                var nodes = document.DocumentNode.SelectNodes(xpath);
                nodes = ValidateNodes(nodes, xpath);

                classDays.Add(ParseToClassDay(nodes, days[j], schedules));
            }

            return classDays;
        }

        public static HtmlNodeCollection ValidateNodes(HtmlNodeCollection nodes, string xpath)
        {
            if (nodes == null)
                throw new NodeNotFoundException($"Could not found node {xpath}");

            return nodes;
        }

        private static ClassDay ParseToClassDay(HtmlNodeCollection nodes, string day, List<string> schedules)
        {
            if (day.Equals("Sexta"))
                Console.Write("adasdad");
            //Abreviacao do nome da disciplina.
            var nickNames = nodes.Select(x => x.InnerText.Trim()).ToList();

            //Embora no site esteja a abeviacao do nome, optei por preencher os objetos com o nome completo
            var names = nodes.Select(a => a.GetAttributeValue("title", "")).ToList();

            //Remove um codigo estranho que fica na frente do nome, ex: #1047 - nome
            Regex reg = new Regex("^[^a-zA-Z]+");
            names = names.Select(s => reg.Replace(s, String.Empty)).ToList();

            var disciplines = new List<TimeTableDiscipline>();

            for (int i = 0; i < nickNames.Count; i++)
            {
                disciplines.Add
                    (
                        new TimeTableDiscipline()
                        {
                            NickName = nickNames[i],
                            Name = names[i]
                        }
                    );
            }

            //Dicionario tem a funcao de garantir que o horario estara na mesma posicado do site
            var dayDisciplines = new Dictionary<string, TimeTableDiscipline>();

            for (int i = 0; i < disciplines.Count; i++)
                dayDisciplines.Add(schedules[i], disciplines[i]);

            return new ClassDay()
            {
                DayOfWeek = day,
                FirstClass = GetValue(dayDisciplines, "1"),
                SecondClass = GetValue(dayDisciplines, "2"),
                ThirdClass = GetValue(dayDisciplines, "3"),
                FourthClass = GetValue(dayDisciplines, "4"),
            };
        }

        private static TimeTableDiscipline GetValue(Dictionary<string, TimeTableDiscipline> disciplines, string key)
        {
            disciplines.TryGetValue(key, out TimeTableDiscipline value);

            return value ?? new TimeTableDiscipline
            {
                NickName = "-",
                Name = "AULA VAGA"
            };
        }
    }
}
