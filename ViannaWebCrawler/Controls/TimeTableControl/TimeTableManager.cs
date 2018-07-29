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

            //Pega a linha que contem os dias da semana
            var nodeDays = document.DocumentNode.SelectNodes("//th[@style='width: 18%;']");
            var days = nodeDays.Select(n => n.InnerText).ToList();

            var classDays = new List<ClassDay>();

            //Pega a coluna com a ordem dos horarios
            var schedulesNodes = document.DocumentNode.SelectNodes($"//tbody/tr[td]/*[1]");
            var schedules = schedulesNodes.Select(o => o.InnerText.Replace("&ordm;", String.Empty)).ToList();

            for (int i = 2, j = 0; i < nodeDays.Count + 2; i++, j++)
            {
                var nodes = document.DocumentNode.SelectNodes($"//tbody/tr[td]/*[{i}]/abbr");

                classDays.Add(ParseToClassDay(nodes, days[j], schedules));
            }

            return classDays;
        }

        private static ClassDay ParseToClassDay(HtmlNodeCollection nodes, string day, List<string> schedules)
        {
            //Abreviacao do nome da siciplina.
            var nickNames = nodes.Select(x => x.InnerText.Trim()).ToList();

            //Embora no site esteja a abeviacao do nome, optei por preencher os objetos com o nome completo
            var names = nodes.Select(a => a.GetAttributeValue("title", "")).ToList();

            //Remove um codigo estranho que fica na frente do nome, ex: #1047 - nome
            Regex reg = new Regex("^[^a-zA-Z]+");
            names = names.Select(s => reg.Replace(s, String.Empty)).ToList();

            //Dicionario tem a funcao de garantir que o horario estara na mesma posicado do site
            var dayDictionary = new Dictionary<string, string>();

            for (int i = 0; i < schedules.Count(); i++)
                dayDictionary.Add(schedules[i], names[i]);

            return new ClassDay()
            {
                DayOfWeek = day,
                FirstClass = dayDictionary["1"],
                SecondClass = dayDictionary["2"],
                ThirdClass = dayDictionary["3"],
                FourthClass = dayDictionary["4"],
            };
        }
    }
}
