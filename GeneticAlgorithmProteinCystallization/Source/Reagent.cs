using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.SourceCode
{
    class Reagent
    {
        private string name;
        private string type;
        private double score;

        public Reagent(string name, string type, double score)
        {
            this.name = name;
            this.type = type;
            this.score = score;
        }

        public double getScore()
        {
            return this.score;
        }

        public void setScore(double score)
        {
            this.score = score;
        }

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getType()
        {
            return this.type;
        }

        public void setType(string type)
        {
            this.type = type;
        }
    }
}
