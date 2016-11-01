namespace GeneticAlgorithmProteinCystallization
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.inputFileText = new System.Windows.Forms.TextBox();
            this.outputFileText = new System.Windows.Forms.TextBox();
            this.inputFileBtn = new System.Windows.Forms.Button();
            this.outputFileBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.processBtn = new System.Windows.Forms.Button();
            this.PopSizeLabel = new System.Windows.Forms.Label();
            this.popSizeText = new System.Windows.Forms.TextBox();
            this.genLabel = new System.Windows.Forms.Label();
            this.generationText = new System.Windows.Forms.TextBox();
            this.mutationRate = new System.Windows.Forms.Label();
            this.mutationRateText = new System.Windows.Forms.TextBox();
            this.tournamentSizeLabel = new System.Windows.Forms.Label();
            this.tournamentSizeText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(25, 27);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(61, 17);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "Input file";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(25, 69);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(73, 17);
            this.outputLabel.TabIndex = 2;
            this.outputLabel.Text = "Output file";
            this.outputLabel.Click += new System.EventHandler(this.outputLabel_Click);
            // 
            // inputFileText
            // 
            this.inputFileText.Location = new System.Drawing.Point(130, 24);
            this.inputFileText.Name = "inputFileText";
            this.inputFileText.Size = new System.Drawing.Size(284, 22);
            this.inputFileText.TabIndex = 3;
            this.inputFileText.TextChanged += new System.EventHandler(this.inputFileText_TextChanged);
            // 
            // outputFileText
            // 
            this.outputFileText.Location = new System.Drawing.Point(130, 67);
            this.outputFileText.Name = "outputFileText";
            this.outputFileText.Size = new System.Drawing.Size(284, 22);
            this.outputFileText.TabIndex = 4;
            this.outputFileText.TextChanged += new System.EventHandler(this.outputFileText_TextChanged);
            // 
            // inputFileBtn
            // 
            this.inputFileBtn.Location = new System.Drawing.Point(430, 24);
            this.inputFileBtn.Name = "inputFileBtn";
            this.inputFileBtn.Size = new System.Drawing.Size(75, 23);
            this.inputFileBtn.TabIndex = 5;
            this.inputFileBtn.Text = "Open...";
            this.inputFileBtn.UseVisualStyleBackColor = true;
            this.inputFileBtn.Click += new System.EventHandler(this.inputFileBtn_Click);
            // 
            // outputFileBtn
            // 
            this.outputFileBtn.Location = new System.Drawing.Point(430, 67);
            this.outputFileBtn.Name = "outputFileBtn";
            this.outputFileBtn.Size = new System.Drawing.Size(75, 23);
            this.outputFileBtn.TabIndex = 6;
            this.outputFileBtn.Text = "Open...";
            this.outputFileBtn.UseVisualStyleBackColor = true;
            this.outputFileBtn.Click += new System.EventHandler(this.outputFileBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.Location = new System.Drawing.Point(195, 193);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(146, 39);
            this.resetBtn.TabIndex = 7;
            this.resetBtn.Text = "Reset";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(370, 194);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(133, 39);
            this.exitBtn.TabIndex = 8;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // processBtn
            // 
            this.processBtn.Location = new System.Drawing.Point(28, 193);
            this.processBtn.Name = "processBtn";
            this.processBtn.Size = new System.Drawing.Size(143, 38);
            this.processBtn.TabIndex = 9;
            this.processBtn.Text = "Process";
            this.processBtn.UseVisualStyleBackColor = true;
            this.processBtn.Click += new System.EventHandler(this.processBtn_Click);
            // 
            // PopSizeLabel
            // 
            this.PopSizeLabel.AutoSize = true;
            this.PopSizeLabel.Location = new System.Drawing.Point(25, 111);
            this.PopSizeLabel.Name = "PopSizeLabel";
            this.PopSizeLabel.Size = new System.Drawing.Size(64, 17);
            this.PopSizeLabel.TabIndex = 14;
            this.PopSizeLabel.Text = "Pop Size";
            // 
            // popSizeText
            // 
            this.popSizeText.Location = new System.Drawing.Point(130, 111);
            this.popSizeText.Name = "popSizeText";
            this.popSizeText.Size = new System.Drawing.Size(87, 22);
            this.popSizeText.TabIndex = 15;
            this.popSizeText.TextChanged += new System.EventHandler(this.popSizeText_TextChanged);
            // 
            // genLabel
            // 
            this.genLabel.AutoSize = true;
            this.genLabel.Location = new System.Drawing.Point(300, 108);
            this.genLabel.Name = "genLabel";
            this.genLabel.Size = new System.Drawing.Size(111, 17);
            this.genLabel.TabIndex = 16;
            this.genLabel.Text = "# of generations";
            // 
            // generationText
            // 
            this.generationText.Location = new System.Drawing.Point(429, 108);
            this.generationText.Name = "generationText";
            this.generationText.Size = new System.Drawing.Size(74, 22);
            this.generationText.TabIndex = 17;
            this.generationText.TextChanged += new System.EventHandler(this.generationText_TextChanged);
            // 
            // mutationRate
            // 
            this.mutationRate.AutoSize = true;
            this.mutationRate.Location = new System.Drawing.Point(25, 150);
            this.mutationRate.Name = "mutationRate";
            this.mutationRate.Size = new System.Drawing.Size(96, 17);
            this.mutationRate.TabIndex = 18;
            this.mutationRate.Text = "Mutation Rate";
            // 
            // mutationRateText
            // 
            this.mutationRateText.Location = new System.Drawing.Point(130, 150);
            this.mutationRateText.Name = "mutationRateText";
            this.mutationRateText.Size = new System.Drawing.Size(87, 22);
            this.mutationRateText.TabIndex = 19;
            this.mutationRateText.TextChanged += new System.EventHandler(this.mutationRateText_TextChanged);
            // 
            // tournamentSizeLabel
            // 
            this.tournamentSizeLabel.AutoSize = true;
            this.tournamentSizeLabel.Location = new System.Drawing.Point(298, 150);
            this.tournamentSizeLabel.Name = "tournamentSizeLabel";
            this.tournamentSizeLabel.Size = new System.Drawing.Size(116, 17);
            this.tournamentSizeLabel.TabIndex = 20;
            this.tournamentSizeLabel.Text = "Tournament Size";
            // 
            // tournamentSizeText
            // 
            this.tournamentSizeText.Location = new System.Drawing.Point(429, 145);
            this.tournamentSizeText.Name = "tournamentSizeText";
            this.tournamentSizeText.Size = new System.Drawing.Size(74, 22);
            this.tournamentSizeText.TabIndex = 21;
            this.tournamentSizeText.TextChanged += new System.EventHandler(this.tournamentSizeText_TextChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 257);
            this.Controls.Add(this.tournamentSizeText);
            this.Controls.Add(this.tournamentSizeLabel);
            this.Controls.Add(this.mutationRateText);
            this.Controls.Add(this.mutationRate);
            this.Controls.Add(this.generationText);
            this.Controls.Add(this.genLabel);
            this.Controls.Add(this.popSizeText);
            this.Controls.Add(this.PopSizeLabel);
            this.Controls.Add(this.processBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.outputFileBtn);
            this.Controls.Add(this.inputFileBtn);
            this.Controls.Add(this.outputFileText);
            this.Controls.Add(this.inputFileText);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.inputLabel);
            this.Name = "MainWindow";
            this.Text = "GenScreen";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.TextBox inputFileText;
        private System.Windows.Forms.TextBox outputFileText;
        private System.Windows.Forms.Button inputFileBtn;
        private System.Windows.Forms.Button outputFileBtn;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button processBtn;
        private System.Windows.Forms.Label PopSizeLabel;
        private System.Windows.Forms.TextBox popSizeText;
        private System.Windows.Forms.Label genLabel;
        private System.Windows.Forms.TextBox generationText;
        private System.Windows.Forms.Label mutationRate;
        private System.Windows.Forms.TextBox mutationRateText;
        private System.Windows.Forms.Label tournamentSizeLabel;
        private System.Windows.Forms.TextBox tournamentSizeText;
    }
}

