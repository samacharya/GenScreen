using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.SourceCode
{
    class Main
    {
        enum Reagent_Type { PH, CHEMICAL, ANION, CATION }

        //Finds the rank of reagent in cocktail
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
            else if (rt == Reagent_Type.CHEMICAL)
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
            else if (rt == Reagent_Type.ANION && reagent != "")
            {
                #region CalculateRankOfAnion
                var avgRankOfAnion = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                      where ((tuple.Field<string>("C1_Anion") == reagent) ||
                                      (tuple.Field<string>("C2_Anion") == reagent) ||
                                      (tuple.Field<string>("C3_Anion") == reagent)
                                      ||
                                      (tuple.Field<string>("C4_Anion") == reagent) ||
                                      (tuple.Field<string>("C5_Anion") == reagent)
                                      )
                                      select (new
                                      {
                                          avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                      })).Average(i => i.avgScore);
                if (reagent != " ")
                {

                    var avgRankOfNotAnion = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                             where ((tuple.Field<string>("C1_Anion") != reagent) ||
                                             (tuple.Field<string>("C2_Anion") != reagent) ||
                                             (tuple.Field<string>("C3_Anion") != reagent)
                                             ||
                                             (tuple.Field<string>("C4_Anion") != reagent) ||
                                             (tuple.Field<string>("C5_Anion") != reagent)
                                             )
                                             select (new
                                             {
                                                 avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                             })).Average(i => i.avgScore);

                    return ((double)avgRankOfAnion) / ((double)avgRankOfNotAnion);
                }
                else
                    return ((double)avgRankOfAnion) / getAvgScore();
                #endregion
            }
            else
            {
                #region CalculateRankOfCation
                var avgRankOfCation = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                       where ((tuple.Field<string>("C1_Cation") == reagent) ||
                                       (tuple.Field<string>("C2_Cation") == reagent) ||
                                       (tuple.Field<string>("C3_Cation") == reagent)
                                       ||
                                       (tuple.Field<string>("C4_Cation") == reagent) ||
                                       (tuple.Field<string>("C5_Cation") == reagent)
                                       )
                                       select (new
                                       {
                                           avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                       })).Average(i => i.avgScore);
                if (reagent != " ")
                {
                    var avgRankOfNotCation = (from tuple in Helper.inputScreenFile.Tables[0].AsEnumerable()
                                              where ((tuple.Field<string>("C1_Cation") != reagent) ||
                                              (tuple.Field<string>("C2_Cation") != reagent) ||
                                              (tuple.Field<string>("C3_Cation") != reagent)
                                              ||
                                              (tuple.Field<string>("C4_Cation") != reagent) ||
                                              (tuple.Field<string>("C5_Cation") != reagent)
                                              )
                                              select (new
                                              {
                                                  avgScore = (tuple.Field<double>("S_a") + tuple.Field<double>("S_b") + tuple.Field<double>("S_c")) / 3,
                                              })).Average(i => i.avgScore);

                    return ((double)avgRankOfCation) / ((double)avgRankOfNotCation);
                }
                else
                    return ((double)avgRankOfCation) / getAvgScore();
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

        public Population createPopulation(DataTable dt)
        {
            Population population = new Population();

            var distinctTuples = (from tuple in dt.AsEnumerable()
                                  select (new
                                  {
                                      pH = tuple.Field<string>("Ph"),
                                      precipitant = tuple.Field<string>("C1_Anion") + " " + tuple.Field<string>("C1_Cation"),
                                      salt = tuple.Field<string>("C2_Anion") + " " + tuple.Field<string>("C2_Cation"),
                                      anion = tuple.Field<string>("C2_Anion"),
                                      cation = tuple.Field<string>("C2_Cation"),
                                  })).Distinct().ToList();

            for (int i = 0; i < distinctTuples.Count; i++)
            {
                double rankOfPH = getRankOfReagent(distinctTuples[i].pH, Reagent_Type.PH);
                Reagent pH = new Reagent(distinctTuples[i].pH, "PH", rankOfPH);

                double rankOfPrecipitant = getRankOfReagent(distinctTuples[i].precipitant, Reagent_Type.CHEMICAL);
                Reagent precipitant = new Reagent(distinctTuples[i].precipitant, "PRECIPITANT", rankOfPrecipitant);

                double rankOfCation = getRankOfReagent(distinctTuples[i].cation, Reagent_Type.CATION);
                Reagent cation = new Reagent(distinctTuples[i].cation, "CATION", rankOfCation);

                double rankOfAnion = getRankOfReagent(distinctTuples[i].anion, Reagent_Type.ANION);
                Reagent anion = new Reagent(distinctTuples[i].anion, "ANION", rankOfAnion);

                Cocktail cocktail = new Cocktail();
                cocktail.setGene(0, pH);
                cocktail.setGene(1, precipitant);
                cocktail.setGene(2, cation);
                cocktail.setGene(3, anion);

                population.saveCocktail(i, cocktail);
            }
            return population;
        }

    }
}
