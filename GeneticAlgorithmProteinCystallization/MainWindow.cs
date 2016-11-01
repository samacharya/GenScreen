using GeneticAlgorithm;
using GeneticAlgorithm.SourceCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithmProteinCystallization
{
    public partial class MainWindow : Form
    {
        DataTable candidates = new DataTable();
        private string inputFile;
        private string outputFile;
        private string inputFileAED;
        private int lowScore = 3, highScore = 10;
        private GenAlgorithm ga;

        private static double MUTATION_RATE = 0.5;
        private static int TOURNAMENT_SIZE = 2;
        private static bool ELITISM = false;
        private static int GENERATIONS = 400;
        private static int NEW_POP_SIZE = 200;

        public MainWindow()
        {
            InitializeComponent();
            ga = new GenAlgorithm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mutationRateText.Text = MUTATION_RATE.ToString();
            tournamentSizeText.Text = TOURNAMENT_SIZE.ToString();
            generationText.Text = GENERATIONS.ToString();
            popSizeText.Text = NEW_POP_SIZE.ToString();
        }

        private void inputFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = false;
            fd.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|Excel 95 files (*.xls)|*.xls";
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            fd.ShowDialog();      
            inputFile = fd.FileName;
            inputFileText.Text = inputFile;
        }

        private void outputFileBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.FileName = "GA_Output";
            fd.DefaultExt = ".xlsx";
            fd.Filter = "Excel 2007 files (*.xlsx)|*.xlsx";
            fd.ShowDialog();
            outputFile = fd.FileName;
            outputFileText.Text = outputFile;
        }

        //private void outputFileBtn_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog fd = new OpenFileDialog();
        //    fd.Multiselect = false;
        //    fd.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|Excel 95 files (*.xls)|*.xls";
        //    fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    fd.ShowDialog();
        //    inputFileAED = fd.FileName;
        //    outputFileText.Text = inputFileAED;
        //}

        private void processBtn_Click(object sender, EventArgs e)
        {
            if (inputFile == null || outputFile == null)
            {
                MessageBox.Show("Please provide valid input or output files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Helper hp = new Helper();
            hp.ReadExcelFile(inputFile, lowScore, highScore);

            if (Helper.rangeOfScreensTable != null && Helper.inputScreenFile.Tables.Count != 0)
            {
                System.Console.WriteLine("Read file successfully!\n");

                //GenAlgorithm ga = new GenAlgorithm();
                candidates = ga.applyGenAlgorithm(Helper.rangeOfScreensTable);

                if (candidates.Rows.Count != 0)
                {
                    if (hp.WriteExcelFile(candidates, outputFile))
                    {
                        MessageBox.Show("Excel file has been saved!", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Program could not generate any candidate cosktails for the given input!", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }

        private void inputFileText_TextChanged(object sender, EventArgs e)
        {

        }

        private void outputFileText_TextChanged(object sender, EventArgs e)
        {

        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            inputFileText.Text = ""; inputFile = null;
            outputFileText.Text = ""; outputFile = null;
            mutationRateText.Text = MUTATION_RATE.ToString();
            tournamentSizeText.Text = TOURNAMENT_SIZE.ToString();
            generationText.Text = GENERATIONS.ToString();
            popSizeText.Text = NEW_POP_SIZE.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        //private void CompareBtn_Click(object sender, EventArgs e)
        //{
        //    DataTable aed;
        //    DataTable ga;
        //    DataTable common;
        //    int count = 0;

        //    Helper hp = new Helper();
        //    aed = hp.ReadExcelFileCompare(inputFile).Tables[0];
        //    ga = hp.ReadExcelFileCompare(inputFileAED).Tables[0];
        //    common = ga.Clone();

        //    foreach (DataRow row1 in aed.Rows)
        //    {
        //        foreach (DataRow row2 in ga.Rows)
        //        {
        //            var array1 = row1.ItemArray;
        //            var array2 = row2.ItemArray;
        //            if (array1.SequenceEqual(array2))
        //            {
        //                Console.WriteLine("Common row found!\n  Count: {0}", count++);
        //                common.Rows.Add(row2.ItemArray);
        //            }
        //        }
        //    }

        //    count = 0;
        //    foreach (DataRow dr in common.Rows)
        //    {
        //        Console.Write("_____________________________________________\nCount: {0} \n", count++);
        //        foreach (var item in dr.ItemArray)
        //        {
        //            Console.WriteLine(item);
        //        }
        //    }

        //    if (common.Rows.Count != 0)
        //    {
        //        if (hp.WriteExcelFileCompare(common, outputFile))
        //        {
        //            MessageBox.Show("Excel file has been saved!", "Information",
        //                MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Program could not generate any candidate cosktails for the given input!", "Information",
        //                MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }

        //    }

        //}

        //private void file2Btn_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog fd = new OpenFileDialog();
        //    fd.Multiselect = false;
        //    fd.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|Excel 95 files (*.xls)|*.xls";
        //    fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    fd.ShowDialog();
        //    inputFileAED = fd.FileName;
        //    inputFile2.Text = inputFileAED;
        //}

        //private void inputFile2_TextChanged(object sender, EventArgs e)
        //{

        //}

        private void outputLabel_Click(object sender, EventArgs e)
        {

        }

        private void popSizeText_TextChanged(object sender, EventArgs e)
        {
            ga.newPopSize = Convert.ToInt32(popSizeText.Text);
        }

        private void generationText_TextChanged(object sender, EventArgs e)
        {
            ga.numIter = Convert.ToInt32(generationText.Text);
        }

        private void mutationRateText_TextChanged(object sender, EventArgs e)
        {
            ga.mutationRate = Convert.ToDouble(mutationRateText.Text);
        }

        private void tournamentSizeText_TextChanged(object sender, EventArgs e)
        {
            ga.tournamentSize = Convert.ToInt32(tournamentSizeText.Text);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
