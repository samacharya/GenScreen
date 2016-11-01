using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.SourceCode
{
    class Cocktail
    {
        static int defaultGeneLenth = 3;
        //static int defaultGeneLenth = 4;

        private Reagent[] candidate = new Reagent[defaultGeneLenth];
        private double fitness = 0;

        // Get the reagent at a specified index in the cocktail
        public Reagent getGene(int index)
        {
            return (Reagent)candidate[index];
        }

        // Sets a reagent at given index in the cocktail
        public void setGene(int index, Reagent reagent)
        {
            candidate[index] = reagent;
        }

        // Returns size of the cocktail
        public int size()
        {
            return candidate.Length;
        }

        // Gets fitness score of the cocktail
        public double getFitness()
        {
            double totalScore = 0;
            for (int i = 0; i < defaultGeneLenth; i++)
            {
                totalScore += candidate[i].getScore();
            }
            return (totalScore / defaultGeneLenth);
        }

        //public double getFitness()
        //{
        //    double totalScore = 0;
        //    totalScore = (candidate[0].getScore() + candidate[1].getScore() + (candidate[2].getScore() + candidate[3].getScore()) / 2) / 3;
        //    return totalScore;
        //}
    }
}
