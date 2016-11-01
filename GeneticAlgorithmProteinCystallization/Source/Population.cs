using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.SourceCode
{
    class Population
    {
        Cocktail[] cocktails;

        public Population()
        {

        }

        public Population(int size)
        {
            cocktails = new Cocktail[size];
        }


        public void saveCocktail(int index, Cocktail cocktail)
        {
            cocktails[index] = cocktail;
        }

        public Cocktail getCocktail(int index)
        {
            return cocktails[index];
        }

        public Cocktail getFittest()
        {
            Cocktail fittest = cocktails[0];
            for (int i = 1; i < this.populationSize(); i++)
            {
                if (fittest.getFitness() < cocktails[i].getFitness())
                {
                    fittest = cocktails[i];
                }
            }
            return fittest;
        }

        public int populationSize()
        {
            return cocktails.Length;
        }
    }
}
