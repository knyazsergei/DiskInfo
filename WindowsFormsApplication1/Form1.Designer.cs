namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.labelInfoAboutDisk = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.allDisks = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInfoAboutDisk
            // 
            this.labelInfoAboutDisk.AutoSize = true;
            this.labelInfoAboutDisk.Location = new System.Drawing.Point(197, 27);
            this.labelInfoAboutDisk.Name = "labelInfoAboutDisk";
            this.labelInfoAboutDisk.Size = new System.Drawing.Size(16, 13);
            this.labelInfoAboutDisk.TabIndex = 1;
            this.labelInfoAboutDisk.Text = "...";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 142);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(284, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(302, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Eject";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // allDisks
            // 
            this.allDisks.FormattingEnabled = true;
            this.allDisks.Location = new System.Drawing.Point(12, 27);
            this.allDisks.Name = "allDisks";
            this.allDisks.Size = new System.Drawing.Size(68, 95);
            this.allDisks.TabIndex = 8;
            this.allDisks.SelectedIndexChanged += new System.EventHandler(this.allDisks_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(101, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 90);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 170);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.allDisks);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.labelInfoAboutDisk);
            this.Name = "Form1";
            this.Text = "Lab1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelInfoAboutDisk;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox allDisks;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

