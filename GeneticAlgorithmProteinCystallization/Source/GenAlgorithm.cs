using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.SourceCode
{
    class GenAlgorithm
    {
        private const int numberOfDistinctBuffer = 1;
        private static readonly Random rnd = new Random();
        private Population oldPopulation = null;
        public double mutationRate = 0.5;
        public int tournamentSize = 2;
        public bool elitism = false;
        public int numIter = 400;
        public int newPopSize = 200;        

        public static Dictionary<string, double> scoresOfPreps = new Dictionary<string, double>();
        public static Dictionary<string, double> scoresOfSalts = new Dictionary<string, double>();
        public static Dictionary<string, double> scoresOfPhs = new Dictionary<string, double>();

        enum Reagent_Type { PH, CHEMICAL, ANION, CATION }

        //Finds the rank of reagent in cocktail
        //private double getRankOfReagent(String reagent, Reagent_Type rt)
        //{
        //    if (rt == Reagent_Type.PH)
        //    {
        //        #region CalculateRankOfPh
        //        var avgRankOfPh = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                           where (tuple.Field<string>("Ph") == reagent)
        //                           select (new
        //                           {
        //                               avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                           })).Average(i => i.avgScore);


        //        var avgRankOfNotPh = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                              where (tuple.Field<string>("Ph") != reagent)
        //                              select (new
        //                              {
        //                                  avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                              })).Average(i => i.avgScore);

        //        return ((double)avgRankOfPh) / ((double)avgRankOfNotPh);

        //        #endregion
        //    }
        //    else if (rt == Reagent_Type.CHEMICAL)
        //    {
        //        #region CalculateRankOfChemical
        //        var avgRankOfChem = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                             where ((tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation") == reagent) ||
        //                             (tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation") == reagent) ||
        //                             (tuple.Field<string>("C3_Anion") + " " + tuple.Field<string>("C3_Cation") == reagent) ||
        //                             (tuple.Field<string>("C4_Anion") + " " + tuple.Field<string>("C4_Cation") == reagent) ||
        //                             (tuple.Field<string>("C5_Anion") + " " + tuple.Field<string>("C5_Cation") == reagent))
        //                             select (new
        //                             {
        //                                 avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                             })).Average(i => i.avgScore);
        //        if (reagent != " ")
        //        {

        //            var avgRankOfNotChem = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                                    where ((tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation") != reagent) ||
        //                                    (tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation") != reagent) ||
        //                                    (tuple.Field<string>("C3_Anion") + " " + tuple.Field<string>("C3_Cation") != reagent) ||
        //                                    (tuple.Field<string>("C4_Anion") + " " + tuple.Field<string>("C4_Cation") != reagent) ||
        //                                    (tuple.Field<string>("C5_Anion") + " " + tuple.Field<string>("C5_Cation") != reagent))
        //                                    select (new
        //                                    {
        //                                        avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                                    })).Average(i => i.avgScore);

        //            return ((double)avgRankOfChem) / ((double)avgRankOfNotChem);
        //        }
        //        else
        //            return ((double)avgRankOfChem) / getAvgScore();
        //        #endregion

        //    }
        //    else if (rt == Reagent_Type.ANION)
        //    {
        //        #region CalculateRankOfAnion
        //        var avgRankOfAnion = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                              where ((tuple.Field<string>("C1_Anion") == reagent) ||
        //                              (tuple.Field<string>("C2_Anion") == reagent) ||
        //                              (tuple.Field<string>("C3_Anion") == reagent)
        //                              ||
        //                              (tuple.Field<string>("C4_Anion") == reagent) ||
        //                              (tuple.Field<string>("C5_Anion") == reagent)
        //                              )
        //                              select (new
        //                              {
        //                                  avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                              })).Average(i => i.avgScore);
        //        if (reagent != "")
        //        {

        //            var avgRankOfNotAnion = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                                     where ((tuple.Field<string>("C1_Anion") != reagent) ||
        //                                     (tuple.Field<string>("C2_Anion") != reagent) ||
        //                                     (tuple.Field<string>("C3_Anion") != reagent)
        //                                     ||
        //                                     (tuple.Field<string>("C4_Anion") != reagent) ||
        //                                     (tuple.Field<string>("C5_Anion") != reagent)
        //                                     )
        //                                     select (new
        //                                     {
        //                                         avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                                     })).Average(i => i.avgScore);

        //            return ((double)avgRankOfAnion) / ((double)avgRankOfNotAnion);
        //        }
        //        else
        //            return ((double)avgRankOfAnion) / getAvgScore();
        //        #endregion
        //    }
        //    else //if (rt == Reagent_Type.CATION)
        //    {
        //        #region CalculateRankOfCation
        //        var avgRankOfCation = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                               where ((tuple.Field<string>("C1_Cation") == reagent) ||
        //                               (tuple.Field<string>("C2_Cation") == reagent) ||
        //                               (tuple.Field<string>("C3_Cation") == reagent)
        //                               ||
        //                               (tuple.Field<string>("C4_Cation") == reagent) ||
        //                               (tuple.Field<string>("C5_Cation") == reagent)
        //                               )
        //                               select (new
        //                               {
        //                                   avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                               })).Average(i => i.avgScore);
        //        if (reagent != "")
        //        {
        //            var avgRankOfNotCation = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
        //                                      where ((tuple.Field<string>("C1_Cation") != reagent) ||
        //                                      (tuple.Field<string>("C2_Cation") != reagent) ||
        //                                      (tuple.Field<string>("C3_Cation") != reagent)
        //                                      ||
        //                                      (tuple.Field<string>("C4_Cation") != reagent) ||
        //                                      (tuple.Field<string>("C5_Cation") != reagent)
        //                                      )
        //                                      select (new
        //                                      {
        //                                          avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
        //                                      })).Average(i => i.avgScore);

        //            return ((double)avgRankOfCation) / ((double)avgRankOfNotCation);
        //        }
        //        else
        //            return ((double)avgRankOfCation) / getAvgScore();
        //        #endregion
        //    }

        //    //else return 0;

        //}

        private double getRankOfReagent(String reagent, Reagent_Type rt)
        {
            if (rt == Reagent_Type.PH)
            {
                #region CalculateRankOfPh
                var avgRankOfPh = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                   where (tuple.Field<string>("Ph") == reagent)
                                   select (new
                                   {
                                       avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                   })).Average(i => i.avgScore);


                var avgRankOfNotPh = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                      where (tuple.Field<string>("Ph") != reagent)
                                      select (new
                                      {
                                          avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                      })).Average(i => i.avgScore);

                return ((double)avgRankOfPh) / ((double)avgRankOfNotPh);

                #endregion
            }
            else 
            {
                #region CalculateRankOfChemical
                var avgRankOfChem = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                     where ((tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation") == reagent) ||
                                     (tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation") == reagent) ||
                                     (tuple.Field<string>("C3_Anion") + " " + tuple.Field<string>("C3_Cation") == reagent) ||
                                     (tuple.Field<string>("C4_Anion") + " " + tuple.Field<string>("C4_Cation") == reagent) ||
                                     (tuple.Field<string>("C5_Anion") + " " + tuple.Field<string>("C5_Cation") == reagent))
                                     select (new
                                     {
                                         avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                     })).Average(i => i.avgScore);
                if (reagent != " ")
                {

                    var avgRankOfNotChem = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                            where ((tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation") != reagent) ||
                                            (tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation") != reagent) ||
                                            (tuple.Field<string>("C3_Anion") + " " + tuple.Field<string>("C3_Cation") != reagent) ||
                                            (tuple.Field<string>("C4_Anion") + " " + tuple.Field<string>("C4_Cation") != reagent) ||
                                            (tuple.Field<string>("C5_Anion") + " " + tuple.Field<string>("C5_Cation") != reagent))
                                            select (new
                                            {
                                                avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                            })).Average(i => i.avgScore);

                    return ((double)avgRankOfChem) / ((double)avgRankOfNotChem);
                }
                else
                    return ((double)avgRankOfChem) / getAvgScore();
                #endregion
            }
        }

        //Calculates average score for the input file
        private double getAvgScore()
        {
            var avgScore = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                            select (new
                            {
                                avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                            })).Average(i => i.avgScore);
            return (double)avgScore;
        }

        //public Population getPopulationFromFile(DataTable dt)
        //{
        //    var distinctTuples = (from tuple in dt.AsEnumerable()
        //                          select (new
        //                          {
        //                              pH = tuple.Field<string>("Ph"),
        //                              precipitant = tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation"),
        //                              //salt = tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation"),
        //                              anion = tuple.Field<string>("C2_Anion"),
        //                              cation = tuple.Field<string>("C2_Cation"),
        //                          })).Distinct().ToList();

        //    oldPopulation = new Population(distinctTuples.Count);
        //    for (int i = 0; i < distinctTuples.Count; i++)
        //    {
        //        double rankOfPH = getRankOfReagent(distinctTuples[i].pH, Reagent_Type.PH);
        //        Reagent pH = new Reagent(distinctTuples[i].pH, "PH", rankOfPH);

        //        double rankOfPrecipitant = getRankOfReagent(distinctTuples[i].precipitant, Reagent_Type.CHEMICAL);
        //        Reagent precipitant = new Reagent(distinctTuples[i].precipitant, "PRECIPITANT", rankOfPrecipitant);

        //        double rankOfCation = getRankOfReagent(distinctTuples[i].cation, Reagent_Type.CATION);
        //        Reagent cation = new Reagent(distinctTuples[i].cation, "CATION", rankOfCation);

        //        double rankOfAnion = getRankOfReagent(distinctTuples[i].anion, Reagent_Type.ANION);
        //        Reagent anion = new Reagent(distinctTuples[i].anion, "ANION", rankOfAnion);

        //        Cocktail cocktail = new Cocktail();
        //        cocktail.setGene(0, pH);
        //        cocktail.setGene(1, precipitant);
        //        cocktail.setGene(2, cation);
        //        cocktail.setGene(3, anion);

        //        oldPopulation.saveCocktail(i, cocktail);
        //    }
        //    return oldPopulation;
        //}

        public Population getPopulationFromFile(DataTable dt)
        {
            var distinctTuples = (from tuple in dt.AsEnumerable()
                                  select (new
                                  {
                                      pH = tuple.Field<string>("Ph"),
                                      precipitant = tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation"),
                                      salt = tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation"),
                                  })).Distinct().ToList();

            oldPopulation = new Population(distinctTuples.Count);
            for (int i = 0; i < distinctTuples.Count; i++)
            {
                double rankOfPH = getRankOfReagent(distinctTuples[i].pH, Reagent_Type.PH);
                Reagent pH = new Reagent(distinctTuples[i].pH, "PH", rankOfPH);

                double rankOfPrecipitant = getRankOfReagent(distinctTuples[i].precipitant, Reagent_Type.CHEMICAL);
                Reagent precipitant = new Reagent(distinctTuples[i].precipitant, "PRECIPITANT", rankOfPrecipitant);

                double rankOfSalt = getRankOfReagent(distinctTuples[i].salt, Reagent_Type.CHEMICAL);
                Reagent salt = new Reagent(distinctTuples[i].salt, "SALT", rankOfSalt);

                Cocktail cocktail = new Cocktail();
                cocktail.setGene(0, pH);
                cocktail.setGene(1, precipitant);
                cocktail.setGene(2, salt);

                oldPopulation.saveCocktail(i, cocktail);
            }
            return oldPopulation;
        }

        // Evolves a population 
        public Population evolvePopulation(Population pop, int iterations)
        {
            Population newPopulation = new Population(newPopSize);

            int elitismOffset = 0;
            if (elitism)
            {
                newPopulation.saveCocktail(0, pop.getFittest());
                elitismOffset = 1;
            }

            for (int i = elitismOffset; i < newPopulation.populationSize(); i++)
            {
                Cocktail parent1 = tournamentSelection(pop);
                Cocktail parent2 = tournamentSelection(pop);

                Cocktail child = crossover(parent1, parent2);
                newPopulation.saveCocktail(i, child);
            }

            if (iterations < numIter - 1)
            {
                for (int i = elitismOffset; i < newPopulation.populationSize(); i++)
                {
                    mutate(newPopulation.getCocktail(i), pop);
                }
            }
            return newPopulation;
        }

        //Evolves a population
        //public Population evolvePopulation(Population pop, int iterations)
        //{
        //    Population newPopulation = new Population(newPopSize);

        //    int elitismOffset = 0;
        //    if (elitism)
        //    {
        //        newPopulation.saveCocktail(0, pop.getFittest());
        //        elitismOffset = 1;
        //    }

        //    for (int i = elitismOffset; i < newPopSize; i++)
        //    {
        //        Cocktail parent1 = tournamentSelection(pop);
        //        Cocktail parent2 = tournamentSelection(pop);

        //        Cocktail child = crossover(parent1, parent2);
        //        newPopulation.saveCocktail(i, child);
        //    }

        //    if (iterations < numIter - 1)
        //    {
        //        for (int i = elitismOffset; i < newPopSize; i++)
        //        {
        //            mutate(newPopulation.getCocktail(i), pop);
        //        }
        //    }

        //    checkEmptyAnion(newPopulation);
        //    return newPopulation;
        //}

        // Apply crossover using single point crossover from two parent cocktails
        //public Cocktail crossover(Cocktail parent1, Cocktail parent2)
        //{
        //    Cocktail child = new Cocktail();
        //    //Random rnd = new Random();

        //    //Crossover point is the dividing point between two parents
        //    int crossOverPoint = rnd.Next(0, parent1.size());

        //    for (int i = 0; i < parent1.size(); i++)
        //    {
        //        if (i <= crossOverPoint)
        //        {
        //            child.setGene(i, parent1.getGene(i));
        //        }
        //        else if (i > crossOverPoint)
        //        {
        //            child.setGene(i, parent2.getGene(i));
        //        }
        //    }
        //    if ((child.getGene(2).getName() == "" && child.getGene(3).getName() != "") || (child.getGene(3).getName() == "" && child.getGene(2).getName() != ""))
        //    {
        //        int r = rnd.Next(0, 2);
        //        if (r < 1)
        //        {
        //            child.setGene(2, parent1.getGene(2));
        //            child.setGene(3, parent1.getGene(3));
        //        }
        //        else
        //        {
        //            child.setGene(2, parent2.getGene(2));
        //            child.setGene(3, parent2.getGene(3));
        //        }
        //    }
        //    return child;
        //}

        //public Cocktail crossover(Cocktail parent1, Cocktail parent2)
        //{
        //    Cocktail child = new Cocktail();
        //    //Random rnd = new Random();

        //    //Crossover point is the dividing point between two parents
        //    int crossOverPoint = rnd.Next(0, parent1.size());

        //    for (int i = 0; i < parent1.size(); i++)
        //    {
        //        if (i <= crossOverPoint)
        //        {
        //            child.setGene(i, parent1.getGene(i));
        //        }
        //        else if (i > crossOverPoint)
        //        {
        //            child.setGene(i, parent2.getGene(i));
        //        }
        //    }
        //    if ((child.getGene(2).getName() == "" && child.getGene(3).getName() != "") || (child.getGene(3).getName() == "" && child.getGene(2).getName() != ""))
        //    {
        //        int r = rnd.Next(0, 2);
        //        if (r < 1)
        //        {
        //            child.setGene(2, parent1.getGene(2));
        //            child.setGene(3, parent1.getGene(3));
        //        }
        //        else
        //        {
        //            child.setGene(2, parent2.getGene(2));
        //            child.setGene(3, parent2.getGene(3));
        //        }
        //    }
        //    return child;
        //}

        public Cocktail crossover(Cocktail parent1, Cocktail parent2)
        {
            Cocktail child = new Cocktail();
            //Random rnd = new Random();

            //Crossover point is the dividing point between two parents
            int crossOverPoint = rnd.Next(0, parent1.size());

            for (int i = 0; i < parent1.size(); i++)
            {
                if (i <= crossOverPoint)
                {
                    child.setGene(i, parent1.getGene(i));
                }
                else if (i > crossOverPoint)
                {
                    child.setGene(i, parent2.getGene(i));
                }
            }
            return child;
        }

        // Mutate a cocktail using random genes from a random cocktail in the population
        //private void mutate(Cocktail cocktail, Population pop)
        //{
        //    for (int i = 0; i < cocktail.size(); i++)
        //    {
        //        // Generate random number to check for mutation
        //        //Random rnd = new Random();
        //        double randomId = rnd.NextDouble();

        //        if (randomId < mutationRate)
        //        {
        //            // Get a random cocktail from old population
        //            int randomOldCocktailID = rnd.Next(0, oldPopulation.populationSize());
        //            Cocktail rndCocktail = oldPopulation.getCocktail(randomOldCocktailID);

        //            // Get a random gene for mutation from the cocktail
        //            int rndGenetId = rnd.Next(0, 2);
        //            Reagent mutantGene = rndCocktail.getGene(rndGenetId);

        //            // Set the mutantion gene in the original cocktail
        //            if (mutantGene.getName() != "")
        //            {
        //                cocktail.setGene(rndGenetId, mutantGene);
        //            }
        //        }
        //    }
        //}

        private void mutate(Cocktail cocktail, Population pop)
        {
            for (int i = 0; i < cocktail.size(); i++)
            {
                // Generate random number to check for mutation
                //Random rnd = new Random();
                double randomId = rnd.NextDouble();

                if (randomId < mutationRate)
                {
                    // Get a random cocktail from old population
                    int randomOldCocktailID = rnd.Next(0, oldPopulation.populationSize());
                    Cocktail rndCocktail = oldPopulation.getCocktail(randomOldCocktailID);

                    // Get a random gene for mutation from the cocktail
                    int rndGenetId = rnd.Next(0, 3);
                    Reagent mutantGene = rndCocktail.getGene(rndGenetId);

                    // Set the mutantion gene in the original cocktail
                    //if (mutantGene.getName() != "")
                    //{
                    cocktail.setGene(rndGenetId, mutantGene);
                    //}
                }
            }
        }

        //private void checkEmptyAnion(Population pop)
        //{
        //    Cocktail cocktail;
        //    for(int i = 0; i < pop.populationSize(); i++)
        //    {
        //        cocktail = pop.getCocktail(i);

        //        //if (cocktail.getGene(2).getName() != "" && cocktail.getGene(3).getName() == "")
        //        //    {
        //        //        int ran = rnd.Next(0, oldPopulation.populationSize());
        //        //        cocktail = setIon(cocktail, 3);
        //        //        pop.getCocktail(i).setGene(3, cocktail.getGene(3));
        //        //        System.Console.WriteLine("/nAnion set!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //        //    }

        //        //    if (cocktail.getGene(3).getName() != "" && cocktail.getGene(2).getName() == "")
        //        //    {
        //        //        int ran = rnd.Next(0, oldPopulation.populationSize());
        //        //        cocktail = setIon(cocktail, 2);
        //        //        pop.getCocktail(i).setGene(2, cocktail.getGene(2));
        //        //        System.Console.WriteLine("/nAnion set!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //        //    }

        //            if (isNoAnionCocktail(cocktail))
        //            {
        //                pop.getCocktail(i).getGene(3).setName("");
        //                pop.getCocktail(i).getGene(3).setScore(cocktail.getGene(2).getScore());
        //                System.Console.WriteLine("/nAnion set!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //            }
        //    //    }
        //    }
        //}

        //private void setAnion(Cocktail cocktail, int r)
        //{
        //    if (oldPopulation.getCocktail(r).getGene(3).getName() != "")
        //    {
        //        cocktail.setGene(3, oldPopulation.getCocktail(r).getGene(3));
        //        return;
        //    }
        //    else
        //    {
        //        int ran = rnd.Next(0, oldPopulation.populationSize());
        //        setAnion(cocktail, ran);
        //    }
        //}
        //private Cocktail setIon(Cocktail cocktail, int i)
        //{
        //    //while(true)
        //    //{
        //    //    int rand = rnd.Next(0, oldPopulation.populationSize());
        //    //    if (oldPopulation.getCocktail(rand).getGene(i).getName() != "")
        //    //    {
        //    //        cocktail.setGene(i, oldPopulation.getCocktail(rand).getGene(i));
        //    //        return cocktail;
        //    //    }
        //    //}
        //    int rand = rnd.Next(0, Helper.cations.Count);
        //    if(i == 2)
        //    {
        //        cocktail.setGene(2, Helper.cations[2]);
        //    }
        //}

        // Select candidate cocktail from a random population for Crossover
        private Cocktail tournamentSelection(Population pop)
        {
            Population tournament = new Population(tournamentSize);

            for (int i = 0; i < tournamentSize; i++)
            {
                //Random rnd = new Random();
                int randomId = rnd.Next(0, pop.populationSize());
                tournament.saveCocktail(i, pop.getCocktail(randomId));
            }

            // Get the best cocktail
            Cocktail fittest = tournament.getFittest();
            return fittest;
        }

        //private DataTable getDatatableFromPopulation(Population pop)
        //{
        //    DataTable table = new DataTable("candidates");
        //    table.Columns.Add(new DataColumn("pH", typeof(String)));
        //    table.Columns.Add(new DataColumn("Precipitant", typeof(String)));
        //    table.Columns.Add(new DataColumn("Cation", typeof(String)));
        //    table.Columns.Add(new DataColumn("Anion", typeof(String)));

        //    for (int i = 0; i < pop.populationSize(); i++)
        //    {
        //        table.Rows.Add(pop.getCocktail(i).getGene(0).getName(),
        //            pop.getCocktail(i).getGene(1).getName(),
        //            pop.getCocktail(i).getGene(2).getName(),
        //            pop.getCocktail(i).getGene(3).getName());
        //    }
        //    DataView view = new DataView(table);
        //    DataTable distinctValues = view.ToTable(true);
        //    return distinctValues;
        //}

        private DataTable getDatatableFromPopulation(Population pop)
        {
            DataTable table = new DataTable("candidates");
            table.Columns.Add(new DataColumn("pH", typeof(String)));
            table.Columns.Add(new DataColumn("Precipitant", typeof(String)));
            table.Columns.Add(new DataColumn("Salt", typeof(String)));

            for (int i = 0; i < pop.populationSize(); i++)
            {
                table.Rows.Add(pop.getCocktail(i).getGene(0).getName(),
                    pop.getCocktail(i).getGene(1).getName(),
                    pop.getCocktail(i).getGene(2).getName());
            }
            DataView view = new DataView(table);
            DataTable distinctValues = view.ToTable(true);
            return distinctValues;
        }

        private String[] GetDistinctBuffers(DataTable dt, String Ph)
        {
            var distinctBufferTypes = (from tuple in dt.AsEnumerable()
                                       where tuple.Field<string>("Ph") == Ph
                                       select (new
                                       {
                                           buffer = tuple.Field<string>("B_Anion") + " " + tuple.Field<string>("B_Cation"),
                                       })).Distinct().ToList();
            String[] buffers = new String[distinctBufferTypes.Count];
            for (int i = 0; i < distinctBufferTypes.Count; i++)
            {
                buffers[i] = distinctBufferTypes[i].buffer;
            }

            return buffers;
        }

        private String[] GetDistinctPrepCon(DataTable dt, String typeOfPrep)
        {

            var distinctPrespConc = (from tuple in dt.AsEnumerable()
                                     where (tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation")) == typeOfPrep
                                     select (new
                                     {
                                         prep = tuple.Field<string>("C1_Conc") + tuple.Field<string>("C1_M"),
                                     })).Distinct().ToList();

            String[] preps = new String[distinctPrespConc.Count];
            for (int i = 0; i < distinctPrespConc.Count; i++)
            {
                preps[i] = distinctPrespConc[i].prep;
            }
            return preps;
        }

        private String[] GetDistinctSaltCon(DataTable dt, String typeOfSalt)
        {
            var distinctSaltConc = (from tuple in dt.AsEnumerable()
                                    where (tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation")) == typeOfSalt
                                    select (new
                                    {
                                        saltCon = tuple.Field<string>("C2_Conc") + tuple.Field<string>("C2_M"),
                                    })).Distinct().ToList();


            String[] salts = new String[distinctSaltConc.Count];
            for (int i = 0; i < distinctSaltConc.Count; i++)
            {
                salts[i] = distinctSaltConc[i].saltCon;
            }
            return salts;
        }

        //private String[] GetDistinctSaltCon(DataTable dt, String typeOfSalt)
        //{
        //    string[] s = typeOfSalt.Split(' ');

        //    var distinctSaltConc = (from tuple in dt.AsEnumerable()
        //                            where (tuple.Field<string>("C2_Cation")) == s[0]
        //                            select (new
        //                            {
        //                                saltCon = tuple.Field<string>("C2_Conc") + tuple.Field<string>("C2_M"),
        //                            })).Distinct().ToList();


        //    String[] salts = new String[distinctSaltConc.Count];
        //    for (int i = 0; i < distinctSaltConc.Count; i++)
        //    {
        //        salts[i] = distinctSaltConc[i].saltCon;
        //    }
        //    return salts;
        //}

        private DataTable generateConcentrations(DataTable dtAllCocktails, DataTable candidateTriples)
        {
            DataTable table = new DataTable("Candidates");
            table.Columns.Add(new DataColumn("Ph", typeof(String)));
            table.Columns.Add(new DataColumn("Buffer", typeof(String)));
            table.Columns.Add(new DataColumn("B_Conc", typeof(String)));
            table.Columns.Add(new DataColumn("Precipitant", typeof(String)));
            table.Columns.Add(new DataColumn("P_Conc", typeof(String)));
            table.Columns.Add(new DataColumn("Salt", typeof(String)));
            table.Columns.Add(new DataColumn("S_Conc", typeof(String)));
            table.Columns.Add(new DataColumn("Rank", typeof(double)));


            for (int i = 0; i < candidateTriples.Rows.Count; i++)
            {
                String[] bufferTypes = GetDistinctBuffers(dtAllCocktails, candidateTriples.Rows[i].ItemArray[0].ToString());
                String[] distinctPrepConc = GetDistinctPrepCon(dtAllCocktails, candidateTriples.Rows[i].ItemArray[1].ToString());
                String[] distinctSaltConc = GetDistinctSaltCon(dtAllCocktails, candidateTriples.Rows[i].ItemArray[2].ToString());

                // distinct buffers has been canceled here. Just change the outer loop 
                // to generate different experiment for each buffer
                //numberofdistinctbuffer=bufferTypes.Length

                for (int j = 0; j < numberOfDistinctBuffer; j++)
                {
                    for (int m = 0; m < distinctPrepConc.Length; m++)
                    {
                        for (int n = 0; n < distinctSaltConc.Length; n++)
                        {
                            table.Rows.Add(candidateTriples.Rows[i].ItemArray[0].ToString(), bufferTypes[j], "0.1",
                               candidateTriples.Rows[i].ItemArray[1].ToString(), distinctPrepConc[m],
                               candidateTriples.Rows[i].ItemArray[2].ToString(), distinctSaltConc[n]);
                        }
                    }
                }
            }
            return table;
        }

        private DataTable checkUniqeness(DataTable dt)
        {
            var currentConditions = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                     select (new
                                     {
                                         pH = tuple.Field<string>("Ph"),
                                         precipitant = tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation"),
                                         prepConc = tuple.Field<string>("C1_Conc") + tuple.Field<string>("C1_M"),
                                         salt = tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation"),
                                         saltConc = tuple.Field<string>("C2_Conc") + tuple.Field<string>("C2_M"),
                                     })).Distinct().ToList();

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                bool exist = false;

                // if prepicipitant and salt are equal, it a bad screen, delete it
                if (dt.Rows[i].ItemArray[3].ToString() == dt.Rows[i].ItemArray[5].ToString())
                {
                    dt.Rows[i].Delete();
                    break;
                }

                for (int j = 0; j < currentConditions.Count; j++)
                {
                    if (currentConditions[j].pH == dt.Rows[i].ItemArray[0].ToString() && currentConditions[j].precipitant == dt.Rows[i].ItemArray[3].ToString()
                        && currentConditions[j].prepConc == dt.Rows[i].ItemArray[4].ToString() && currentConditions[j].salt == dt.Rows[i].ItemArray[5].ToString()
                        && currentConditions[j].saltConc == dt.Rows[i].ItemArray[6].ToString())
                    {
                        exist = true;
                        break;
                    }
                }

                if (exist)
                    dt.Rows[i].Delete();
            }
            return dt;
        }

        private bool isNoAnionCocktail(Cocktail c)
        {
            String cation = c.getGene(2).getName();
            //foreach(object obj in Helper.noAnions)
            //{
            //    result = reagent.Equals(obj.ToString(), StringComparison.OrdinalIgnoreCase);
            //}

            return Helper.noAnions.Contains(cation);
        }

        private bool CheckBadCombinations(object[] chemicalList)
        {
            string chemical = null;
            int counter = 0;
            bool badCom1 = false, badCom2 = false;
            for (int i = 0; i < chemicalList.Length; i++)
            {
                chemical = chemicalList[i].ToString();
                while (counter < Math.Max(Helper.badCom1.Count, Helper.badCom2.Count))
                {
                    if (counter < Helper.badCom1.Count)
                    {
                        if (chemical.IndexOf(Helper.badCom1[counter]) != -1)
                            badCom1 = true;
                    }
                    if (counter < Helper.badCom2.Count)
                    {
                        if (chemical.IndexOf(Helper.badCom2[counter]) != -1)
                            badCom2 = true;
                    }
                    counter++;
                }
                counter = 0;
            }
            if (badCom1 & badCom2) return true;
            else return false;
        }

        private void eliminateBadCombinations(ref DataTable candidateCocktails)
        {
            for (int i = candidateCocktails.Rows.Count - 1; i >= 0; i--)
            {
                bool badComb = CheckBadCombinations(candidateCocktails.Rows[i].ItemArray);
                if (badComb)
                    candidateCocktails.Rows[i].Delete();
            }
        }

        private void UpdateReagentScoreLists(string pH, double rankOfPh, string prep, double rankOfPrecipitant, string salt, double rankOfSalt)
        {
            if (!scoresOfPhs.ContainsKey(pH))
                scoresOfPhs.Add(pH, rankOfPh);
            if (!scoresOfPreps.ContainsKey(prep))
                scoresOfPreps.Add(prep, rankOfPrecipitant);
            if (!scoresOfSalts.ContainsKey(salt))
                scoresOfSalts.Add(salt, rankOfSalt);
        }

        private void applyRanking(ref DataTable dt)
        {
            double rankOfPh = 0.0, rankOfSalt = 0.0, rankOfPrecipitant = 0.0, overallRank = 0.0;
            for (int i = dt.Rows.Count - 1; i > -1; i--)
            {
                DataRow dr = dt.Rows[i];
                rankOfPh = getRankOfReagent(dr["Ph"].ToString(), Reagent_Type.PH);
                rankOfPrecipitant = getRankOfReagent(dr["Precipitant"].ToString(), Reagent_Type.CHEMICAL);
                rankOfSalt = getRankOfReagent(dr["Salt"].ToString(), Reagent_Type.CHEMICAL);
                UpdateReagentScoreLists(dr["Ph"].ToString(), rankOfPh, dr["Precipitant"].ToString(),
                    rankOfPrecipitant, dr["Salt"].ToString(), rankOfSalt);
                overallRank = (rankOfPh + rankOfPrecipitant + rankOfSalt) / 3;

                dr["Rank"] = overallRank;
                if (overallRank < 1)
                    dt.Rows[i].Delete();
            }
        }

        //private void applyRanking(ref DataTable dt)
        //{
        //    double rankOfPh = 0.0, rankOfSalt = 0.0, rankOfPrecipitant = 0.0, overallRank = 0.0;
        //    double rankOfCation = 0.0, rankOfAnion = 0.0;
        //    string[] ions;
        //    for (int i = dt.Rows.Count - 1; i > -1; i--)
        //    {
        //        DataRow dr = dt.Rows[i];
        //        ions = dr["Salt"].ToString().Split(' ');
        //        rankOfPh = getRankOfReagent(dr["Ph"].ToString(), Reagent_Type.PH);
        //        rankOfPrecipitant = getRankOfReagent(dr["Precipitant"].ToString(), Reagent_Type.CHEMICAL);
        //        //rankOfSalt = getRankOfReagent(dr["Salt"].ToString(), Reagent_Type.CHEMICAL);
        //        rankOfCation = getRankOfReagent(ions[0], Reagent_Type.CATION);
        //        rankOfCation = getRankOfReagent(ions[1], Reagent_Type.ANION);


        //        //UpdateReagentScoreLists(dr["Ph"].ToString(), rankOfPh, dr["Precipitant"].ToString(),
        //           // rankOfPrecipitant);
        //        overallRank = (rankOfPh + rankOfPrecipitant + (rankOfCation + rankOfAnion) / 2) / 3;

        //        dr["Rank"] = overallRank;
        //        if (overallRank < 1)
        //            dt.Rows[i].Delete();
        //    }
        //}

        private void sortResult(ref DataTable dt)
        {
            IEnumerable<DataRow> sortedTable = dt.AsEnumerable()
                 .OrderByDescending(r => r.Field<double>("Rank")).ToList();
            dt = sortedTable.CopyToDataTable<DataRow>();
        }


        public DataTable applyGenAlgorithm(DataTable t)
        {
            DataTable candidates;
            Population pop = getPopulationFromFile(t);

            for (int i = 0; i < numIter; i++)
            {
                pop = evolvePopulation(pop, i);
            }
            candidates = getDatatableFromPopulation(pop);
            DataTable candidateCocktails = generateConcentrations(t, candidates);
            candidateCocktails = checkUniqeness(candidateCocktails);

            eliminateBadCombinations(ref candidateCocktails);
            if (candidateCocktails.Rows.Count != 0)
            {
                applyRanking(ref candidateCocktails);
                sortResult(ref candidateCocktails);
            }

            return candidateCocktails;
        }
    }
}
