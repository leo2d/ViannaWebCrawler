using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViannaWebCrawler
{
    public class Discipline
    {
        public string Name { get; set; }
        //Nota do primeiro Bimestre
        public double FirstBimesterGrade { get; set; }
        //Nota do segundo Bimestre
        public double SecondBimesterGrade { get; set; }
        public double Media { get; set; }
        //Nota da recuperacao ou prova exame
        public double RetakeTestGrade { get; set; }
        //Nota final
        public double TotalGrade { get; set; }
        //Numero de faltas
        public int MissedClass { get; set; }
        //Percentual de faltas
        public double MissedClassPercentage { get; set; }

    }
}
